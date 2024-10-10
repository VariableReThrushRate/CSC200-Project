namespace Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
    public class Airport
    {
        public string Name;
        //IE, Seattle International is SEA. We can google these for our data.
        public string callsign;
        private CLatLng coords;
        public Airport(string name, string callsign, double latitude, double longitude)
        {
            Name = name;
            this.callsign = callsign;
            this.coords = new CLatLng(latitude, longitude);
        }
        public Airport(string name, string callsign, CLatLng coords)
        {
            Name = name;
            this.callsign = callsign;
            this.coords = coords;
        }
        public CLatLng getCoords() { return coords; }
        //Functionality to impliment:
        //Way to add flights
        //Way to remove flights
        //Maybe track capacity? We can use arbitrary values for that. IE How many 727s we have or some shit idk
    }
    public class Aircraft
    {
        //A fine selection of aircraft to choose from. When constructing, I'll make it so that it assigns how much fuel is left per plane.
        private float fuelLeft; //Expresss in precent, IE 0.80
        private float range;
        private int capacity;

        private enum ptype
        {
            AirbusA220,
            AirbusA300,
            AirbusA380,
            AirbusA310,
            Boeing737,
            Boeing777,
            Boeing747,
        }

    }

    public class CLatLng
    {
        public double Lat { get; private set; }
        public double Lng { get; private set; }
        public double dist { get; private set; }


        public CLatLng(double lat, double lng, double dist)
        {
            this.Lat = lat;
            this.Lng = lng;
            this.dist = dist;
        }
        public CLatLng(double lat, double lng)
        {
            this.Lat = lat;
            this.Lng = lng;
            this.dist = 1;
        }
    }
}
