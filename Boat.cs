using System;
using System.Linq;

namespace TheHarbour
{

    class Boat
    {
        public string ID { get; set; }
        public int Weight { get; set; }
        public double MaxSpeed { get; set; }
        public int Unique { get; set; }
        public int DaysTilDeparture { get; set; }
        public string BoatType { get; set; }
        public string UniqueProperty { get; set; }
        public int DockSpot { get; set; }

        static Random rnd = new Random();
        public static string RandomString() //Metod att randomisera ID
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return new string(Enumerable.Repeat(chars, 3)

              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
        public virtual string PrintBoat()
        {
            return "XXXX";
        }
    }

}
