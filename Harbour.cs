using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace TheHarbour
{
    class Harbour
    {
        public static Harbour[] DockingSpots = new Harbour[64];
        public Boat[] DockSpot { get; set; } = new Boat[2];
        public int DockNumber { get; set; }
        public bool FreeDockSpace = true;
        static List<Boat> RejectedBoats = new List<Boat>();
        static int dailyAmountOfBoats = 5;
        static bool isRunning = true;
        static int dayCounter = 0;
        public static void CreateDock()
        {
            for (int i = 0; i < DockingSpots.Length; i++)
            {
                if (DockingSpots[i] is null)
                {
                    DockingSpots[i] = new Harbour();
                    DockingSpots[i].DockNumber = i + 1;
                    DockingSpots[i].FreeDockSpace = true;
                }
            }
        }
        public static void ToDock(List<Boat> boats)
        {
            ExtractBoat();

            foreach (var boat in boats)
            {
                InsertBoat(boat);
            }
            boats.Clear();

            PrintBoats();
            LoadSave.SaveData(DockingSpots);
        }
        private static void InsertBoat(Boat boat)
        {
            bool rejected = true;
            for (int i = 0; i < DockingSpots.Length; i++)
            {
                if (boat is RowBoat)
                {
                    if (DockingSpots[i].FreeDockSpace == true && DockingSpots[i].DockSpot[0] is null)
                    {
                        DockingSpots[i].DockSpot[0] = boat;
                        boat.DockSpot = DockingSpots[i].DockNumber;
                        DockingSpots[i].FreeDockSpace = false;
                        rejected = false;
                        break;
                    }
                    if (DockingSpots[i].DockSpot[0] is RowBoat && DockingSpots[i].DockSpot[1] is null)
                    {
                        DockingSpots[i].DockSpot[1] = boat;
                        boat.DockSpot = DockingSpots[i].DockNumber;
                        rejected = false;
                        break;
                    }
                }
                if (boat is SailBoat)
                {
                    if (i < DockingSpots.Length - 1)
                    {
                        if (DockingSpots[i].FreeDockSpace == true && DockingSpots[i].DockSpot[0] is null
                            && DockingSpots[i + 1].DockSpot[0] is null)
                        {
                            DockingSpots[i].DockSpot[0] = boat;
                            boat.DockSpot = DockingSpots[i].DockNumber;
                            DockingSpots[i].FreeDockSpace = false;
                            DockingSpots[i + 1].FreeDockSpace = false;
                            rejected = false;
                            break;
                        }
                    }
                }
                if (boat is MotorBoat)
                {
                    if (DockingSpots[i].FreeDockSpace == true && DockingSpots[i].DockSpot[0] is null)
                    {
                        DockingSpots[i].DockSpot[0] = boat;
                        boat.DockSpot = DockingSpots[i].DockNumber;
                        DockingSpots[i].FreeDockSpace = false;
                        rejected = false;
                        break;
                    }
                }
                if (boat is CargoShip)
                {
                    if (i < DockingSpots.Length - 3)
                    {

                        if (DockingSpots[i].FreeDockSpace == true && DockingSpots[i].DockSpot[0] is null
                            && DockingSpots[i + 1].DockSpot[0] is null && DockingSpots[i + 2].DockSpot[0]
                            is null && DockingSpots[i + 3].DockSpot[0] is null)
                        {
                            DockingSpots[i].DockSpot[0] = boat;
                            boat.DockSpot = DockingSpots[i].DockNumber;
                            DockingSpots[i].FreeDockSpace = false;
                            DockingSpots[i + 1].FreeDockSpace = false;
                            DockingSpots[i + 2].FreeDockSpace = false;
                            DockingSpots[i + 3].FreeDockSpace = false;
                            rejected = false;
                            break;
                        }
                    }
                }
                if (boat is Catamaran)
                {
                    if (i < DockingSpots.Length - 2)
                    {
                        if (DockingSpots[i].FreeDockSpace == true && DockingSpots[i].DockSpot[0] is null && DockingSpots[i + 1].DockSpot[0] is null && DockingSpots[i + 2].DockSpot[0] is null)
                        {
                            DockingSpots[i].DockSpot[0] = boat;
                            boat.DockSpot = DockingSpots[i].DockNumber;
                            DockingSpots[i].FreeDockSpace = false;
                            DockingSpots[i + 1].FreeDockSpace = false;
                            DockingSpots[i + 2].FreeDockSpace = false;
                            rejected = false;
                            break;
                        }
                    }
                }
            }
            if (rejected)
            {
                RejectedBoats.Add(boat);
            }
        }
        private static void ExtractBoat()
        {
            for (int i = 0; i < DockingSpots.Length; i++)
            {
                if (DockingSpots[i].DockSpot[0] is CargoShip)
                {

                    DockingSpots[i].DockSpot[0].DaysTilDeparture--;
                    if (DockingSpots[i].DockSpot[0].DaysTilDeparture == 0)
                    {
                        DockingSpots[i].DockSpot[0] = null;
                        DockingSpots[i].FreeDockSpace = true;
                        DockingSpots[i + 1].FreeDockSpace = true;
                        DockingSpots[i + 2].FreeDockSpace = true;
                        DockingSpots[i + 3].FreeDockSpace = true;
                    }
                }
                if (DockingSpots[i].DockSpot[0] is SailBoat)
                {
                    DockingSpots[i].DockSpot[0].DaysTilDeparture--;
                    if (DockingSpots[i].DockSpot[0].DaysTilDeparture == 0)
                    {
                        DockingSpots[i].DockSpot[0] = null;
                        DockingSpots[i].FreeDockSpace = true;
                        DockingSpots[i + 1].FreeDockSpace = true;
                    }
                }
                if (DockingSpots[i].DockSpot[0] is MotorBoat)
                {
                    DockingSpots[i].DockSpot[0].DaysTilDeparture--;
                    if (DockingSpots[i].DockSpot[0].DaysTilDeparture == 0)
                    {
                        DockingSpots[i].DockSpot[0] = null;
                        DockingSpots[i].FreeDockSpace = true;
                    }

                }
                if (DockingSpots[i].DockSpot[0] is Catamaran)
                {

                    DockingSpots[i].DockSpot[0].DaysTilDeparture--;
                    if (DockingSpots[i].DockSpot[0].DaysTilDeparture == 0)
                    {
                        DockingSpots[i].DockSpot[0] = null;
                        DockingSpots[i].FreeDockSpace = true;
                        DockingSpots[i + 1].FreeDockSpace = true;
                        DockingSpots[i + 2].FreeDockSpace = true;
                    }
                }
                if (DockingSpots[i].DockSpot[0] is RowBoat)
                {
                    DockingSpots[i].DockSpot[0].DaysTilDeparture--;
                    if (DockingSpots[i].DockSpot[0].DaysTilDeparture == 0)
                    {
                        DockingSpots[i].DockSpot[0] = null;
                        DockingSpots[i].FreeDockSpace = true;
                    }

                }
                if (DockingSpots[i].DockSpot[1] is RowBoat)
                {
                    DockingSpots[i].DockSpot[1].DaysTilDeparture--;
                    if (DockingSpots[i].DockSpot[1].DaysTilDeparture == 0)
                    {
                        DockingSpots[i].DockSpot[1] = null;
                        DockingSpots[i].FreeDockSpace = true;
                    }

                }
            }

        }
        public static void PrintBoats()
        {
            List<Boat> listOfBoats = new List<Boat>();
            Console.Clear();
            Console.WriteLine($"***********************************************************[DAY {dayCounter}]***********************************************************");
            Console.WriteLine($"\t\t\t\t\t\t\t\t\t\t\t\t\t    [Daily boats: {dailyAmountOfBoats}]");
            Console.WriteLine($"Dock number\tType of boat\t\tID\t\tWeight  \tMax speed  \tMiscellaneous");
            Console.WriteLine("[---------------------------------------------------------------------------------------------------------------------------]");

            for (int i = 0; i < DockingSpots.Length; i++) // kollar om det finns båt på plats eller om det är tomt
            {
                if (DockingSpots[i].FreeDockSpace is true)
                    Console.WriteLine($"{DockingSpots[i].DockNumber} [EMPTY]");
                if (DockingSpots[i].FreeDockSpace is false)
                {
                    if (DockingSpots[i].DockSpot[0] != null)
                        Console.WriteLine($"{DockingSpots[i].DockSpot[0].WriteBoat()}");
                    if (DockingSpots[i].DockSpot[1] != null)
                        Console.WriteLine($"{DockingSpots[i].DockSpot[1].WriteBoat()}");
                }
            }
            for (int i = 0; i < DockingSpots.Length; i++)
            {
                if (DockingSpots[i].DockSpot[0] != null)
                {
                    listOfBoats.Add(DockingSpots[i].DockSpot[0]);
                }
                if (DockingSpots[i].DockSpot[1] != null)
                {
                    listOfBoats.Add(DockingSpots[i].DockSpot[1]);
                }
            }
            var aTotalWeight = listOfBoats
                .Where(b => b != null)
                .Select(b => b.Weight)
                .Sum();
            var aAverageSpeed = listOfBoats
                .Where(b => b != null)
                .Select(b => b.MaxSpeed)
                .Average();
            var aBoats = listOfBoats
                .Where(b => b != null)
                .GroupBy(b => b.BoatType);
            var aFreeSpaces = DockingSpots
                .Where(b => b.FreeDockSpace == true);
            var aOccupied = DockingSpots
                .Where(b => b.FreeDockSpace == false);

            Console.WriteLine("[------------------------------------------------------------------------------------------------------------------------------------]");
            Console.WriteLine($"Total weight: {aTotalWeight} || Average speed: {Utils.KnotsToKmh(aAverageSpeed)} km/h  || Rejected Boats: {RejectedBoats.Count}" +
                $"|| Free spaces: {aFreeSpaces.Count()} of 64  || Occupied spots: {aOccupied.Count()} of 64");
            Console.WriteLine("[--------------------------------------------]");

            Console.Write("Boats in harbour: ");
            foreach (var item in aBoats)
            {
                Console.WriteLine($"{item.Key}: {item.Count()} ");
            }
            Console.WriteLine("[--------------------------------------------]");
            dayCounter++;
        }
        public static void IncomingBoats()
        {
            List<Boat> boatsToHarbour = new List<Boat>();

            while (isRunning)
            {
                MiniMenu();

                for (int i = 0; i < dailyAmountOfBoats; i++) // genererar x antal random typ av båtar för en dag 
                {
                    int rndBoat = Utils.RandomNumber(1, 5 + 1);

                    switch (rndBoat)
                    {
                        case 1:
                            boatsToHarbour.Add(new RowBoat());
                            break;
                        case 2:
                            boatsToHarbour.Add(new MotorBoat());
                            break;
                        case 3:
                            boatsToHarbour.Add(new SailBoat());
                            break;
                        case 4:
                            boatsToHarbour.Add(new CargoShip());
                            break;
                        case 5:
                            boatsToHarbour.Add(new Catamaran());
                            break;
                    }
                }
                ToDock(boatsToHarbour);
                Thread.Sleep(5000);
            }
        }
        public static void MiniMenu()
        {
            while (Console.KeyAvailable)
            {
                Console.WriteLine("-------------------------------------------------------------------------------");
                Console.WriteLine("\n" + "[Instructions]" + "\n[Arrow Up] = Add + 1 to daily incoming boats" +
                    "\n[Arrow Down] = Remove - 1 to daily incoming boats" +
                    "\n[ENTER] = Input any amount" + "\n[ESC] = Quits loop and program");
                Console.ReadKey(true);
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        dailyAmountOfBoats++;  // Lägger till en båt till i dagliga inkomna båtar
                        Console.WriteLine("Boat added");
                        Thread.Sleep(2000);
                        break;
                    case ConsoleKey.DownArrow:
                        dailyAmountOfBoats--; // -.--.- samma fast tar bort en båt
                        Console.WriteLine("Boat removed");
                        Thread.Sleep(2000);
                        break;
                    case ConsoleKey.Enter:
                        try
                        {
                            Console.Write("Input amount of boats: ");
                            dailyAmountOfBoats = int.Parse(Console.ReadLine());

                        }
                        catch (Exception)
                        {
                            Console.WriteLine("ERROR! Please use an integer" + $"\nReverted back to {dailyAmountOfBoats} daily boats");
                            Thread.Sleep(3000);
                        }
                        Console.WriteLine($"Daily incoming boats: {dailyAmountOfBoats}");
                        Thread.Sleep(2000);
                        break;
                    case ConsoleKey.Escape:
                        isRunning = false; // Loopar en sista gång sen avslutas loopen och programmet
                        Console.WriteLine("Quitting Program...");
                        Thread.Sleep(1000);
                        break;
                    default:
                        break;
                }


            }

        }
    }
}