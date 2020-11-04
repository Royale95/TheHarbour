namespace TheHarbour
{
    class CargoShip : Boat
    {
        public int Containers = Utils.RandomNumber(0, 501);

        public CargoShip()
        {
            BoatType = "CargoShip";
            ID = "L-" + RandomString();
            Weight = Utils.RandomNumber(3000, 20000 + 1);
            MaxSpeed = Utils.RandomNumber(1, 21);
            Unique = Containers;
            DaysTilDeparture = 6;
            UniqueProperty = $"Containers: {Containers}";
            DockSpot = 0;

        }
        public override string PrintBoat()
        {
            if (Weight > 10000)
                return $"{DockSpot}-{DockSpot + 3}\t\t{BoatType}\t\t{ID}\t\t{Weight} kg\t{Utils.KnotsToKmh(MaxSpeed)} Km/h\t\t{UniqueProperty}";
            else return $"{DockSpot}-{DockSpot + 3}\t\t{BoatType}\t\t{ID}\t\t{Weight} kg\t\t{Utils.KnotsToKmh(MaxSpeed)} Km/h\t\t{UniqueProperty}";
        }
    }
}
