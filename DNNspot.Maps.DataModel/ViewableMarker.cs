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
using System.Linq;
using System.Web;
using DNNspot.Maps.DataModel.ES;

namespace DNNspot.Maps.DataModel
{
    public class ViewAbleMarker
    {
        public string InfoWindowHtml { get; set; }

        public int MarkerId { get; set; }
        public int ModuleId { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public string CustomField { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string IconUrl { get; set; }
        public string IconShadowUrl { get; set; }
        public double Distance { get; set; }

        public string Title { get; set; }
        public string Priority { get; set; }
        public string PhoneNumber { get; set; }

        public ViewAbleMarker() { }

        public ViewAbleMarker(Marker marker)
        {
            MarkerId = marker.MarkerId.Value;
            Address1 = marker.Address1;
            Address2 = marker.Address2;
            City = marker.City;
            Region = marker.Region;
            PostalCode = marker.PostalCode;
            Country = marker.Country;

            Latitude = marker.Latitude.GetValueOrDefault(0);
            Longitude = marker.Longitude.GetValueOrDefault(0);

            IconUrl = marker.IconUrl;
            IconShadowUrl = marker.IconShadowUrl;
            InfoWindowHtml = marker.InfoWindowHtml;

            Distance = Convert.ToDouble(marker.GetColumn("Distance"));
            CustomField = marker.CustomField;
            ModuleId = marker.ModuleId.Value;

            Title = marker.Title;
            Priority = marker.Priority;
            PhoneNumber = marker.PhoneNumber;


        }
    }
}