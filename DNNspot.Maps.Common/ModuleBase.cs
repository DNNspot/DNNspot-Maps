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
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using DNNspot.Maps.DataModel;
using DNNspot.Maps.DataModel.ES;
using DotNetNuke.Entities.Modules;
using EntitySpaces.Interfaces;
using EntitySpaces.LoaderMT;
using Marker = DNNspot.Maps.DataModel.ES.Marker;

namespace DNNspot.Maps.Common
{
    public class ModuleBase : PortalModuleBase
    {
        protected string ModuleWebPath
        {
            get { return String.Format("/DesktopModules/{0}/", ModuleConfiguration.FolderName); }
        }


        public bool HidePointsOnPageLoad = false;
        public int MaxPoints = 0;
        public string CustomFilterSelection = string.Empty;

        public static class ControlKeys
        {
            public const string License = "License";
            public const string ListMarkers = "ListMarkers";
            public const string EditMarker = "EditMarker";
            public const string UploadMarkers = "UploadMarkers";
            public const string Settings = "Settings";
            public const string Default = "";
        }

        public static StringBuilder ReplaceTokens(IEnumerable<Marker> markers, string template, int moduleId)
        {
            StringBuilder html = new StringBuilder();


            foreach (var marker in markers)
            {
                string tempHtml = template;

                string linkStart = String.Empty;
                string linkEnd = String.Empty;

                if (marker.Longitude != null && marker.Latitude != null)
                {
                    linkStart = String.Format(@"<a onclick=""selectPoint_{5}('{0}', '{1}', '{2}', '{3}', '{4}', '{6}')"" href=""javascript:void(0);"">", marker.Latitude, marker.Longitude, marker.Country, marker.Region, marker.City, moduleId, marker.MarkerId);
                    linkEnd = String.Format(@"</a>");
                }

                tempHtml = tempHtml.Replace("[LINK]", linkStart);
                tempHtml = tempHtml.Replace("[/LINK]", linkEnd);

                tempHtml = tempHtml.Replace("[TITLE]", marker.Title);
                tempHtml = tempHtml.Replace("[INFOWINDOWHTML]", marker.InfoWindowHtml);
                tempHtml = tempHtml.Replace("[ADDRESS1]", marker.Address1);
                tempHtml = tempHtml.Replace("[ADDRESS2]", marker.Address2);
                tempHtml = tempHtml.Replace("[CITY]", marker.City);
                tempHtml = tempHtml.Replace("[REGION]", marker.Region);
                tempHtml = tempHtml.Replace("[POSTALCODE]", marker.PostalCode);
                tempHtml = tempHtml.Replace("[COUNTRY]", marker.Country);
                tempHtml = tempHtml.Replace("[LATITUDE]", marker.Latitude.ToString());
                tempHtml = tempHtml.Replace("[LONGITUDE]", marker.Longitude.ToString());
                tempHtml = tempHtml.Replace("[ICONURL]", marker.IconUrl);
                tempHtml = tempHtml.Replace("[ICONSHADOWURL]", marker.IconShadowUrl);
                tempHtml = tempHtml.Replace("[CUSTOMFIELD]", marker.CustomField);
                tempHtml = tempHtml.Replace("[PHONENUMBER]", marker.PhoneNumber);

                html.AppendFormat(tempHtml);
            }


            return html;
        }

        public static StringBuilder ReplaceTokens(IEnumerable<ViewAbleMarker> markers, string template, int moduleId)
        {
            StringBuilder html = new StringBuilder();


            foreach (var marker in markers)
            {
                string tempHtml = template;

                string linkStart = String.Empty;
                string linkEnd = String.Empty;

                if (marker.Longitude != null && marker.Latitude != null)
                {
                    linkStart = String.Format(@"<a onclick=""selectPoint_{5}('{0}', '{1}', '{2}', '{3}', '{4}', '{6}')"" href=""javascript:void(0);"">", marker.Latitude, marker.Longitude, marker.Country, marker.Region, marker.City, moduleId, marker.MarkerId);
                    linkEnd = String.Format(@"</a>");
                }

                tempHtml = tempHtml.Replace("[LINK]", linkStart);
                tempHtml = tempHtml.Replace("[/LINK]", linkEnd);

                tempHtml = tempHtml.Replace("[TITLE]", marker.Title);
                tempHtml = tempHtml.Replace("[INFOWINDOWHTML]", marker.InfoWindowHtml);
                tempHtml = tempHtml.Replace("[ADDRESS1]", marker.Address1);
                tempHtml = tempHtml.Replace("[ADDRESS2]", marker.Address2);
                tempHtml = tempHtml.Replace("[CITY]", marker.City);
                tempHtml = tempHtml.Replace("[REGION]", marker.Region);
                tempHtml = tempHtml.Replace("[POSTALCODE]", marker.PostalCode);
                tempHtml = tempHtml.Replace("[COUNTRY]", marker.Country);
                tempHtml = tempHtml.Replace("[LATITUDE]", marker.Latitude.ToString());
                tempHtml = tempHtml.Replace("[LONGITUDE]", marker.Longitude.ToString());
                tempHtml = tempHtml.Replace("[ICONURL]", marker.IconUrl);
                tempHtml = tempHtml.Replace("[ICONSHADOWURL]", marker.IconShadowUrl);
                tempHtml = tempHtml.Replace("[CUSTOMFIELD]", marker.CustomField);
                tempHtml = tempHtml.Replace("[PRIORITY]", marker.Priority);
                tempHtml = tempHtml.Replace("[PHONENUMBER]", marker.PhoneNumber);
                tempHtml = tempHtml.Replace("[DISTANCE]", marker.Distance.ToString("0.0"));
                tempHtml = tempHtml.Replace("[DIRECTIONSLINK]", String.Format("<a href='javascript:void(0);' onclick=\"calculateDirections_{0}({1},{2})\">", marker.ModuleId, marker.Latitude, marker.Longitude));
                tempHtml = tempHtml.Replace("[/DIRECTIONSLINK]", "</a>");

                if (marker.Priority != "zzzz")
                {
                    tempHtml = tempHtml.Replace("[IFPRIORITY]", String.Empty);
                    tempHtml = tempHtml.Replace("[/IFPRIORITY]", String.Empty);
                }
                else
                {
                    int start = tempHtml.IndexOf("[IFPRIORITY]") == -1 ? 0 : tempHtml.IndexOf("[IFPRIORITY]");
                    int end = 0;
                    if (tempHtml.Contains("[/IFPRIORITY]"))
                    {
                        end = tempHtml.LastIndexOf("[/IFPRIORITY]") + 13; // ADD LENGTH OF IFPRIORITY
                    }

                    tempHtml = tempHtml.Remove(start, end - start);
                }

                html.AppendFormat(tempHtml);
            }


            return html;
        }

