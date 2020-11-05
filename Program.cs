﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace TheHarbour
{
    class Program
    {
        static void Main(string[] args)
        {
            Utils.Announce();
            Console.SetWindowSize(140, 60);
            HarbourSlots.CreateDock();
            LoadSave.LoadData(HarbourSlots.DockingSpots);
            HarbourSlots.IncomingBoats();
            Console.Clear();
        }

    }
}