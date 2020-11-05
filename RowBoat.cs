namespace TheHarbour
{
    class RowBoat : Boat
    {
        public int MaxAmountOfPassengers = Utils.RandomNumber(1, 7);

        public RowBoat()
        {
            ID = "R-" + RandomChar();
            Weight = Utils.RandomNumber(100, 300 + 1);
            MaxSpeed = Utils.RandomNumber(1, 4);
            Unique = MaxAmountOfPassengers;
            DaysTilDeparture = 1;
            BoatType = "Rowingboat";
            UniqueProperty = $"Max amount of passengers: {MaxAmountOfPassengers}";
            DockSpot = 0;
        }
        public override string WriteBoat()
        {
            return $"{DockSpot}\t\t{BoatType}\t\t{ID}\t\t{Weight} kg\t\t{Utils.KnotsToKmh(MaxSpeed)}" +
                $" Km/h\t\t{UniqueProperty}";
        }
    }
}
