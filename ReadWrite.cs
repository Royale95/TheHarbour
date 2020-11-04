using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TheHarbour
{
    class ReadWrite
    {
        private const string file = "Harbour.txt";
        public static void SaveData(HarbourSlots[] harbour)
        {
            StreamWriter sw = new StreamWriter(file, false);
            foreach (var h in harbour)
            {
                if (h.DockSpot[0] != null)
                {
                    sw.WriteLine($"{h.DockSpot[0].ID};{h.DockSpot[0].Weight};{h.DockSpot[0].DaysTilDeparture};{h.DockSpot[0].DockSpot};" +
                        $"{h.DockSpot[0].BoatType};{h.DockSpot[0].MaxSpeed};{h.DockSpot[0].UniqueProperty}");
                }
                if (h.DockSpot[1] != null)
                {
                    sw.WriteLine($"{h.DockSpot[1].ID};{h.DockSpot[1].Weight};{h.DockSpot[1].DaysTilDeparture};{h.DockSpot[1].DockSpot};" +
                        $"{h.DockSpot[1].BoatType};{h.DockSpot[1].MaxSpeed};{h.DockSpot[1].UniqueProperty}");
                }
            }
            sw.Close();
        }
        public static void Load(HarbourSlots[] harbour)
        {
            foreach (var boat in File.ReadLines(file, Encoding.UTF8))
            {
                string[] boatData = boat.Split(';');

                for (int i = 0; i < boatData.Length; i++)
                {
                    switch (boatData[0].First())
                    {
                        case 'R':
                            RowBoat r = new RowBoat
                            {
                                ID = boatData[0],
                                Weight = int.Parse(boatData[1]),
                                DaysTilDeparture = int.Parse(boatData[2]),
                                DockSpot = int.Parse(boatData[3]),
                                BoatType = boatData[4],
                                MaxSpeed = int.Parse(boatData[5]),
                                UniqueProperty = boatData[6]
                            };
                            LoadBoat(r, harbour);
                            break;
                        case 'M':
                            MotorBoat m = new MotorBoat
                            {
                                ID = boatData[0],
                                Weight = int.Parse(boatData[1]),
                                DaysTilDeparture = int.Parse(boatData[2]),
                                DockSpot = int.Parse(boatData[3]),
                                BoatType = boatData[4],
                                MaxSpeed = int.Parse(boatData[5]),
                                UniqueProperty = boatData[6]
                            };
                            LoadBoat(m, harbour);
                            break;
                        case 'S':
                            SailBoat s = new SailBoat
                            {
                                ID = boatData[0],
                                Weight = int.Parse(boatData[1]),
                                DaysTilDeparture = int.Parse(boatData[2]),
                                DockSpot = int.Parse(boatData[3]),
                                BoatType = boatData[4],
                                MaxSpeed = int.Parse(boatData[5]),
                                UniqueProperty = boatData[6]
                            };
                            LoadBoat(s, harbour);
                            break;
                        case 'L': // 'L' för Lastfartyg istället för 'C' då Catamaran behöver 'C'
                            CargoShip l = new CargoShip
                            {
                                ID = boatData[0],
                                Weight = int.Parse(boatData[1]),
                                DaysTilDeparture = int.Parse(boatData[2]),
                                DockSpot = int.Parse(boatData[3]),
                                BoatType = boatData[4],
                                MaxSpeed = int.Parse(boatData[5]),
                                UniqueProperty = boatData[6]
                            };
                            LoadBoat(l, harbour);
                            break;
                        case 'C':
                            Catamaran c = new Catamaran
                            {
                                ID = boatData[0],
                                Weight = int.Parse(boatData[1]),
                                DaysTilDeparture = int.Parse(boatData[2]),
                                DockSpot = int.Parse(boatData[3]),
                                BoatType = boatData[4],
                                MaxSpeed = int.Parse(boatData[5]),
                                UniqueProperty = boatData[6]
                            };
                            LoadBoat(c, harbour);
                            break;
                        default:
                            break;
                    }
                }
            }
            HarbourSlots.PrintBoats();
        }
        private static void LoadBoat(Boat f, HarbourSlots[] hamnplatser)
        {
            for (int i = 0; i < hamnplatser.Length; i++)
            {
                if (f is RowBoat)
                {
                    if (f.DockSpot == hamnplatser[i].DockNumber && hamnplatser[i].FreeDockSpace == true)
                    {
                        hamnplatser[i].DockSpot[0] = f;
                        hamnplatser[i].FreeDockSpace = false;
                        break;
                    }
                    if (hamnplatser[i].DockSpot[0] is RowBoat && hamnplatser[i].DockSpot[1] is null && hamnplatser[i].DockSpot[0].ID != f.ID)
                    {
                        hamnplatser[i].DockSpot[1] = f;
                        break;
                    }
                }
                if (f is MotorBoat)
                {
                    if (f.DockSpot == hamnplatser[i].DockNumber && hamnplatser[i].FreeDockSpace == true)
                    {
                        hamnplatser[i].DockSpot[0] = f;
                        hamnplatser[i].FreeDockSpace = false;
                        break;
                    }
                }
                if (f is SailBoat)
                {
                    if (f.DockSpot == hamnplatser[i].DockNumber)
                    {
                        hamnplatser[i].DockSpot[0] = f;
                        hamnplatser[i].FreeDockSpace = false;
                        hamnplatser[i + 1].FreeDockSpace = false;
                        break;
                    }
                }
                if (f is CargoShip)
                {
                    if (f.DockSpot == hamnplatser[i].DockNumber)
                    {
                        hamnplatser[i].DockSpot[0] = f;
                        hamnplatser[i].FreeDockSpace = false;
                        hamnplatser[i + 1].FreeDockSpace = false;
                        hamnplatser[i + 2].FreeDockSpace = false;
                        hamnplatser[i + 3].FreeDockSpace = false;
                        break;
                    }
                }
                if (f is Catamaran)
                {
                    if (f.DockSpot == hamnplatser[i].DockNumber)
                    {
                        hamnplatser[i].DockSpot[0] = f;
                        hamnplatser[i].FreeDockSpace = false;
                        hamnplatser[i + 1].FreeDockSpace = false;
                        hamnplatser[i + 2].FreeDockSpace = false;
                        break;
                    }
                }
            }


        }
    }
}
