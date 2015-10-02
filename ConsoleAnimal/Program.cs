
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
    public class Program
    {
        private static void Main()
        {
            Actor actor = new Actor();

            IFoodValuesEvaluator foodValuesEvaluator =
                actor.SetFoodValueEvaluator(new Dictionary<FoodItem, FoodDigestion>
                {
                    {FoodItem.Apple, new FoodDigestion(3, 1)},
                    {FoodItem.Pear, new FoodDigestion(4, 1)},
                    {FoodItem.Meat, new FoodDigestion(6, 3)},
                    {FoodItem.Grass, new FoodDigestion(3, 2)},
                    {FoodItem.Carrot, new FoodDigestion(4, 2)}
                });

            IActionsEvaluator actionsEvaluator = actor.SetActionsEvaluator(new Dictionary<ActionType, int>
            {
                {ActionType.Rain, -3},
                {ActionType.Snow, -5},
                {ActionType.Sun, -6}
                    
            });

            InternalState internalState = actor.SetInternalState(new TimeSpan(0, 0, 12), new TimeSpan(0, 0, 9));
            Animal animal = actor.SetAnimal(internalState, foodValuesEvaluator, actionsEvaluator);

            IList<EventTime<Action>> actionsTimes = new List<EventTime<Action>>
            {
                new EventTime<Action>(new Action(ActionType.Hot, new TimeSpan(0, 0, 2)), new TimeSpan(0, 0, 6)),  
                new EventTime<Action>(new Action(ActionType.Snow, new TimeSpan(0, 0, 1)), new TimeSpan(0, 0, 15)),  
                new EventTime<Action>(new Action(ActionType.Sun, new TimeSpan(0, 0, 3)), new TimeSpan(0, 0, 3)),  
                new EventTime<Action>(new Action(ActionType.Rain, new TimeSpan(0, 0, 2)), new TimeSpan(0, 0, 12)),  
            };
            IActionFunction actionFunction = new ActionFunction(actionsTimes);
            
            IList<EventTime<Food>> foodsTimes = new List<EventTime<Food>>
            {
                new EventTime<Food>(new Food(FoodType.Valuable, FoodItem.Apple, 3), new TimeSpan(0, 0, 6)),  
                new EventTime<Food>(new Food(FoodType.Neutral, FoodItem.Pear, 2), new TimeSpan(0, 0, 15)),  
                new EventTime<Food>(new Food(FoodType.TastyButUseless, FoodItem.Carrot, 4), new TimeSpan(0, 0, 3)),  
                new EventTime<Food>(new Food(FoodType.Valuable, FoodItem.Meat, 3), new TimeSpan(0, 0, 12)),  
            };
            IFoodFunction foodFunction = new FoodFunction(foodsTimes);

            EnvironmentArea environmentArea = actor.SetEnvironmentArea(foodFunction, actionFunction);

            while (true)
            {
                TimeSpan time = environmentArea.GetTimeBeforeFood();

                if (animal.WillStayAlive(time))
                {
                    if (!animal.IsSatiated)
                    {
                        animal.SearchForFood(time);
                    }
                    else
                    {
                        animal.Sleep();
                    }

                    Food food = environmentArea.CreateFood();
                    FoodReaction reaction = animal.EatFood(food);
                    if (reaction == FoodReaction.Alive)
                    {
                        continue;
                    }

                    animal.Die("FOOD REACTION");
                    internalState = new InternalState(new TimeSpan(0, 0, 12), new TimeSpan(0, 0, 9));
                    animal = actor.SetAnimal(internalState, foodValuesEvaluator, actionsEvaluator);
                    environmentArea = actor.SetEnvironmentArea(foodFunction, actionFunction);
                    continue;
                }

                animal.Die("NO FOOD");
                internalState = new InternalState(new TimeSpan(0, 0, 12), new TimeSpan(0, 0, 9));
                animal = actor.SetAnimal(internalState, foodValuesEvaluator, actionsEvaluator);
                environmentArea = actor.SetEnvironmentArea(foodFunction, actionFunction);
            }
        }
    }
}
