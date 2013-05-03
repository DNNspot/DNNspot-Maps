namespace DNNspot.Maps.Models.GoogleMaps {
    public class Marker {
        public bool Clickable { get; set; }
        public string Cursor { get; set; }
        public bool Dragable { get; set; }
        public bool Flat { get; set; }
        public string Icon { get; set; }
        public Map Map { get; set; }
        public LatLng Position { get; set; }
        public MarkerShape Shape { get; set; }
        public string Title { get; set; }
        public bool Visible { get; set; }
        public int ZIndex { get; set; }
    }
}