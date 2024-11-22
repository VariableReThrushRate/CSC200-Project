using System.Linq.Expressions;
using System.Numerics;

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
        private string Name { get; }
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
        //Way to add flights - What did I mean by this? Flights is an instance object, they're gonna be added by instantiation or by the SQL server
        //Way to remove flights - Flightname.Destroy lmao
        //Maybe track capacity? We can use arbitrary values for that. IE How many 727s we have or some carp idk
    }
    //List of applicable planes. Change Aircraft if you need to add more.
    public enum Ptype
    {
        AirbusA220,
        AirbusA300,
        AirbusA380,
        AirbusA310,
        Boeing737,
        Boeing777,
        Boeing747
    }
    public class Aircraft
    {
        //A fine selection of aircraft to choose from. When constructing, I'll make it so that it assigns how much fuel is left per plane.
        public float fuelLeft = 100; //Expresss in precent, IE 0.80
        private float range { get; }
        private int capacity { get; }
        private Ptype plane { get; }
        public Aircraft(Ptype plane) 
        {
            this.plane = plane;
            switch (plane)
            {
                case Ptype.AirbusA220:
                    //Range is in miles. All other aircraft follow this statistical format.
                    capacity = 160;
                    range = 3798.0F;
                    break;
                case Ptype.AirbusA300:

                    break;
                case Ptype.AirbusA380:

                    break;
                case Ptype.AirbusA310:

                    break;
                case Ptype.Boeing737:

                    break;
                case Ptype.Boeing777:

                    break;
                case Ptype.Boeing747:

                    break;
                default:
                    break;
            }

        }

    }
    //There's probably a better way to do this, and if there is, please let me know.
    public class Flight
    {
        public Flight(Aircraft aircraft, Airport departure, Airport arrival)
        {
            this.aircraft = aircraft;
            this.departure = departure;
            this.arrival = arrival;
        }
        private Aircraft aircraft;
        private Airport departure;
        public Airport arrival;
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
