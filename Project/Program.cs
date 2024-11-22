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
            // Aesthetic Touches
            // SQL Code for connecting to the server and initializing the three lists would go here.
            // Think of the lists as a table, each row is an instance of the object attached to the list.

            //Gonna start writing the UI. No clue when this will be done.
            //UI TODO: Need function for: Removing flight, adding flight, adding plane, removing plane, etc. add/rempove all
            //Could refactor search function to give us the indext to delete, or just reuse that line of code to make it happen.
            //I'll probably just refactor it, on second thought.
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("Please select the method you'd like to run, or press EEE to exit.:");
                Console.WriteLine("1. Basic Initialization of Data");
                Console.WriteLine("2. Get a specific aircraft's info via its callsign.");
                Console.WriteLine("3. Get info on a specific flight via flight number.");
                Console.WriteLine("4. Get info on a specific Airport via callsign.");
                Console.WriteLine("3. List all aircraft in the fleet.");
                Console.WriteLine("4. Show all airports associated with our fleet.");
                Console.WriteLine("5. List all the flights of our fleet.");
                Console.Write("Insert selection here: "); 

                string brug = Console.ReadLine();
                try
                {
                    //exit line
                    if (brug == "EEE") {
                        break;
                    }
                    int sel = Convert.ToInt32(brug);
                    if (sel >= 1 && sel <= 5) // Update that value whenever you add a method:
                    {
                        switch (sel)
                        {
                            case 1:
                                Initialize();
                                break;
                            case 2:
                                GetAircraftInfo();
                                break;
                            case 3:
                                GetFlightInfo();
                                break;
                            case 4:
                                
                                break;
                            case 5:
                                ListAllAircraft();
                                break;
                            case 6:
                                ListAllAirports();
                                break;
                            case 7:
                                ListAllFlights();
                                break;
                            
                            default:
                                Console.WriteLine("How did you get here???");
                                break;
                        }
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
            airports.Add(new Airport("Seatac", "KSEA", 47.448355745344145, -122.30849428001085));
            airports.Add(new Airport("Las Vegas", "KLAS", 36.08, -115.152222));
            foreach (Airport airport in airports)
            {
                Console.WriteLine(airport);
            }
            planes.Add(new Aircraft("Whiskey Alpha Lima", Ptype.AirbusA220));
            flights.Add(new Flight(planes[0], airports.Find(airport => airport.Callsign == "KSEA"), airports.Find(airport => airport.Callsign == "KLAS")));
            foreach (Flight flight in flights)
            {
                Console.WriteLine(flight);
            }
            foreach (Aircraft plane in planes)
            {
                Console.WriteLine(plane);
            }
        }
        public static void GetAircraftInfo()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please insert the callsign of the aircraft you want to see, or type 'EEE' to leave the function:");
            while (true)
            {
                string brug = Console.ReadLine();
                try
                {
                    if (brug == "EEE")
                    {
                        break;
                    }
                    
                    var found = planes.Find(plane => plane.Callsign == brug);
                    if (found == null) { throw new InvalidSelectionException(); }
                    Console.WriteLine("The aircraft you requested: " + found.ToString());
                    break;
                }
                catch
                {
                    Console.Write("Callsign not found. Please try again, or type 'EEE' to exit.");
                    //brug = Console.ReadLine();
                }
            }
        }
        public static void GetFlightInfo() 
        {
            throw new NotImplementedException();
        }
        public static void GetAirportInfo() 
        {
            throw new NotImplementedException();
        }
        // For loops people. For loops.
        public static void ListAllAircraft()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            foreach (Aircraft plane in planes)
            {
                Console.WriteLine("Displaying all planes:");
                Console.WriteLine(plane);
            }
        }
        public static void ListAllAirports()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            foreach (Airport airport in airports)
            {
                Console.WriteLine("Displaying all airports:");
                Console.WriteLine(airport);
            }
        }
        public static void ListAllFlights()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            foreach (Flight flight in flights)
            {
                Console.WriteLine("Displaying all flight information:");
                Console.WriteLine(flight);
            }
        }
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
            return $"Airport name: {Name}, Callsign: {Callsign}, Coordinates: {Coords}";
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
        public string Callsign { get; private set; }
        //A fine selection of aircraft to choose from. When constructing, I'll make it so that it assigns how much fuel is left per plane.
        public float FuelLeft = 100; //Expresss in precent, IE 0.80
        public float Range { get; private set; } // how far it can go
        public int Capacity { get; private set; } // amount of humans on board
        public Ptype Plane { get; private set; } // the kind of plane
        public Aircraft(string callsign, Ptype plane) 
        {
            this.Callsign = callsign;
            this.Plane = plane;
            switch (plane)
            {
                case Ptype.AirbusA220:
                    //Range is in miles. All other aircraft follow this statistical format.
                    Capacity = 160;
                    Range = 3798.0F;
                    break;
                case Ptype.AirbusA300:
                    Capacity = 345;
                    Range = 4685F;
                    break;
                case Ptype.AirbusA380:
                    Capacity = 525;
                    Range = 9206.236F;
                    break;
                case Ptype.AirbusA310:
                    Capacity = 200;
                    Range = 5002F;
                    break;
                case Ptype.Boeing737:
                    Capacity = 180;
                    Range = 3582F;
                    break;
                case Ptype.Boeing777:
                    Capacity = 300;
                    Range = 9844.3838F;
                    break;
                case Ptype.Boeing747:
                    Capacity = 500;
                    Range = 8357.4F;
                    break;
                default:
                    break;
            }
            
        }
        public override string ToString()
        {
            return $"Aircraft type: {Plane}, Percentage of Fuel Left: {FuelLeft}, Range: {Range}, Capacity: {Capacity}";
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
        public int FlightNum { get; private set; }
        public Aircraft Aircraft {  get; private set; }
        public Airport Departure { get; private set; }
        public Airport Arrival {  get; private set; }
        public override string ToString()
        {
            return $"Arrival Airport: {Arrival}, Departure Airport: {Departure}, Aircraft flying: {Aircraft}";
        }
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

