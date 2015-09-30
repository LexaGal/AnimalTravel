using System;
using System.Collections.Generic;
using AnimalEnvironmentItems;
using AnimalLib;
using FoodActionsLib;

namespace Actor
{
    public class Program
    {
        private static void Main()
        {
            Actor actor = new Actor();
            FoodValueEvaluator energyValueEvaluator =
                actor.SetFoodValueEvaluator(new Dictionary<FoodItem, FoodDigestion>
                {
                    {FoodItem.Apple, new FoodDigestion(3, 1)},
                    {FoodItem.Pear, new FoodDigestion(4, 1)},
                    {FoodItem.Meat, new FoodDigestion(6, 3)},
                    {FoodItem.Grass, new FoodDigestion(3, 2)},
                    {FoodItem.Carrot, new FoodDigestion(4, 2)}
                });

            ActionsAnalyser actionsAnalyser = actor.SetActionsAnalyser(new Dictionary<ActionType, int>
            {
                {ActionType.Rain, -3},
                {ActionType.Snow, -5},
                {ActionType.Sun, -6}
                    
            });
            Animal animal = actor.SetAnimal(energyValueEvaluator, actionsAnalyser, 12, 9);
            EnvironmentArea environmentArea = actor.SetEnvironmentArea(animal);

            while (true)
            {
                TimeSpan time = environmentArea.GetTimeBeforeFood();

                if (animal.WillStayAlive(time))
                {
                    if (animal.IsHungry)
                    {
                        animal.SearchForFood(time);
                    }
                    else
                    {
                        animal.Sleep();
                    }

                    Food food = environmentArea.CreateFood();
                    FoodReaction reaction = environmentArea.GiveFood(food);
                    if (reaction == FoodReaction.Alive)
                    {
                        continue;
                    }
                 
                    animal.Die("FOOD REACTION");
                    animal = actor.SetAnimal(energyValueEvaluator, actionsAnalyser, 12, 9);
                    environmentArea = actor.SetEnvironmentArea(animal);
                    continue;
                }

                animal.Die("NO FOOD");
                animal = actor.SetAnimal(energyValueEvaluator, actionsAnalyser, 12, 9);
                environmentArea = actor.SetEnvironmentArea(animal);
            }
        }
    }
}
