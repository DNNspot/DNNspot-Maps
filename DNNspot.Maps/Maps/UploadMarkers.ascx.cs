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
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using DNNspot.Maps.Common;
using DNNspot.Maps.DataModel.ES;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Framework;
using DotNetNuke.Security;
using WA.FileHelpers.Csv;
using WA.Geocoding;
using WA.Geocoding.Google;

namespace DNNspot.Maps.Maps
{
    public partial class UploadMarkers : ModuleBase
    {
        private List<Marker> _markers = new List<Marker>();
        protected new void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);

            if (AJAX.IsInstalled())
                AJAX.RegisterPostBackControl(btnUpload);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ToggleGeocodeMarkersPanel();

            if (!IsPostBack)
                LoadControls();
        }

        private void ToggleGeocodeMarkersPanel()
        {
            MarkerCollection nongeocodedMarker = GeocodeHandler.GetTopMarkerByModuleId(ModuleId);
            if (nongeocodedMarker.Count == 1)
            {
             
                pnlGeocodeMarkers.Visible = true;
            }
            else
            {
                pnlGeocodeMarkers.Visible = false;
            }
        }

        public static MarkerCollection GetTopMarkerByModuleId(int moduleId)
        {
            var markerQuery = new MarkerQuery();
            var markerCollection = new MarkerCollection();

            markerQuery.es.Top = 1;
            markerQuery.Where(markerQuery.ModuleId == moduleId && markerQuery.Latitude.IsNull() && markerQuery.Longitude.IsNull());

            markerCollection.Query.es.Top = 1; // Not sure if the first "Top" assignment took care of this
            markerCollection.Load(markerQuery);

            return markerCollection;
        }

        private void LoadControls()
        {
            var filePath = String.Format("{0}Maps/uploads/{1}", ModuleWebPath, Settings[ModuleSettingNames.FileName]);

            if (File.Exists(Server.MapPath(filePath)))
                lnkDownload.NavigateUrl = filePath;
            else
                lnkDownload.Enabled = false;
        }

        private String SaveCsvFile()
        {
            if (!fupCsvFile.HasFile)
                return "Please select a CSV file to be uploaded.";

            var filePath = Server.MapPath(String.Format("{0}Maps/uploads/{1}", ModuleWebPath, fupCsvFile.FileName));
            fupCsvFile.SaveAs(filePath);

            return null;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            btnUpload.Text = "Please wait...";
            btnUpload.Enabled = false;
            litMessage.Text = "Uploading and parsing file. Geocoding markers without coordinates. Please do not reload the page.";

            var message = SaveCsvFile();
            string status = string.Empty;
            if (message == null)
            {
                message = "Successfully uploaded CSV file.";

                var moduleController = new ModuleController();
                moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.FileName, fupCsvFile.FileName);

                status = ParseCsvFile();
            }

            litMessage.Text = message + " " + status;
        }

        private string ParseCsvFile()
        {
            var fileName = Settings[ModuleSettingNames.FileName];
            var filePath = HttpContext.Current.Server.MapPath(String.Format("{0}Maps/uploads/{1}", ModuleWebPath, fileName));
            string status = string.Empty;
            if (!File.Exists(filePath))
            {
                status = String.Format("File {0} doesn't exist. Please upload a CSV file in the module settings.", fileName);
                return status;
            }

            var csvOptions = new CsvReaderOptions() { HasHeaderRecord = true, Strict = false };
            var parser = new CsvParser(new StreamReader(filePath));
            var reader = new CsvReader(parser, csvOptions);

            // Get header names
            var address1 = (string)(Settings[ModuleSettingNames.Address1] ?? ModuleSettingNames.Address1);
            var address2 = (string)(Settings[ModuleSettingNames.Address2] ?? ModuleSettingNames.Address2);
            var city = (string)(Settings[ModuleSettingNames.City] ?? ModuleSettingNames.City);
            var country = (string)(Settings[ModuleSettingNames.Country] ?? ModuleSettingNames.Country);
            var iconUrl = (string)(Settings[ModuleSettingNames.IconUrl] ?? ModuleSettingNames.IconUrl);
            var iconShadowUrl = (string)(Settings[ModuleSettingNames.IconShadowUrl] ?? ModuleSettingNames.IconShadowUrl);
            var infoWindowHtml = (string)(Settings[ModuleSettingNames.InfoWindowHtml] ?? ModuleSettingNames.InfoWindowHtml);
            var latitude = (string)(Settings[ModuleSettingNames.Latitude] ?? ModuleSettingNames.Latitude);
            var longitude = (string)(Settings[ModuleSettingNames.Longitude] ?? ModuleSettingNames.Longitude);
            var postalCode = (string)(Settings[ModuleSettingNames.PostalCode] ?? ModuleSettingNames.PostalCode);
            var region = (string)(Settings[ModuleSettingNames.Region] ?? ModuleSettingNames.Region);
            var title = (string)(Settings[ModuleSettingNames.Title] ?? ModuleSettingNames.Title);
            var customField = (string)(Settings[ModuleSettingNames.CustomField] ?? ModuleSettingNames.CustomField);
            var priority = (string)(Settings[ModuleSettingNames.Priority] ?? ModuleSettingNames.Priority);
            var phoneNumber = (string)(Settings[ModuleSettingNames.PhoneNumber] ?? ModuleSettingNames.PhoneNumber);

            // Delete old markers
            var markerQuery = new MarkerQuery();
            markerQuery.Where(markerQuery.ModuleId == ModuleId);

            var markerCollection = new MarkerCollection();
            markerCollection.Load(markerQuery);
            markerCollection.MarkAllAsDeleted();
            markerCollection.Save();


            while (reader.Read())
            {
                // Add new markers to db from csv
                try
                {

                    var marker = new Marker
                    {
                        ModuleId = ModuleId,
                        Title = reader.GetField(title),
                        InfoWindowHtml = reader.GetField(infoWindowHtml),
                        Address1 = reader.GetField(address1),
                        Address2 = reader.GetField(address2),
                        City = reader.GetField(city),
                        Region = reader.GetField(region),
                        PostalCode = reader.GetField(postalCode),
                        Country = reader.GetField(country),
                        Latitude = WA.Parser.ToDouble(reader.GetField(latitude)),
                        Longitude = WA.Parser.ToDouble(reader.GetField(longitude)),
                        IconUrl = reader.GetField(iconUrl),
                        IconShadowUrl = reader.GetField(iconShadowUrl),
                        CustomField = reader.GetField(customField),
                        PhoneNumber = reader.GetField(phoneNumber),
                        Priority = (reader.GetField(priority) == "" || reader.GetField(priority) == null) ? "zzzz" : reader.GetField(priority)
                    };

                    marker.Save();

                    if (!marker.Latitude.HasValue && !marker.Longitude.HasValue)
                    {
                        _markers.Add(marker);
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            status = String.Format("<h1>File {0} parsed successfully</h1><hr />", fileName);
            status += GeocodeMarkerList(_markers);

            return status;
        }

        private static string GeocodeMarkerList(List<Marker> markers)
        {
            string status = String.Empty;
            if (markers.Count > 0)
            {
                status += "<h1>Geocode Results:</h1>";
                foreach (Marker marker in markers)
                {
                    status += "<p>" + GeocodeMarker(marker) + "</p>";
                }
            }
            else
            {
                status += "<h2>All markers geocoded</h2>";
            }

            return status;
        }

        private static String GeocodeMarker(Marker marker)
        {
            var address = String.Format("{0} {1}, {2}, {3} {4} {5}", marker.Address1, marker.Address2, marker.City, marker.Region, marker.PostalCode, marker.Country);

            var geocodeResult = GeocodeLocation(address);
            if (geocodeResult.GeocodeSuccess)
            {
                marker.Latitude = geocodeResult.Latitude;
                marker.Longitude = geocodeResult.Longitude;
                marker.Save();
            }
            return geocodeResult.StatusMessage;
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

        private void ExportMarkersFromModule()
        {
            var markers = MarkerCollection.LoadAll(ModuleId);

            List<CsvMarkerInfo> csvProducts = MarkerExport.GetCsvMarkersList(markers);

            Response.Clear();
            Response.ClearHeaders();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", String.Format("attachment; filename=markers-{0}.csv", ModuleId));

            MarkerExport.ExportMarkers(csvProducts, Response.OutputStream);

            Response.Flush();
            Response.End();
        }

        protected void lbDownloadCsv_Click(object sender, EventArgs e)
        {
            ExportMarkersFromModule();
        }

        protected void btnGeocodeRemainingMarkers_Click(object sender, EventArgs e)
        {
            List<Marker> markers = MarkerCollection.LoadNotGeolocated(ModuleId).ToList();

            litMessage.Text = GeocodeMarkerList(markers);
        }
    }
}