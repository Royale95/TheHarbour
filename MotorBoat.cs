namespace TheHarbour
{
    class MotorBoat : Boat
    {        
        public int HorsePower = Utils.RandomNumber(10, 1000 + 1);
        public MotorBoat()
        {
            BoatType = "Motorboat";
            ID = "M-" + RandomChar();
            Weight = Utils.RandomNumber(200, 3000 + 1);
            MaxSpeed = Utils.RandomNumber(1, 60 + 1);
            Unique = HorsePower;
            DaysTilDeparture = 3;
            UniqueProperty = $"Horsepower: {HorsePower}";
            DockSpot = 0;

        }
        public override string WriteBoat()
        {
            // Utils.KnotsToKmh(MaxSpeed); // Utanför jämfört med de andra båtarna pga en bugg som gör så att Horsepower blir fel placerad.

            if (MaxSpeed < 10)
                return $"{DockSpot}\t\t{BoatType}\t\t{ID}\t\t{Weight}" + // bugg ingen räkning
                    $" kg\t\t{MaxSpeed} Km/h \t\t{UniqueProperty}";
            else if (MaxSpeed < 150)
                return $"{DockSpot}\t\t{BoatType}\t\t{ID}\t\t{Weight}" +
                    $" kg\t\t{MaxSpeed} Km/h \t{UniqueProperty}";
            else
                return $"{DockSpot}\t\t{BoatType}\t\t{ID}\t\t{Weight}" +
                    $" kg\t\t{MaxSpeed} Km/h \t{UniqueProperty}";
        }

    }
}
