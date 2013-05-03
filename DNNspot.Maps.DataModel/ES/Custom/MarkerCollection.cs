/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2012.1.0930.0
EntitySpaces Driver  : SQL
Date Generated       : 4/12/2013 11:13:17 AM
===============================================================================
*/

using System;

using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;

namespace DNNspot.Maps.DataModel.ES
{
	public partial class MarkerCollection : esMarkerCollection
	{
		public MarkerCollection()
		{
		
		}

        public bool LoadByProximity(float centerLat, float centerLon, float searchRadiusInMiles, string customField, int moduleId)
        {
            var parms = new esParameters();
            parms.Add("CenterLat", centerLat);
            parms.Add("CenterLon", centerLon);
            parms.Add("SearchRadiusMiles", searchRadiusInMiles);
            parms.Add("CustomField", customField);
            parms.Add("ModuleId", moduleId);

            //string objectQualifier = DnnHelper.GetDbObjectQualifier() ?? "";
            //string dbOwner = DnnHelper.GetDbOwner() ?? "";
            //if (!string.IsNullOrEmpty(dbOwner))
            //{
            //    dbOwner += ".";
            //}

            //return Load(esQueryType.StoredProcedure, String.Format("{0}{1}DNNspot_Find_By_Proximity", dbOwner, objectQualifier), parms);
            return Load(esQueryType.StoredProcedure, "DNNspot_Find_By_Proximity", parms);
        }

        public static MarkerCollection LoadAll(int moduleId)
        {
            MarkerQuery q = new MarkerQuery();
            q.Where(q.ModuleId == moduleId);

            MarkerCollection markers = new MarkerCollection();
            markers.Load(q);

            return markers;
        }

        public static MarkerCollection LoadNotGeolocated(int moduleId)
        {
            MarkerQuery q = new MarkerQuery();
            q.Where(q.ModuleId == moduleId);
            q.Where(q.Longitude.IsNull()).Or(q.Latitude.IsNull());

            MarkerCollection markers = new MarkerCollection();
            markers.Load(q);

            return markers;
        }
	}
}
