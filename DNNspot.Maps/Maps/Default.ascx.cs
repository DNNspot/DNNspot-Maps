/*
* This software is licensed under the GNU General Public License, version 2
* You may copy, distribute and modify the software as long as you track changes/dates of in source files and keep all modifications under GPL. You can distribute your application using a GPL library commercially, but you must also provide the source code.

* DNNspot Software (http://www.dnnspot.com)
* Copyright (C) 2013 Atriage Software LLC
* Authors: Kevin Southworth, Matthew Hall, Ryan Doom

* This program is free software; you can redistribute it and/or
* modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation; either version 2
* of the License, or (at your option) any later version.

* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.

* You should have received a copy of the GNU General Public License
* along with this program; if not, write to the Free Software
* Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

* Full license viewable here: http://www.gnu.org/licenses/gpl-2.0.txt
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Maps.Common;
using DNNspot.Maps.DataModel;
using DNNspot.Maps.DataModel.ES;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using EntitySpaces.Interfaces;
using DNNspot.Newtonsoft.Json;
using WA.FileHelpers.Csv;
using WA.Geocoding;
using WA.Geocoding.Google;
using Marker = DNNspot.Maps.DataModel.ES.Marker;

namespace DNNspot.Maps.Maps
{
    public partial class Default : ModuleBase, IActionable
    {

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection();

                actions.Add(GetNextActionID(), "List Markers", ModuleActionType.EditContent, "", "",
                    EditUrl(ControlKeys.ListMarkers), false, SecurityAccessLevel.Edit, true, false
                    );

                actions.Add(GetNextActionID(), "Upload Markers", ModuleActionType.EditContent, "", "",
                            EditUrl(ControlKeys.UploadMarkers), false, SecurityAccessLevel.Edit, true,
                            false
                    );

                return actions;
            }
        }

        public string TargetModuleId;
        protected string MapLocation = null;
        protected string MapZoom = null;
        protected int AutoZoomLevel = -1;
        public bool IsSearch = false;
        public string CustomFieldLabel;

        private void LoadControls(string customFilter = "")
        {
            if (Convert.ToBoolean(Settings[ModuleSettingNames.ShowMapFilter]))
            {
                pnlMapFilters.Visible = true;

                ddlCountries.DataSource = Queries.GetDistinctCountries(ModuleId, MaxPoints, String.Empty);
                ddlCountries.DataValueField = "Country";
                ddlCountries.DataBind();

                ddlCountries.Items.Insert(0, new ListItem("Filter by:", ""));
            }

            var customFields = Queries.GetDistinctCustomFields(ModuleId, MaxPoints);

            var customFieldList = new List<ListItem>();

            foreach (var field in customFields)
            {
                var splittedCustomField = field.CustomField.Split('|');

                foreach (var splitted in splittedCustomField)
                {
                    if (!string.IsNullOrEmpty(splitted))
                    {
                        customFieldList.Add(new ListItem() { Text = splitted });
                    }
                }
            }

            if (Convert.ToBoolean(Settings[ModuleSettingNames.ShowCustomFieldFilter]))
            {
                pnlCustomMapFilter.Visible = true;

                ddlCustomField.DataSource = customFieldList.Distinct();
                //ddlCustomField.DataValueField = "CustomField";
                ddlCustomField.DataBind();

                ddlCustomField.Items.Insert(0, new ListItem("Filter by:", ""));
            }

            if (Convert.ToBoolean(Settings[ModuleSettingNames.ShowProximitySearch]))
            {
                pnlCustomSearchFilter.Visible = true;
            }
            else
            {
                pnlCustomSearchFilter.Visible = false;
            }


            ddlCustomFilter.DataSource = customFieldList.Distinct();
            //ddlCustomFilter.DataValueField = "CustomField";
            ddlCustomFilter.DataBind();
            ddlCustomFilter.Items.Insert(0, new ListItem("View All", ""));

            if (!string.IsNullOrEmpty(customFilter))
            {
                ddlCustomFilter.SelectedValue = customFilter;
            }
        }


        private string SerializeMarkers(List<ViewAbleMarker> markers)
        {
            return String.Format("<script type=\"text/javascript\"> var markers_{0} = {1}; </script>", ModuleId, JsonConvert.SerializeObject(markers));
        }

        protected new void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            RegisterJavascriptFileOnceInBody("GoogleMapsV3", String.Format("https://maps.googleapis.com/maps/api/js?sensor=false&key={0}", Convert.ToString(Settings[ModuleSettingNames.ApiKey])));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["custom"] != null)
            {
                CustomFilterSelection = Request.Params["custom"];
            }
            TargetModuleId = ModuleId.ToString();
            if (Settings[ModuleSettingNames.HideMapPointsOnPageLoad] != null)
            {
                HidePointsOnPageLoad = Convert.ToBoolean(Settings[ModuleSettingNames.HideMapPointsOnPageLoad]);
            }

            if (!IsPostBack)
            {
                AddMapsDiv();



                litMap.Visible = true;

                CustomFieldLabel = Settings[ModuleSettingNames.CustomFieldLabel] != null ? Convert.ToString(Settings[ModuleSettingNames.CustomFieldLabel]) : "Custom Field";

                LoadMembers();
                LoadControls();

                if (HidePointsOnPageLoad)
                {
                    MaxPoints = 0;
                }

                litMarkers.Text = SerializeMarkers(LoadMarkers(ModuleId));

                if (Request.Params["m"] != null && Request.Params["address"] != null)
                {
                    string moduleId = Request.Params["m"];

                    if (moduleId == ModuleId.ToString())
                    {
                        var address = Request.Params["address"];
                        var custom = Request.Params["custom"];
                        var proximity = Request.Params["proximity"];

                        IsSearch = true;
                        MaxPoints = 9999999;
                        Search(address, custom, proximity);

                        txtAddress.Text = address;
                        ddlCustomFilter.SelectedValue = custom;
                        ddlProximity.SelectedValue = proximity;
                    }
                }
            }
        }

        private void AddMapsDiv()
        {
            litMap.Text += "<div id='DNNspot-Maps-Map-" + ModuleId + "' class='mapsModule' style='height:";
            if (Settings[ModuleSettingNames.MapHeight] == null)
            {
                litMap.Text += "500";
            }
            else
            {
                litMap.Text += Convert.ToString(Settings[ModuleSettingNames.MapHeight]);
            }

            litMap.Text += "px; width: ";

            if (Settings[ModuleSettingNames.MapWidth] == null)
            {
                litMap.Text += "450";
            }
            else
            {
                litMap.Text += Convert.ToString(Settings[ModuleSettingNames.MapWidth]);
            }

            litMap.Text += "px;'></div>";
        }

        private void LoadMembers()
        {
            MapLocation = Request.QueryString["mapLocation"];
            MapZoom = Request.QueryString["mapZoom"];
            if (!String.IsNullOrEmpty((string)(Settings[ModuleSettingNames.AutoZoomLevel])))
            {
                AutoZoomLevel = Convert.ToInt32(Settings[ModuleSettingNames.AutoZoomLevel]);
            }
        }



        private static Coordinates GeocodeLocation(string address)
        {
            Coordinates coordinates = new Coordinates();
            IGeocodeRequest geocodeRequest = new GeocodeRequest(address);

            GoogleGeocoderV3 geocodeService = new GoogleGeocoderV3();
            var geocodeResponse = geocodeService.Geocode(geocodeRequest);

            Thread.Sleep(500);

            if (geocodeResponse.Success)
            {
                coordinates.Latitude = (float)geocodeResponse.Lat;
                coordinates.Longitude = (float)geocodeResponse.Long;
                coordinates.StatusMessage = String.Format("Successfully geocoded {0}", address);
                coordinates.GeocodeSuccess = true;
                return coordinates;
            }
            else
            {
                coordinates.GeocodeSuccess = false;
                coordinates.StatusMessage = String.Format("FAILED to geocode {0}. Response: {1}", address, geocodeResponse.ErrorMessage);
                return coordinates;
            }
        }


        public void Search(string address, string customFilter, string proximity)
        {
            esTransactionScope.IsolationLevel = IsolationLevel.ReadUncommitted;

            int searchRadius;
            if (!int.TryParse(proximity, out searchRadius))
            {
                searchRadius = 10;
            }

            List<ViewAbleMarker> markers = new List<ViewAbleMarker>();

            Coordinates geoResult = new Coordinates() { GeocodeSuccess = false };

            if (!string.IsNullOrEmpty(address))
            {
                geoResult = GeocodeLocation(address);
            }

            if (geoResult.GeocodeSuccess)
            {
                var markersCollection = new MarkerCollection();
                markersCollection.LoadByProximity(geoResult.Latitude, geoResult.Longitude, searchRadius, customFilter, ModuleId);

                markersCollection.ToList().ForEach(marker => markers.Add(new ViewAbleMarker(marker)));
                litMarkers.Text = SerializeMarkers(markers);

                if ((Settings[ModuleSettingNames.SearchResultsTemplate] != null))
                {
                    string template = Convert.ToString(Settings[ModuleSettingNames.SearchResultsTemplate]);

                    litSearchResults.Text = ReplaceTokens(markers, template, ModuleId).ToString();
                }

                if (markers.Count == 0)
                {
                    LoadMembers();
                    LoadControls();
                    litError.Text = "We currently do not have any locations that match your criteria";
                    pnlNoMatches.Visible = true;
                    litMarkers.Text = SerializeMarkers(LoadMarkers(ModuleId));
                }
            }
            else
            {
                LoadMembers();
                LoadControls(CustomFilterSelection);
                if (!string.IsNullOrEmpty(address))
                {
                    litError.Text = "Unable to geocode address";
                    pnlNoMatches.Visible = true;
                }

                litMarkers.Text = !string.IsNullOrEmpty(CustomFilterSelection) ? SerializeMarkers(LoadMarkers(ModuleId, CustomFilterSelection)) : SerializeMarkers(LoadMarkers(ModuleId));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //Search(txtAddress.Text, ddlCustomFilter.SelectedValue, ddlProximity.SelectedValue);

            var uri = new UriBuilder(Request.UrlReferrer);
            uri.Query = String.Format("address={0}&custom={1}&proximity={2}&m={3}", txtAddress.Text, ddlCustomFilter.SelectedValue, ddlProximity.SelectedValue, ModuleId);
            uri = uri;
            Response.Redirect(uri.ToString());
        }
    }
}