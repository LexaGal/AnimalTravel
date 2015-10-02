using System;
using System.Threading;
using AnimalEnvironmentItems;
using AnimalEnvironmentItems.Foods;
using AnimalLib.Evaluators;
using AnimalLib.FoodReacting;
using AnimalLib.State;

namespace AnimalLib
{
    public class Animal
    {
        private readonly Random _sleepTimeRandom; 

        public IFoodValuesEvaluator FoodValueEvaluator { get; private set; }
        public IActionsEvaluator ActionsEvaluator { get; private set; }

        public InternalState InternalState { get; private set; }
 
        public bool IsHappy
        {
            get { return InternalState.Happiness >= 50; }
        }

        public bool IsSatiated
        {
            get { return InternalState.RemainingLifeTime.TotalSeconds >= InternalState.RemainingLifeTimeLimit.TotalSeconds; }
        }

        public bool IsHealthy
        {
            get { return InternalState.Health >= 50; }
        }

        public Animal(InternalState internalState, IFoodValuesEvaluator foodValueEvaluator, IActionsEvaluator actionsEvaluator)
        {
            InternalState = internalState;
            FoodValueEvaluator = foodValueEvaluator;
            ActionsEvaluator = actionsEvaluator;
            _sleepTimeRandom = new Random();
            Console.WriteLine("Animal is born\n");
        }

        public bool WillStayAlive(TimeSpan timeSpan)
        {
            if (timeSpan.TotalSeconds <= InternalState.RemainingLifeTime.TotalSeconds)
            {
                return true;
            }
            return false;
        }

        public void SearchForFood(TimeSpan timeSpan)
        {
            InternalState.DecreaseRemainingLifeTime(timeSpan);
            InternalState.IncreaseFullLifeTime(timeSpan);
            Console.WriteLine("Animal is searching for food... -{0} to life\n", timeSpan.TotalSeconds);
            Thread.Sleep(timeSpan);
        }

        public FoodReaction EatFood(Food food)
        {
            Console.Write("Animal is eating... ");
            if (GetFoodReaction(food) == FoodReaction.Dead)
            {
                if (InternalState.RemainingLifeTime.TotalSeconds/InternalState.RemainingLifeTimeLimit.TotalSeconds < 0.5)
                {
                    Console.WriteLine("\n");
                    return FoodReaction.Dead;
                }
                Console.Write("Food was refused");
            }
            Console.WriteLine("\n\nAnimal's remaining life time: {0} sec.\n", InternalState.RemainingLifeTime.TotalSeconds);
            return FoodReaction.Alive;
        }

        public void ReactForFood(Food food, int happiness)
        {
            TimeSpan timeSpan = new TimeSpan(0, 0,
                           Math.Abs(FoodValueEvaluator.EvaluateEnergyPlus(food) - FoodValueEvaluator.EvaluateEnergyMinus(food)));
            if (happiness >= 0)
            {
                InternalState.IncreaseHappiness(happiness);
            }
            else
            {
                InternalState.DecreaseHappiness(happiness);
            }
            if (food.Type != FoodType.Harmful)
            {
                InternalState.IncreaseRemainingLifeTime(timeSpan);
            }
            else
            {
                InternalState.DecreaseRemainingLifeTime(timeSpan);
            }
            Console.Write("{0}{1} to life", food.Type == FoodType.Harmful ? '-' : '+', timeSpan.TotalSeconds);
        }

        public FoodReaction GetFoodReaction(Food food)
        {
            switch (food.Type)
            {
                case FoodType.Valuable:
                    ReactForFood(food, 3);
                    return FoodReaction.Alive;

                case FoodType.TastyButUseless:
                    ReactForFood(food, 4);
                    return FoodReaction.Alive;

                case FoodType.Neutral:
                    ReactForFood(food, 0);
                    return FoodReaction.Alive;

                case FoodType.Harmful:
                    ReactForFood(food, -5);
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
            Console.WriteLine("Animal's life time: {0} sec.\n----------------------------\n", InternalState.FullLifeTime.TotalSeconds);
        }

        public void Sleep()
        {
            int remainingTime = (int)InternalState.RemainingLifeTime.TotalSeconds;
            int sleepTime = _sleepTimeRandom.Next(remainingTime/5, remainingTime/4);
            TimeSpan timeSpan = new TimeSpan(0, 0, sleepTime);
            
            InternalState.DecreaseRemainingLifeTime(timeSpan);
            Console.WriteLine("Animal is sleepping ... -{0} to life\n", timeSpan.TotalSeconds);
            InternalState.IncreaseFullLifeTime(timeSpan);
            Thread.Sleep(timeSpan);
        }
    }
}