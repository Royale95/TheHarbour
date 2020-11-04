using System;
using System.Threading;

namespace TheHarbour
{
    public static class Utils
    {
        public static int RandomNumber(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max);
        }
        public static int FootToMeters(int foot) //Konvertera till meter
        {
            return (int)(foot * 0.3048);
        }
        public static double KnotsToKmh(double båt) //Konvertera till km/h
        {
            return Math.Round(båt * 1.852);
        }
        public static void Announce() // Välkomst och lite info
        {
            Console.WriteLine("----------[Welcome to the Harbour!]----------" +
                "\nThe loop will go on until you press any button" +
                "\nFurther instruction will appear" + "\n[Enjoy]");
            Thread.Sleep(5000);
        }

    }
}
