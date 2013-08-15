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
using System.Text;
using DNNspot.Maps.DataModel.ES;

namespace DNNspot.Maps.DataModel
{
    public class Queries
    {
        public static MarkerCollection GetDistinctCustomFields(int moduleId, int maxPoints)
        {
            MarkerCollection customFields = new MarkerCollection();
            MarkerQuery q = new MarkerQuery();
            q.Select(q.CustomField, q.ModuleId);

            q.Where(q.ModuleId == moduleId);
            q.Where(q.Latitude.IsNotNull() && q.Longitude.IsNotNull() && q.CustomField != "");

            q.es.Distinct = true;
            q.OrderBy(q.CustomField.Ascending);

            customFields.Load(q);

            return customFields;
        }

        public static MarkerCollection GetDistinctCountriesByCustomField(int moduleId, int maxPoints, string customField)
        {
            MarkerCollection countries = new MarkerCollection();
            MarkerQuery q = new MarkerQuery();
            q.Select(q.Country, q.ModuleId);

            q.Where(q.ModuleId == moduleId);

            if (!String.IsNullOrEmpty(customField))
            {
                q.Where(q.CustomField == customField);
            }

            q.Where(q.Latitude.IsNotNull() && q.Longitude.IsNotNull());

            q.es.Distinct = true;
            q.OrderBy(q.Country.Ascending);

            countries.Load(q);

            return countries;
        }

        public static MarkerCollection GetDistinctCountries(int moduleId, int maxPoints, string customField)
        {
            var countries = new MarkerCollection();
            MarkerQuery q = new MarkerQuery();

            q.Select(q.Country)
                .Where(q.ModuleId == moduleId)
                .Where(q.Latitude.IsNotNull() && q.Longitude.IsNotNull())
                .OrderBy(q.Country.Ascending);
            //q.OrderBy(q.Country.Ascending);


            if (!String.IsNullOrEmpty(customField))
            {
                //q.Where(q.CustomField == customField);
            }


            
            q.es.Distinct = true;
            countries.Load(q);

            return countries;
        }

        public static MarkerCollection GetDistinctStates(int moduleId, string customField, string country, int maxPoints)
        {
            MarkerCollection states = new MarkerCollection();
            MarkerQuery q = new MarkerQuery();
            q.Select(q.Region);

            q.Where(q.ModuleId == moduleId);

            if (!String.IsNullOrEmpty(customField))
            {
                q.Where(q.CustomField == customField);
            }

            q.Where(q.Country == country);
            q.Where(q.Latitude.IsNotNull() && q.Longitude.IsNotNull());
            q.OrderBy(q.Region.Ascending);
            q.es.Distinct = true;
            

            states.Load(q);

            return states;
        }



        public static MarkerCollection GetDistinctCities(int moduleId, string customField, string country, string state, int maxPoints)
        {
            MarkerCollection states = new MarkerCollection();
            MarkerQuery q = new MarkerQuery();
            q.Select(q.City);

            if (!String.IsNullOrEmpty(customField))
            {
                q.Where(q.CustomField == customField);
            }

            q.Where(q.ModuleId == moduleId);
            q.Where(q.Country == country);
            q.Where(q.Region == state);
            q.Where(q.Latitude.IsNotNull() && q.Longitude.IsNotNull());
            q.OrderBy(q.City.Ascending);
            q.es.Distinct = true;
            

            states.Load(q);

            return states;
        }

        public static MarkerCollection GetMarkersByCountry(int moduleId, string customField, string country, int maxPoints)
        {
            MarkerCollection markers = new MarkerCollection();
            MarkerQuery q = new MarkerQuery();

            q.Where(q.ModuleId == moduleId);
            q.Where(q.Country == country);

            if (!String.IsNullOrEmpty(customField))
            {
                q.Where(q.CustomField == customField);
            }

            q.Where(q.Latitude.IsNotNull() && q.Longitude.IsNotNull());

            markers.Load(q);

            return markers;
        }

        public static MarkerCollection GetMarkersByState(int moduleId, string customField, string country, string state, int maxPoints, int? markerId)
        {
            MarkerCollection markers = new MarkerCollection();
            MarkerQuery q = new MarkerQuery();

            q.Where(q.ModuleId == moduleId);

            if (!String.IsNullOrEmpty(customField))
            {
                q.Where(q.CustomField == customField);
            }

            if (!String.IsNullOrEmpty(country))
            {
                q.Where(q.Country == country);
            }

            if (!String.IsNullOrEmpty(state))
            {
                q.Where(q.Region == state);
            }

            if(markerId != null)
            {
                q.Where(q.MarkerId == markerId.Value);
            }

            q.es.Top = maxPoints;

            q.Where(q.Latitude.IsNotNull() && q.Longitude.IsNotNull());

            markers.Load(q);

            return markers;
        }

        public static MarkerCollection GetMarkersByCity(int moduleId, string customField, string country, string state, string city, int maxPoints)
        {
            MarkerCollection markers = new MarkerCollection();
            MarkerQuery q = new MarkerQuery();

            if (!String.IsNullOrEmpty(customField))
            {
                q.Where(q.CustomField == customField);
            }

            q.Where(q.ModuleId == moduleId);
            q.Where(q.Country == country);
            q.Where(q.Region == state);
            q.Where(q.City == city);

            q.Where(q.Latitude.IsNotNull() && q.Longitude.IsNotNull());

            markers.Load(q);

            return markers;
        }

        public static MarkerCollection GetMarkersByCustomField(int moduleId, string customField, string country, string state, string city, int maxPoints)
        {
            MarkerCollection markers = new MarkerCollection();
            MarkerQuery q = new MarkerQuery();

            q.Where(q.ModuleId == moduleId);

            if (!String.IsNullOrEmpty(customField))
            {
                q.Where(q.CustomField == customField);
            }

            if (!String.IsNullOrEmpty(country))
            {
                q.Where(q.Country == country);
            }

            if (!String.IsNullOrEmpty(state))
            {
                q.Where(q.Region == state);
            }

            if (!String.IsNullOrEmpty(city))
            {
                q.Where(q.City == city);
            }

            q.Where(q.Latitude.IsNotNull() && q.Longitude.IsNotNull());

            markers.Load(q);

            return markers;
        }

        public static MarkerCollection FilterByCustomField(int moduleId, string customField, string country, string state, string city, int maxPoints)
        {
            MarkerCollection states = new MarkerCollection();
            MarkerQuery q = new MarkerQuery();
            q.Select(q.Country, q.ModuleId, q.Region, q.City);

            q.Where(q.ModuleId == moduleId);
            q.Where(q.CustomField == customField);

            if (!String.IsNullOrEmpty(country))
            {
                q.Where(q.Country == country);
            }

            if (!String.IsNullOrEmpty(state))
            {
                q.Where(q.Region == state);
            }

            if (!String.IsNullOrEmpty(city))
            {
                q.Where(q.City == city);
            }
            q.Where(q.Latitude.IsNotNull() && q.Longitude.IsNotNull());

            q.es.Distinct = true;
            q.OrderBy(q.Country.Ascending);

            states.Load(q);

            return states;
        }
    }
}
