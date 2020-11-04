namespace TheHarbour
{
    class RowBoat : Boat
    {
        public int MaxAntalPassagerare = Utils.RandomNumber(1, 7);

        public RowBoat()
        {
            ID = "R-" + RandomString();
            Weight = Utils.RandomNumber(100, 300 + 1);
            MaxSpeed = Utils.RandomNumber(1, 4);
            Unique = MaxAntalPassagerare;
            DaysTilDeparture = 1;
            BoatType = "Rowingboat";
            UniqueProperty = $"Max Antal Passagerare: {MaxAntalPassagerare}";
            DockSpot = 0;
        }
        public override string PrintBoat()
        {
            return $"{DockSpot}\t\t{BoatType}\t\t{ID}\t\t{Weight} kg\t\t{Utils.KnotsToKmh(MaxSpeed)} Km/h\t\t{UniqueProperty}";
        }
    }
}
