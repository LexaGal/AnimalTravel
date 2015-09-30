using System.Collections.Generic;
using AnimalEnvironmentItems;

namespace AnimalLib
{
    public class FoodValueEvaluator
    {
        public IDictionary<FoodItem, FoodDigestion> Dictionary { get; private set; }

        public FoodValueEvaluator(IDictionary<FoodItem, FoodDigestion> dictionary)
        {
            Dictionary = dictionary;
        }

        public int EvaluateEnergyPlus(Food food)
        {
            switch (food.Type)
            {
                case FoodType.Valuable:
                    return Dictionary[food.Item].EnergyPlus*food.N*2;
                case FoodType.TastyButUseless:
                    return Dictionary[food.Item].EnergyPlus*food.N/2;
                case FoodType.Neutral:
                    return Dictionary[food.Item].EnergyPlus*food.N;
                case FoodType.Harmful:
                    return Dictionary[food.Item].EnergyPlus*food.N/2;
                default:
                    return 0;
            }
        }

        public int EvaluateEnergyMinus(Food food)
        {
            switch (food.Type)
            {
                case FoodType.Valuable:
                    return Dictionary[food.Item].EnergyPlus*food.N/2;
                case FoodType.TastyButUseless:
                    return (int) (Dictionary[food.Item].EnergyPlus*food.N/1.5);
                case FoodType.Neutral:
                    return Dictionary[food.Item].EnergyPlus*food.N;
                case FoodType.Harmful:
                    return Dictionary[food.Item].EnergyPlus*food.N*2;
                default:
                    return 0;
            }
        }
    }
}