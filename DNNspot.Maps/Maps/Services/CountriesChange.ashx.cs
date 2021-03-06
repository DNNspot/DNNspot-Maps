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
    /// Summary description for CountriesChange
    /// </summary>
    public class CountriesChange : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            SharedMethods.InitializeEntitySpaces();
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            context.Response.ContentType = "application/json";

            int maxPoints = Convert.ToInt32(request.Params["MaxPoints"]);

            var customField = request.Params["customField"];
            var country = request.Params["country"];
            var moduleId = Convert.ToInt32(request.Params["moduleId"]);

            var states = Queries.GetDistinctStates(moduleId, customField, country, maxPoints);
            var statesHtml = SharedMethods.CreateStatesHtml(states);
            var markerCollection = Queries.GetMarkersByCountry(moduleId, customField, country, maxPoints);

            var markers = new List<ViewAbleMarker>();
            markerCollection.ToList().ForEach(marker => markers.Add(new ViewAbleMarker(marker)));

            var jsonObject = new { states = statesHtml, message = String.Empty, success = true, markers };

            response.Write(JsonConvert.SerializeObject(jsonObject));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}