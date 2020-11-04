namespace TheHarbour
{
    class SailBoat : Boat
    {
        public int Båtlängd = Utils.RandomNumber(10, 61);

        public SailBoat()
        {

            ID = "S-" + RandomString();
            Weight = Utils.RandomNumber(800, 6000 + 1);
            MaxSpeed = Utils.RandomNumber(1, 13);
            Unique = Utils.FootToMeters(Båtlängd);
            DaysTilDeparture = 4;
            BoatType = "Sailboat";
            UniqueProperty = $"Båtlängd: {Båtlängd} meter";
            DockSpot = 0;

        }
        public override string PrintBoat()
        {
            return $"{DockSpot}-{DockSpot + 1}\t\t{BoatType}\t\t{ID}\t\t{Weight} kg\t\t{Utils.KnotsToKmh(MaxSpeed)} Km/h\t\t{UniqueProperty}";
        }
    }
}
