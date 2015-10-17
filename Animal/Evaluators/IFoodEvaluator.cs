using System.Collections.Generic;
using AnimalEnvironmentItems.Foods;
using AnimalLib.EventsReacting;
using AnimalLib.State;

namespace AnimalLib.Evaluators
{
    public interface IFoodEvaluator
    {
        IDictionary<FoodItem, FoodReaction> FoodReactions { get; }
        StateDelta Evaluate(Food food);
        int EvaluateSatiety(Food food);
        int EvaluateHappiness(Food food);
        int EvaluateHealth(Food food);
    }
}