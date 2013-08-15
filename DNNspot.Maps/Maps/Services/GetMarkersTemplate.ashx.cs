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
using System.Web;
using DNNspot.Maps.DataModel;
using DNNspot.Maps.DataModel.ES;
using DNNspot.Newtonsoft.Json;
using EntitySpaces.Interfaces;
using EntitySpaces.LoaderMT;

namespace DNNspot.Maps.Maps.Services
{
    /// <summary>
    /// Summary description for GetMarkersTemplate
    /// </summary>
    public class GetMarkersTemplate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            SharedMethods.InitializeEntitySpaces();
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            context.Response.ContentType = "application/json";


            int maxPoints = int.MaxValue;

            var customField = request.Params["customField"] ?? String.Empty;
            var country = request.Params["country"] ?? String.Empty;
            var state = request.Params["state"] ?? String.Empty;
            var city = request.Params["city"] ?? String.Empty;

            var moduleId = Convert.ToInt32(request.Params["moduleId"]);
            var targetModuleId = Convert.ToInt32(request.Params["targetModuleId"]);

            var markerCollection = Queries.GetMarkersByState(targetModuleId, customField, country, state, maxPoints, null);

            var markers = new List<ViewAbleMarker>();
            markerCollection.ToList().ForEach(marker => markers.Add(new ViewAbleMarker(marker)));

            DotNetNuke.Entities.Modules.ModuleController objModules = new DotNetNuke.Entities.Modules.ModuleController();
            var settings = objModules.GetModuleSettings(moduleId);
            var template = Convert.ToString(settings[DNNspot.Maps.MarkerListing.ModuleSettingNames.ListTemplate]);
            var markerHtml = DNNspot.Maps.Common.ModuleBase.ReplaceTokens(markerCollection, template, moduleId);

            var jsonObject = new { message = String.Empty, success = true, markerHtml = markerHtml.ToString() };

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