        private static void InitializeProvider()
        {
            if (esConfigSettings.ConnectionInfo.Default != "SiteSqlServer")
            {
                var connectionInfoSettings = esConfigSettings.ConnectionInfo;
                foreach (esConnectionElement connection in connectionInfoSettings.Connections)
                {
                    //if there is a SiteSqlServer in es connections set it default
                    if (connection.Name == "SiteSqlServer")
                    {
                        esConfigSettings.ConnectionInfo.Default = connection.Name;
                        return;
                    }
                }

                //no SiteSqlServer found grab dnn cnn string and create
                var dnnConnection = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString;

                // Manually register a connection
                var conn = new esConnectionElement();
                conn.ConnectionString = dnnConnection;
                conn.Name = "SiteSqlServer";
                conn.Provider = "EntitySpaces.SqlClientProvider";
                conn.ProviderClass = "DataProvider";
                conn.SqlAccessType = esSqlAccessType.DynamicSQL;
                conn.ProviderMetadataKey = "esDefault";
                conn.DatabaseVersion = "2005";

                // Assign the Default Connection
                esConfigSettings.ConnectionInfo.Connections.Add(conn);
                esConfigSettings.ConnectionInfo.Default = "SiteSqlServer";

                // Register the Loader
                esProviderFactory.Factory = new esDataProviderFactory();
            }
        }

        protected bool IsAuthenticatedEditor()
        {
            return ModuleConfiguration.AuthorizedEditRoles.Split(',').Any(role => UserInfo.IsInRole(role));
        }

        protected void Page_Init(Object sender, EventArgs e)
        {
            InitializeProvider();
        }

        protected void RegisterJavascriptFileOnceInBody(string keyName, string pathToJsFile)
        {
            if (!Page.ClientScript.IsClientScriptIncludeRegistered(keyName))
            {
                Page.ClientScript.RegisterClientScriptInclude(keyName, pathToJsFile);
            }
        }

        public List<ViewAbleMarker> LoadMarkers(int moduleId, string customFieldFilter = "")
        {
            var markers = new List<ViewAbleMarker>();
            var markerQuery = new MarkerQuery();
            var markerCollection = new MarkerCollection();

            markerQuery.Where(markerQuery.ModuleId == moduleId && markerQuery.Latitude.IsNotNull() && markerQuery.Longitude.IsNotNull()).OrderBy("Priority").OrderBy(markerQuery.Title.Ascending);

            if (HidePointsOnPageLoad)
            {
                MaxPoints = 0;
                markerQuery.es.Top = MaxPoints;
            }

            if (!string.IsNullOrEmpty(customFieldFilter))
            {
                markerQuery.Where(markerQuery.CustomField.Like(String.Format("%{0}%", customFieldFilter)));
            }

            markerCollection.Load(markerQuery);
            markerCollection.ToList().ForEach(marker => markers.Add(new ViewAbleMarker(marker)));
            return markers;
        }

        protected void RegisterJavascript(string fullPath)
        {
            var script = new HtmlGenericControl("script");
            script.Attributes.Add("type", "text/javascript");
            script.Attributes.Add("src", fullPath);

            Page.Header.Controls.Add(script);
        }

        protected static string GetModuleFolderPath()
        {
            HttpRequest request = HttpContext.Current.Request;

            string path = (request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath);

            if (!path.EndsWith(path))
            {
                path = path + "/";
            }
            return path.TrimEnd('/') + string.Format(@"/DesktopModules/DNNspot-Maps/");
        }

        protected new bool IsEditable
        {
            get
            {
                return base.IsEditable || UserInfo.IsSuperUser || UserInfo.IsInRole(PortalSettings.AdministratorRoleName);
            }
        }
    }
}