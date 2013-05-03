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
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using DNNspot.Maps.Common;
using DNNspot.Maps.DataModel.ES;
using DNNspot.Newtonsoft.Json;
using EntitySpaces.Interfaces;
using EntitySpaces.LoaderMT;
using WA.Geocoding;
using WA.Geocoding.Google;

namespace DNNspot.Maps.Maps
{
    /// <summary>
    /// Summary description for GeocodeHandler
    /// </summary>
    public class GeocodeHandler : IHttpHandler
    {
        #region IHttpHandler Members

        public void ProcessRequest(HttpContext context) {
            InitializeEntitySpaces();

            var Request = context.Request;
            var Response = context.Response;
            var output = String.Empty;

            if (Request.QueryString["moduleId"] != null)
            {
                var moduleId = Convert.ToInt32(Request.QueryString["moduleId"]);

                if (Request.QueryString["markerId"] != null)
                {
                    var markerId = Convert.ToInt32(Request.QueryString["markerId"]);
                    Marker marker = new Marker();
                    var jsonObject = new
                                         {
                                             success = true,
                                             hasMore = false,
                                             message = GeocodeMarker(moduleId, markerId),
                                             latitude = GetLatitudeByMarkerId(markerId),
                                             longitude = GetLongitudeByMarkerId(markerId)
                                         };
                    output = JsonConvert.SerializeObject(jsonObject);
                }
                else
                {
                    output = JsonConvert.SerializeObject(new { success = true, hasMore = HasMore(moduleId), message = GeocodeNextModuleMarker(moduleId) });        
                }
            }
            else {
                output = JsonConvert.SerializeObject(new{success = false, hasMore = false, message = "ModuleId is not defined."});
            }

            Response.ContentType = "application/json";
            Response.Write(output);
            
            Response.Flush();
            Response.End();
        }

        public bool IsReusable
        {
            get { return false; }
        }

        #endregion

        private static String GeocodeNextModuleMarker(int moduleId)
        {
            var settings = new DotNetNuke.Entities.Modules.ModuleController().GetModuleSettings(moduleId);

            var markers = GetTopMarkerByModuleId(moduleId);

            if (markers.Count < 1)
                return "All markers have been geocoded";

            var marker = markers.First();
            var address = String.Format("{0} {1}, {2}, {3} {4} {5}", marker.Address1, marker.Address2, marker.City, marker.Region, marker.PostalCode, marker.Country);
            //var geocodeRequest = new GeocodeRequest(address);
            //var geocodeService = new GoogleGeocodeService((string)(settings[ModuleSettingNames.ApiKey] ?? "ABQIAAAAeqB6L08C1Tf8o24Lvoqq9BT2yXp_ZAY8_ufC3CFXhHIE1NvwkxRTRiEJPO7V86QE95aNqonFXBhDOQ"));

            var geocodeResult = GeocodeLocation(address);
            if (geocodeResult.GeocodeSuccess)
            {
                marker.Latitude = geocodeResult.Latitude;
                marker.Longitude = geocodeResult.Longitude;
                markers.Save();
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
                coordinates.Latitude = (float) geocodeResponse.Lat;
                coordinates.Longitude = (float) geocodeResponse.Long;
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

        private static String GeocodeMarker(int moduleId, int markerId)
        {
            var settings = new DotNetNuke.Entities.Modules.ModuleController().GetModuleSettings(moduleId);

            //var markers = GetTopMarkerByModuleId(moduleId);

            //if (markers.Count < 1)
            //    return "All markers have been geocoded";

            Marker marker = new Marker();
            marker.LoadByPrimaryKey(markerId);

            //var marker = markers.First();
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

        private static bool HasMore(int moduleId)
        {
            return GetTopMarkerByModuleId(moduleId).Count > 0;
        }

        private static string GetLatitudeByMarkerId(int markerId)
        {
            Marker marker = new Marker();
            marker.LoadByPrimaryKey(markerId);

            return Convert.ToString(marker.Latitude);
        }

        private static string GetLongitudeByMarkerId(int markerId)
        {
            Marker marker = new Marker();
            marker.LoadByPrimaryKey(markerId);

            return Convert.ToString(marker.Longitude);
        }

        private static void InitializeEntitySpaces()
        {
            if (esConfigSettings.ConnectionInfo.Default != "SiteSqlServer")
            {
                esConfigSettings connectionInfoSettings = esConfigSettings.ConnectionInfo;
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
                string dnnConnection = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString;

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
    }
}