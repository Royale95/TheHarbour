using System;
using System.Collections.Generic;
using System.Text;

namespace TheHarbour
{
    class Catamaran : Boat
    {
        public int beds => Utils.RandomNumber(10, 61);

        public Catamaran()
        {
            ID = "C-" + RandomChar();
            Weight = Utils.RandomNumber(1200, 8000 + 1);
            MaxSpeed = Utils.RandomNumber(1, 13);
            Unique = beds;
            DaysTilDeparture = 3;
            BoatType = "Catamaran";
            UniqueProperty = $"Amount of beds: {beds}";
            DockSpot = 0;
        }
        public override string WriteBoat()
        {
            return $"{DockSpot}-{DockSpot + 2}\t\t{BoatType}\t\t{ID}\t\t{Weight}" +
                $" kg\t\t{Utils.KnotsToKmh(MaxSpeed)} Km/h\t\t{UniqueProperty}";
        }
    }
}
