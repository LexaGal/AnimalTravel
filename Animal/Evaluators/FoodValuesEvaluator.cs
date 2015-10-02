using System.Collections.Generic;
using AnimalEnvironmentItems.Foods;
using AnimalLib.FoodReacting;

namespace AnimalLib.Evaluators
{
    public class FoodValuesEvaluator : IFoodValuesEvaluator
    {
        public IDictionary<FoodItem, FoodDigestion> FoodValuesEvaluations { get; private set; }

        public FoodValuesEvaluator(IDictionary<FoodItem, FoodDigestion> dictionary)
        {
            FoodValuesEvaluations = dictionary;
        }

        public int EvaluateEnergyPlus(Food food)
        {
            switch (food.Type)
            {
                case FoodType.Valuable:
                    return FoodValuesEvaluations[food.Item].EnergyPlus*food.N*2;
                case FoodType.TastyButUseless:
                    return FoodValuesEvaluations[food.Item].EnergyPlus*food.N/2;
                case FoodType.Neutral:
                    return FoodValuesEvaluations[food.Item].EnergyPlus*food.N;
                case FoodType.Harmful:
                    return FoodValuesEvaluations[food.Item].EnergyPlus*food.N/2;
                default:
                    return 0;
            }
        }

        public int EvaluateEnergyMinus(Food food)
        {
            switch (food.Type)
            {
                case FoodType.Valuable:
                    return FoodValuesEvaluations[food.Item].EnergyPlus*food.N/2;
                case FoodType.TastyButUseless:
                    return (int) (FoodValuesEvaluations[food.Item].EnergyPlus*food.N/1.5);
                case FoodType.Neutral:
                    return FoodValuesEvaluations[food.Item].EnergyPlus*food.N;
                case FoodType.Harmful:
                    return FoodValuesEvaluations[food.Item].EnergyPlus*food.N*2;
                default:
                    return 0;
            }
        }
    }
}