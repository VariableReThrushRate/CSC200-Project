using System.Diagnostics;
using System.Linq.Expressions;
using System.Numerics;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

namespace Project
{
    internal class Program
    {
        public static List<Airport> airports = new List<Airport>();
        public static List<Aircraft> planes = new List<Aircraft>();
        public static List<Flight> flights = new List<Flight>();
        static void Main(string[] args)
        {
            //Gonna start writing the UI. No clue when this will be done.
            Console.WriteLine("Please select the method you'd like to run:");
            Console.WriteLine("1. Basic Initialization of Data");
            
            while (true)
            {
                string brug = Console.ReadLine();
                try
                {
                    int sel = Convert.ToInt32(brug);
                    if (sel >= 1 && sel <= 1) // Update that value whenever you add a method:
                    {
                        switch (sel)
                        {
                            case 1:
                                Initialize();
                                break;
                            default:
                                Console.WriteLine("How did you get here???");
                                break;
                        }
                        break;
                    }
                    else
                    {
                        throw new InvalidSelectionException();
                    }
                }
                catch
                {
                    Console.Write("That did not work. Please try again : ");
                    //brug = Console.ReadLine();
                }
            }   
        }
        public static void Initialize()
        {
            airports.Add(new Airport("Seatac", "SEA", 47.448355745344145, -122.30849428001085));
            airports.Add(new Airport("Las Vegas", "LAS", 36.08, -115.152222));
            foreach (Airport airport in airports) 
            {
                Console.WriteLine(airport);
            }
            planes.Add(new Aircraft(Ptype.AirbusA220));
            flights.Add(new Flight(planes[0], airports.Find(airport => airport.Callsign == "SEA"), airports.Find(airport => airport.Callsign == "LAS")));

            
        }
    }
    public class Airport
    {
        public string Name { get; private set; }
        //IE, Seattle International is SEA. We can google these for our data.
        public string Callsign { get; }
        public CLatLng Coords { get; private set; }
        public Airport(string name, string callsign, double latitude, double longitude)
        {
            Name = name;
            this.Callsign = callsign;
            this.Coords = new CLatLng(latitude, longitude);
        }
        public Airport(string name, string callsign, CLatLng coords)
        {
            Name = name;
            this.Callsign = callsign;
            this.Coords = coords;
        }
        public override string ToString()
        {
            return $"Airport name: {Name}, Callsign: {Callsign}, coordinates: {Coords}";
        }
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
        public float range { get; private set; } // how far it can go
        public int capacity { get; private set; } // amount of humans on board
        public Ptype plane { get; private set; } // the kind of plane
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
            this.Aircraft = aircraft;
            this.Departure = departure;
            this.Arrival = arrival;
        }
        public Aircraft Aircraft {  get; private set; }
        public Airport Departure { get; private set; }
        public Airport Arrival {  get; private set; }
    }
    public class CLatLng
    {
        public double Lat { get; private set; }
        public double Lng { get; private set; }
        public double Dist { get; private set; }


        public CLatLng(double lat, double lng, double dist)
        {
            this.Lat = lat;
            this.Lng = lng;
            this.Dist = dist;
        }
        public CLatLng(double lat, double lng)
        {
            this.Lat = lat;
            this.Lng = lng;
            this.Dist = 1;
        }
        public override string ToString()
        {
            return $"Latitude: {Lat}, Longitude: {Lng}";
        }
    }
    public class InvalidSelectionException : Exception
    {
        // Default constructor
        public InvalidSelectionException() : base("This is the base one")
        {
            Console.WriteLine("");
        }

        // Constructor that takes a custom message
        public InvalidSelectionException(string message) : base(message)
        {
        }

        // Constructor that takes a custom message and inner exception
        public InvalidSelectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
