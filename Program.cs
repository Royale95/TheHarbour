using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace TheHarbour
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists("Hamnen.txt") == false)
            {
                FileStream fs1 = File.Create("Hamnen.txt");

                fs1.Close();
            }
            Utils.Announce();
            //Console.SetWindowSize(140, 60);
            Harbour.CreateDock();
            LoadSave.LoadData(Harbour.DockingSpots);
            Harbour.IncomingBoats();
            Console.Clear();
        }

    }
}