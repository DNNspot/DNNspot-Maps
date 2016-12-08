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
using DNNspot.Maps.DataModel;
using DNNspot.Maps.DataModel.ES;
using EntitySpaces.Interfaces;
using EntitySpaces.LoaderMT;
using DNNspot.Newtonsoft.Json;
using Marker = DNNspot.Maps.DataModel.ES.Marker;

namespace DNNspot.Maps.Maps.Services
{
    /// <summary>
    /// Summary description for CitiesChange
    /// </summary>
    public class CitiesChange : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            InitializeEntitySpaces();
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            context.Response.ContentType = "application/json";

            int maxPoints = Convert.ToInt32(request.Params["MaxPoints"]);

            var customField = request.Params["customField"];
            var country = request.Params["country"];
            var state = request.Params["state"];
            var city = request.Params["city"];
            var moduleId = Convert.ToInt32(request.Params["moduleId"]);

            var markerCollection = Queries.GetMarkersByCity(moduleId, customField, country, state, city, maxPoints);

            var markers = new List<ViewAbleMarker>();
            markerCollection.ToList().ForEach(marker => markers.Add(new ViewAbleMarker(marker)));

            var jsonObject = new { message = String.Empty, success = true, markers };

            response.Write(JsonConvert.SerializeObject(jsonObject));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
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