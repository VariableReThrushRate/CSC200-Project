namespace Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
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
