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

        public TimeSpan RemainingLifeTimeLimit { get; private set; }

        private readonly Random _sleepTimeRandom; 

        public bool IsHappy
        {
            get { return Happiness >= 50; }
        }

        public bool IsHungry
        {
            get { return RemainingLifeTime.TotalSeconds <= RemainingLifeTimeLimit.TotalSeconds; }
        }


        public Animal(FoodValueEvaluator foodValueEvaluator,
            HappinessEvaluator happinessEvaluator, TimeSpan remainingLifeTime, TimeSpan remainingLifeTimeLimit)
        {
            FoodValueEvaluator = foodValueEvaluator;
            HappinessEvaluator = happinessEvaluator;
            RemainingLifeTime = remainingLifeTime;
            LifeTime = new TimeSpan(0);
            RemainingLifeTimeLimit = remainingLifeTimeLimit;
            Happiness = 50;
            _sleepTimeRandom = new Random();
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
                if (RemainingLifeTime.TotalSeconds/RemainingLifeTimeLimit.TotalSeconds < 0.5)
                {
                    Console.WriteLine("\n");
                    return FoodReaction.Dead;
                }
                Console.Write("Food was refused");
            }
            Console.WriteLine("\n\nAnimal's remaining life time: {0} sec.\n", RemainingLifeTime.TotalSeconds);
            return FoodReaction.Alive;
        }

        public void ChangeState(Food food, int happiness)
        {
            TimeSpan timeSpan = new TimeSpan(0, 0,
                           Math.Abs(FoodValueEvaluator.EvaluateEnergyPlus(food) - FoodValueEvaluator.EvaluateEnergyMinus(food)));
            Console.Write("{0}{1} to life", food.Type == FoodType.Harmful ? '-' : '+', timeSpan.TotalSeconds);
            RemainingLifeTime = food.Type == FoodType.Harmful ? RemainingLifeTime.Subtract(timeSpan) : RemainingLifeTime.Add(timeSpan);
            Happiness += HappinessEvaluator.Evaluate(happiness);
        }

        public FoodReaction GetFoodReaction(Food food)
        {
            switch (food.Type)
            {
                case FoodType.Valuable:
                    ChangeState(food, 3);
                    return FoodReaction.Alive;

                case FoodType.TastyButUseLess:
                    ChangeState(food, 4);
                    return FoodReaction.Alive;

                case FoodType.Neutral:
                    ChangeState(food, 2);
                    return FoodReaction.Alive;

                case FoodType.Harmful:
                    ChangeState(food, -2);
                    return FoodReaction.Alive;

                case FoodType.Killing:
                    return FoodReaction.Dead;

                default:
                    return FoodReaction.Alive;
            }
        }

        public void Die(string reason)
        {
            Console.WriteLine("Sorry, animal is dead ({0})\n", reason);
            Console.WriteLine("Animal's life time: {0} sec.\n----------------------------\n", LifeTime.TotalSeconds);
        }

        public void Sleep()
        {
            int remainingTime = (int)RemainingLifeTime.Subtract(RemainingLifeTimeLimit).TotalSeconds;
            int sleepTime = remainingTime + _sleepTimeRandom.Next((-1)*remainingTime + 1, remainingTime/3);
            TimeSpan timeSpan = new TimeSpan(0, 0, sleepTime);
            
            RemainingLifeTime = RemainingLifeTime.Subtract(timeSpan);
            Console.WriteLine("Animal is sleepping ... -{0} to life\n", timeSpan.TotalSeconds);
            LifeTime = LifeTime.Add(timeSpan);
            Thread.Sleep(timeSpan);
        }
    }
}