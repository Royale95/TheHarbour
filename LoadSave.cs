using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TheHarbour
{
    class LoadSave
    {
        private const string file = "Harbour.txt";
        public static void SaveData(Harbour[] harbour)
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
        public static void LoadData(Harbour[] harbour)
        {
            foreach (var boat in File.ReadLines(file))
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
                        case 'L': 
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
            Harbour.PrintBoats();
        }
        private static void LoadBoat(Boat b, Harbour[] harbourSlots)
        {
            for (int i = 0; i < harbourSlots.Length; i++)
            {
                if (b is RowBoat)
                {
                    if (b.DockSpot == harbourSlots[i].DockNumber && harbourSlots[i].FreeDockSpace is true)
                    {
                        harbourSlots[i].DockSpot[0] = b;
                        harbourSlots[i].FreeDockSpace = false;
                        break;
                    }
                    if (harbourSlots[i].DockSpot[0] is RowBoat && harbourSlots[i].DockSpot[1] is null
                        && harbourSlots[i].DockSpot[0].ID != b.ID)
                    {
                        harbourSlots[i].DockSpot[1] = b;
                        break;
                    }
                }
                if (b is MotorBoat)
                {
                    if (b.DockSpot == harbourSlots[i].DockNumber && harbourSlots[i].FreeDockSpace is true)
                    {
                        harbourSlots[i].DockSpot[0] = b;
                        harbourSlots[i].FreeDockSpace = false;
                        break;
                    }
                }
                if (b is SailBoat)
                {
                    if (b.DockSpot == harbourSlots[i].DockNumber)
                    {
                        harbourSlots[i].DockSpot[0] = b;
                        harbourSlots[i].FreeDockSpace = false;
                        harbourSlots[i + 1].FreeDockSpace = false;
                        break;
                    }
                }
                if (b is CargoShip)
                {
                    if (b.DockSpot == harbourSlots[i].DockNumber)
                    {
                        harbourSlots[i].DockSpot[0] = b;
                        harbourSlots[i].FreeDockSpace = false;
                        harbourSlots[i + 1].FreeDockSpace = false;
                        harbourSlots[i + 2].FreeDockSpace = false;
                        harbourSlots[i + 3].FreeDockSpace = false;
                        break;
                    }
                }
                if (b is Catamaran)
                {
                    if (b.DockSpot == harbourSlots[i].DockNumber)
                    {
                        harbourSlots[i].DockSpot[0] = b;
                        harbourSlots[i].FreeDockSpace = false;
                        harbourSlots[i + 1].FreeDockSpace = false;
                        harbourSlots[i + 2].FreeDockSpace = false;
                        break;
                    }
                }
            }


        }
    }
}
