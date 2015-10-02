using System;
using System.Collections.Generic;
using AnimalEnvironmentItems;
using AnimalEnvironmentItems.Actions;
using AnimalEnvironmentItems.Foods;
using AnimalLib;
using AnimalLib.Evaluators;
using AnimalLib.FoodReacting;
using AnimalLib.State;
using FoodActionsLib;
using FoodActionsLib.Functions;
using Action = AnimalEnvironmentItems.Actions.Action;

namespace Actor
{
    public class Actor
    {
        public IFoodValuesEvaluator SetFoodValueEvaluator(IDictionary<FoodItem, FoodDigestion> dictionary)
        {
            return new FoodValuesEvaluator(dictionary);
        }

        public IActionsEvaluator SetActionsEvaluator(IDictionary<ActionType, int> dictionary)
        {
            return new ActionsEvaluator(dictionary);
        }

        public Animal SetAnimal(InternalState internalState, IFoodValuesEvaluator foodValuesEvaluator,
            IActionsEvaluator actionsEvaluator)
        {
            return new Animal(internalState, foodValuesEvaluator, actionsEvaluator);
        }

        public EnvironmentArea SetEnvironmentArea(IFoodFunction foodFunction, IActionFunction actionFunction)
        {
            return new EnvironmentArea(actionFunction, foodFunction);
        }

        public InternalState SetInternalState(TimeSpan remainingLifeTime, TimeSpan remainingLifeTimeLimit)
        {
            return new InternalState(remainingLifeTime, remainingLifeTimeLimit);
        }

        public IFoodFunction SetFoodFunction(IList<EventTime<Food>> foodsTimes)
        {
            return new FoodFunction(foodsTimes);
        }
        
        public IActionFunction SetActionFunction(IList<EventTime<Action>> actionsTimes)
        {
            return new ActionFunction(actionsTimes);
        }
    }
}