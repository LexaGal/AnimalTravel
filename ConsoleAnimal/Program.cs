using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleAnimal;

namespace ConsoleAnimal
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Actor actor = new Actor();
            FoodValueEvaluator energyValueEvaluator = actor.SetFoodValueEvaluator(1, 1);
            HappinessEvaluator happinessEvaluator = actor.SetHappinessEvaluator(1, 1);
            Animal animal = actor.SetAnimal(energyValueEvaluator, happinessEvaluator, 20);
            EnvironmentArea environmentArea = actor.SetEnvironmentArea(animal);

            while (true)
            {
                TimeSpan time = environmentArea.GetTimeBeforeFood();

                if (animal.WillStayAlive(time))
                {
                    animal.SearchForFood(time);
                    Food food = environmentArea.CreateFood();
                    FoodReaction reaction = environmentArea.GiveFood(food);
                    if (reaction == FoodReaction.Alive)
                    {
                        continue;
                    }
                 
                    animal.Die("FOOD REACTION");
                    animal = actor.SetAnimal(energyValueEvaluator, happinessEvaluator, 20);
                    environmentArea = actor.SetEnvironmentArea(animal);
                    continue;
                }

                animal.Die("NO FOOD");
                animal = actor.SetAnimal(energyValueEvaluator, happinessEvaluator, 20);
                environmentArea = actor.SetEnvironmentArea(animal);
            }
        }
    }
}
