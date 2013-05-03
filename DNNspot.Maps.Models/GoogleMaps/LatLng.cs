using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DNNspot.Maps.Models.GoogleMaps
{
    public class LatLng
    {
        public LatLng(float lat, float lng) {
            Lat = lat;
            Lng = lng;
        }

        public LatLng(float lat, float lng, bool noWrap) {
            Lat = lat;
            Lng = lng;
            NoWrap = noWrap;
        }

        public float Lat { get; set; }
        public float Lng { get; set; }
        public bool NoWrap { get; set; }
    }
}