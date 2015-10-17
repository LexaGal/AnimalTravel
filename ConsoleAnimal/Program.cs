
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
    public class Program
    {
        private static void Main()
        {
            Actor actor = new Actor();

            IFoodEvaluator foodEvaluator = actor.GetFoodEvaluator();
            IActionsEvaluator actionsEvaluator = actor.GetActionsEvaluator();

            InternalState internalState = actor.GetInternalState();
            Animal animal = actor.GetAnimal(internalState, foodEvaluator, actionsEvaluator);

            IActionFunction actionFunction = actor.GetActionFunction();
            IFoodFunction foodFunction = actor.GetFoodFunction();

            EnvironmentArea environmentArea = actor.GetEnvironmentArea(foodFunction, actionFunction);

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
                    EventReaction reaction = animal.EatFood(food);
                    if (reaction == EventReaction.Alive)
                    {
                        Action action = environmentArea.CreateAction();
                        reaction = animal.ReactForAction(action);
                        if (reaction == EventReaction.Alive)
                        {
                            continue;
                        }
                        animal.Die(DeathReason.ActionReaction);
                        Main();
                    }
                    animal.Die(DeathReason.FoodReaction);
                    Main();
                }

                animal.Die(DeathReason.NoFood);
                Main();
            }
        }
    }
}
