using System;

namespace ConsoleAnimal
{
    public enum FoodType
    {
        Valuable,
        TastyButUsefull,
        Harmful,
        Killing,
        Neutral
    }

    public class Food
    {
        public Food(int energyValue, FoodType type)
        {
            Type = type;
            EnergyValue = energyValue;
        }

        public FoodType Type { get; private set; }
        public int EnergyValue { get; private set; }
    }
}