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
            LoadSave.CheckFileExistance();
            Utils.Announce();
            Console.SetWindowSize(140, 60);
            Harbour.CreateDock();
            LoadSave.LoadData(Harbour.DockingSpots);
            Harbour.IncomingBoats();
            Console.Clear();
        }

    }
}