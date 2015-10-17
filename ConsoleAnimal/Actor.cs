using System;
using System.Collections.Generic;
using AnimalEnvironmentItems;
using AnimalEnvironmentItems.Actions;
using AnimalEnvironmentItems.Foods;
using AnimalLib;
using AnimalLib.Evaluators;
using AnimalLib.EventsReacting;
using AnimalLib.State;
using FoodActionsLib;
using FoodActionsLib.Functions;
using Action = AnimalEnvironmentItems.Actions.Action;

namespace Actor
{
    public class Actor
    {
        public IFoodEvaluator GetFoodEvaluator()
        {
            return new FoodEvaluator(new Dictionary<FoodItem, FoodReaction>
            {
                {FoodItem.Apple, new FoodReaction(3, 1, 3, 1, 3, 1)},
                {FoodItem.Pear, new FoodReaction(2, 1, 4, 1, 3, 1)},
                {FoodItem.Meat, new FoodReaction(4, 2, 5, 2, 4, 1)},
                {FoodItem.Grass, new FoodReaction(1, 2, 3, 2, 2, 1)},
                {FoodItem.Carrot, new FoodReaction(3, 2, 3, 1, 3, 1)}
            });
        }

        public IActionsEvaluator GetActionsEvaluator()
        {
            return new ActionsEvaluator(new Dictionary<ActionItem, ActionReaction>
            {
                {ActionItem.Heat, new ActionReaction(1, 2, 0, 2, 0, 0)},
                {ActionItem.Snow, new ActionReaction(2, 1, 1, 0, 0, 0)},
                {ActionItem.Sun, new ActionReaction(3, 0, 2, 1, 0, 0)},
                {ActionItem.Rain, new ActionReaction(1, 2, 1, 2, 0, 0)},
                {ActionItem.Wind, new ActionReaction(1, 3, 0, 2, 0, 0)}
            });
        }

        public Animal GetAnimal(InternalState internalState, IFoodEvaluator foodEvaluator,
            IActionsEvaluator actionsEvaluator)
        {
            return new Animal(internalState, foodEvaluator, actionsEvaluator);
        }

        public EnvironmentArea GetEnvironmentArea(IFoodFunction foodFunction, IActionFunction actionFunction)
        {
            return new EnvironmentArea(actionFunction, foodFunction);
        }

        public InternalState GetInternalState()
        {
            return new InternalState(new TimeSpan(0, 0, 12), new TimeSpan(0, 0, 9));
        }

        public IFoodFunction GetFoodFunction()
        {
            return new FoodFunction(new List<EventTime<Food>>
            {
                new EventTime<Food>(new Food(FoodType.Valuable, FoodItem.Apple, 3), new TimeSpan(0, 0, 2)),  
                new EventTime<Food>(new Food(FoodType.Neutral, FoodItem.Pear, 2), new TimeSpan(0, 0, 7)),  
                new EventTime<Food>(new Food(FoodType.TastyButUseless, FoodItem.Carrot, 4), new TimeSpan(0, 0, 5)),  
                new EventTime<Food>(new Food(FoodType.Valuable, FoodItem.Meat, 3), new TimeSpan(0, 0, 3)),  
                new EventTime<Food>(new Food(FoodType.Valuable, FoodItem.Pear, 1), new TimeSpan(0, 0, 9)),  
                new EventTime<Food>(new Food(FoodType.Neutral, FoodItem.Grass, 4), new TimeSpan(0, 0, 4)),  
                new EventTime<Food>(new Food(FoodType.TastyButUseless, FoodItem.Apple, 2), new TimeSpan(0, 0, 6)),  
                new EventTime<Food>(new Food(FoodType.Harmful, FoodItem.Meat, 4), new TimeSpan(0, 0, 8))  });
        }

        public IActionFunction GetActionFunction()
        {
            return new ActionFunction(new List<EventTime<Action>>
            {
                new EventTime<Action>(new Action(ActionItem.Heat, ActionType.Harmful, new TimeSpan(0, 0, 2)),
                    new TimeSpan(0, 0, 5)),
                new EventTime<Action>(new Action(ActionItem.Snow, ActionType.Fine, new TimeSpan(0, 0, 1)),
                    new TimeSpan(0, 0, 3)),
                new EventTime<Action>(new Action(ActionItem.Sun, ActionType.Fine, new TimeSpan(0, 0, 5)),
                    new TimeSpan(0, 0, 2)),
                new EventTime<Action>(new Action(ActionItem.Rain, ActionType.Harmful, new TimeSpan(0, 0, 3)),
                    new TimeSpan(0, 0, 7)),
                new EventTime<Action>(new Action(ActionItem.Wind, ActionType.Neutral, new TimeSpan(0, 0, 1)),
                    new TimeSpan(0, 0, 11)),
                new EventTime<Action>(new Action(ActionItem.Heat, ActionType.Neutral, new TimeSpan(0, 0, 3)),
                    new TimeSpan(0, 0, 6)),
                new EventTime<Action>(new Action(ActionItem.Snow, ActionType.Harmful, new TimeSpan(0, 0, 1)),
                    new TimeSpan(0, 0, 8)),
                new EventTime<Action>(new Action(ActionItem.Sun, ActionType.Neutral, new TimeSpan(0, 0, 3)),
                    new TimeSpan(0, 0, 10)),
                new EventTime<Action>(new Action(ActionItem.Rain, ActionType.Sad, new TimeSpan(0, 0, 2)),
                    new TimeSpan(0, 0, 4)),
                new EventTime<Action>(new Action(ActionItem.Wind, ActionType.Fine, new TimeSpan(0, 0, 10)),
                    new TimeSpan(0, 0, 9))
            });
        }
    }
}