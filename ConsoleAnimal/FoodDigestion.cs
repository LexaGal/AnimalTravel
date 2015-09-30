namespace ConsoleAnimal
{
    public class FoodDigestion
    {
        public int EnergyPlus { get; set; }
        public int EnergyMinus { get; set; }

        public FoodDigestion(int energyPlus, int energyMinus)
        {
            EnergyPlus = energyPlus;
            EnergyMinus = energyMinus;
        }
    }
}