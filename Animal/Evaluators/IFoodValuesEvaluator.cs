using System.Collections.Generic;
using AnimalEnvironmentItems.Foods;
using AnimalLib.FoodReacting;

namespace AnimalLib.Evaluators
{
    public interface IFoodValuesEvaluator
    {
        IDictionary<FoodItem, FoodDigestion> FoodValuesEvaluations { get; }
        int EvaluateEnergyPlus(Food food);
        int EvaluateEnergyMinus(Food food);
    }
}