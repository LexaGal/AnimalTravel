using System;
using System.Collections.Generic;
using AnimalEnvironmentItems.Foods;
using AnimalLib.EventsReacting;
using AnimalLib.State;

namespace AnimalLib.Evaluators
{
    public class FoodEvaluator : IFoodEvaluator
    {
        public IDictionary<FoodItem, FoodReaction> FoodReactions { get; private set; }

        public FoodEvaluator(IDictionary<FoodItem, FoodReaction> dictionary)
        {
            FoodReactions = dictionary;
        }

        public StateDelta Evaluate(Food food)
        {
            int satiety = EvaluateSatiety(food);
            int happiness = EvaluateHappiness(food);
            int health = EvaluateHealth(food);
            return new StateDelta(happiness, health, satiety);
        }

        public int EvaluateSatiety(Food food)
        {
            switch (food.Type)
            {
                case FoodType.Valuable:
                    return FoodReactions[food.Item].SatietyPlus*food.N*2 -
                           FoodReactions[food.Item].SatietyMinus*food.N;
                case FoodType.TastyButUseless:
                    return FoodReactions[food.Item].SatietyPlus*food.N/2 - FoodReactions[food.Item].SatietyMinus*food.N;
                case FoodType.Neutral:
                    return FoodReactions[food.Item].SatietyPlus*food.N - FoodReactions[food.Item].SatietyMinus*food.N;
                case FoodType.Harmful:
                    return ((-1)*
                            Math.Abs(FoodReactions[food.Item].SatietyPlus*food.N/2 -
                                     FoodReactions[food.Item].SatietyMinus*food.N*2));
                default:
                    return 0;
            }
        }

        public int EvaluateHappiness(Food food)
        {
            switch (food.Type)
            {
                case FoodType.Valuable:
                    return FoodReactions[food.Item].HappinessPlus*food.N*2 -
                           FoodReactions[food.Item].HappinessMinus*food.N;
                case FoodType.TastyButUseless:
                    return FoodReactions[food.Item].HappinessPlus*food.N*2 -
                           FoodReactions[food.Item].HappinessMinus*food.N;
                case FoodType.Neutral:
                    return FoodReactions[food.Item].HappinessPlus*food.N -
                           FoodReactions[food.Item].HappinessMinus*food.N;
                case FoodType.Harmful:
                    return ((-1)*Math.Abs(FoodReactions[food.Item].HappinessPlus*food.N -
                           FoodReactions[food.Item].HappinessMinus*food.N*2));
                default:
                    return 0;
            }
        }

        public int EvaluateHealth(Food food)
        {
            switch (food.Type)
            {
                case FoodType.Valuable:
                    return FoodReactions[food.Item].HealthPlus*food.N*2 - 
                           FoodReactions[food.Item].HealthMinus*food.N;
                case FoodType.TastyButUseless:
                    return FoodReactions[food.Item].HealthPlus*food.N/2 - 
                           FoodReactions[food.Item].HealthMinus*food.N;
                case FoodType.Neutral:
                    return FoodReactions[food.Item].HealthPlus*food.N -
                           FoodReactions[food.Item].HealthMinus*food.N;
                case FoodType.Harmful:
                    return ((-1)*Math.Abs(FoodReactions[food.Item].HealthPlus*food.N -
                           FoodReactions[food.Item].HealthMinus*food.N*2));
                default:
                    return 0;
            }
        }
    }
}





