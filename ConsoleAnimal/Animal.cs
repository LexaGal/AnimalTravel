using System;
using System.Threading;

namespace ConsoleAnimal
{
    public enum FoodReaction
    {
        Alive,
        Dead
    }

    public class Animal
    {
        public FoodValueEvaluator FoodValueEvaluator { get; private set; }
        public HappinessEvaluator HappinessEvaluator { get; private set; }

        public TimeSpan RemainingLifeTime { get; private set; } // seconds
        public TimeSpan LifeTime { get; private set; } // seconds
        public double Happiness { get; private set; } // %

        public Animal(FoodValueEvaluator foodValueEvaluator,
            HappinessEvaluator happinessEvaluator, TimeSpan lifeTime)
        {
            FoodValueEvaluator = foodValueEvaluator;
            HappinessEvaluator = happinessEvaluator;
            RemainingLifeTime = lifeTime;
            LifeTime = new TimeSpan(0);
            Happiness = 50;
            Console.WriteLine("Animal is born\n");
        }

        public bool WillStayAlive(TimeSpan timeSpan)
        {
            if (timeSpan.TotalSeconds <= RemainingLifeTime.TotalSeconds)
            {
                return true;
            }
            return false;
        }

        public void SearchForFood(TimeSpan timeSpan)
        {
            RemainingLifeTime = RemainingLifeTime.Subtract(timeSpan);
            LifeTime = LifeTime.Add(timeSpan);
            Console.WriteLine("Animal is searching for food... -{0} to life\n", timeSpan.TotalSeconds);
            Thread.Sleep(timeSpan);
        }

        public FoodReaction EatFood(Food food)
        {
            Console.Write("Animal is eating... ");
            if (GetFoodReaction(food) == FoodReaction.Dead)
            {
                Console.WriteLine("\n");
                return FoodReaction.Dead;
            }
            Console.WriteLine("\n\nAnimal's remaining life time: {0} sec.\n", RemainingLifeTime.TotalSeconds);
            return FoodReaction.Alive;
        }

        public FoodReaction GetFoodReaction(Food food)
        {
            TimeSpan timeSpan;
            switch (food.Type)
            {
                case FoodType.Valuable: 
                    timeSpan = new TimeSpan(0, 0, FoodValueEvaluator.Evaluate(food.EnergyValue * 2));
                    Console.Write("+{0} to life", timeSpan.TotalSeconds);
                    RemainingLifeTime = RemainingLifeTime.Add(timeSpan);
                    Happiness += HappinessEvaluator.Evaluate(3);
                    return FoodReaction.Alive;

                case FoodType.TastyButUsefull:
                    timeSpan = new TimeSpan(0, 0, FoodValueEvaluator.Evaluate(food.EnergyValue / 2));
                    Console.Write("+{0} to life", timeSpan.TotalSeconds);
                    RemainingLifeTime = RemainingLifeTime.Add(timeSpan);
                    Happiness += HappinessEvaluator.Evaluate(4);
                    return FoodReaction.Alive;

                case FoodType.Neutral:
                     timeSpan = new TimeSpan(0, 0, FoodValueEvaluator.Evaluate(food.EnergyValue));
                     Console.Write("+{0} to life", timeSpan.TotalSeconds);
                     RemainingLifeTime = RemainingLifeTime.Add(timeSpan);
                     Happiness += HappinessEvaluator.Evaluate(2);
                     return FoodReaction.Alive;

                case FoodType.Harmful:
                    timeSpan = new TimeSpan(0, 0, FoodValueEvaluator.Evaluate(food.EnergyValue));
                    Console.Write("-{0} to life", timeSpan.TotalSeconds);
                    RemainingLifeTime = RemainingLifeTime.Subtract(timeSpan);
                    Happiness += HappinessEvaluator.Evaluate(-2);
                    return FoodReaction.Alive;

                case FoodType.Killing:
                    return FoodReaction.Dead;

                default :
                    return FoodReaction.Alive;
            }
        }

        public void Die(string reason)
        {
            Console.WriteLine("Sorry, animal is dead ({0})\n", reason);
            Console.WriteLine("Animal's life time: {0} sec.\n----------------------------\n", LifeTime.TotalSeconds);
        }
    }
}