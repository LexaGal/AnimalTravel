using System;
using System.Collections.Generic;

namespace ConsoleAnimal
{
    public class FoodValueEvaluator
    {
        public IDictionary<FoodItem, FoodDigestion> FoodDigestions { get; private set; }

        public FoodValueEvaluator(IDictionary<FoodItem, FoodDigestion> foodDigestions)
        {
            FoodDigestions = foodDigestions;
        }

        public int EvaluateEnergyPlus(Food food)
        {
            switch (food.Type)
            {
                case FoodType.Valuable:
                    return FoodDigestions[food.Item].EnergyPlus*food.N*2;
                case FoodType.TastyButUseLess:
                    return FoodDigestions[food.Item].EnergyPlus*food.N/2;
                case FoodType.Neutral:
                    return FoodDigestions[food.Item].EnergyPlus*food.N;
                case FoodType.Harmful:
                    return FoodDigestions[food.Item].EnergyPlus*food.N/2;
                default:
                    return 0;
            }
        }

        public int EvaluateEnergyMinus(Food food)
        {
            switch (food.Type)
            {
                case FoodType.Valuable:
                    return FoodDigestions[food.Item].EnergyPlus*food.N/2;
                case FoodType.TastyButUseLess:
                    return (int) (FoodDigestions[food.Item].EnergyPlus*food.N/1.5);
                case FoodType.Neutral:
                    return FoodDigestions[food.Item].EnergyPlus*food.N;
                case FoodType.Harmful:
                    return FoodDigestions[food.Item].EnergyPlus*food.N*2;
                default:
                    return 0;
            }
        }
    }
}