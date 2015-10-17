namespace AnimalLib.EventsReacting
{
    public class FoodReaction
    {
        public int HappinessPlus { get; private set; }
        public int HappinessMinus { get; private set; }
        public int HealthPlus { get; private set; }
        public int HealthMinus { get; private set; }
        public int SatietyPlus { get; private set; }
        public int SatietyMinus { get; private set; }


        public FoodReaction(int happinessPlus, int happinessMinus, int healthPlus,
            int healthMinus, int satietyPlus, int satietyMinus)
        {
            HappinessPlus = happinessPlus;
            HappinessMinus = happinessMinus;
            HealthPlus = healthPlus;
            HealthMinus = healthMinus;
            SatietyPlus = satietyPlus;
            SatietyMinus = satietyMinus;
        }
    }
}