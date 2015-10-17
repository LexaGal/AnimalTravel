using System;
using System.Threading;
using AnimalEnvironmentItems.Actions;
using AnimalEnvironmentItems.Foods;
using AnimalLib.Evaluators;
using AnimalLib.EventsReacting;
using AnimalLib.State;
using Action = AnimalEnvironmentItems.Actions.Action;

namespace AnimalLib
{
    public class Animal : IAnimal
    {
        protected readonly Random SleepTimeRandom;
        protected readonly IFoodEvaluator FoodEvaluator;
        protected readonly IActionsEvaluator ActionsEvaluator;
        protected readonly InternalState InternalState;
 
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

        public Animal(InternalState internalState, IFoodEvaluator foodEvaluator, IActionsEvaluator actionsEvaluator)
        {
            InternalState = internalState;
            FoodEvaluator = foodEvaluator;
            ActionsEvaluator = actionsEvaluator;
            SleepTimeRandom = new Random();
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

        public EventReaction EatFood(Food food)
        {
            Console.Write("Animal is eating {0} {1}... ", food.Type, food.Item);

            if (GetFoodReaction(food) == EventReaction.Dead)
            {
                if (InternalState.RemainingLifeTime.TotalSeconds/InternalState.RemainingLifeTimeLimit.TotalSeconds < 0.5)
                {
                    return EventReaction.Dead;
                }
                Console.Write("Food was refused");
            }

            int remainingLifeTime = (int) InternalState.RemainingLifeTime.TotalSeconds;
         
            InternalState.Change(FoodEvaluator.Evaluate(food));

            if (InternalState.RemainingLifeTime.TotalSeconds <= 0)
            {
                return EventReaction.Dead;
            }

            Console.Write("{0} to life",
                InternalState.RemainingLifeTime.TotalSeconds - remainingLifeTime);
            Console.WriteLine("\n\nAnimal's remaining life time: {0} sec.\n",
                InternalState.RemainingLifeTime.TotalSeconds);

            return EventReaction.Alive;
        }

        public EventReaction ReactForAction(Action action)
        {
            Console.Write("Animal is affected by {0} {1}... ", action.Type, action.Item);

            if (GetActionReaction(action) == EventReaction.Dead)
            {
                if (InternalState.RemainingLifeTime.TotalSeconds/InternalState.RemainingLifeTimeLimit.TotalSeconds < 0.5)
                {
                    return EventReaction.Dead;
                }
                Console.Write("Animal absconded from action");
            }

            int remainingLifeTime = (int) InternalState.RemainingLifeTime.TotalSeconds;

            InternalState.Change(ActionsEvaluator.Evaluate(action));

            if (InternalState.RemainingLifeTime.TotalSeconds <= 0)
            {
                return EventReaction.Dead;
            }

            Console.Write("{0} to life time",
                InternalState.RemainingLifeTime.TotalSeconds - remainingLifeTime);
            Console.WriteLine("\n\nAnimal's remaining life time: {0} sec.\n",
                InternalState.RemainingLifeTime.TotalSeconds);

            Thread.Sleep(action.Duration);
            return EventReaction.Alive;
        }

        protected EventReaction GetFoodReaction(Food food)
        {
            if (food.Type == FoodType.Killing)
            {
                return EventReaction.Dead;
            }
            return EventReaction.Alive;
        }

        protected EventReaction GetActionReaction(Action action)
        {
            if (action.Type == ActionType.Killing)
            {
                return EventReaction.Dead;
            }
            return EventReaction.Alive;
        }

        public void Die(DeathReason reason)
        {
            Console.WriteLine("Sorry, animal is dead ({0})\n", reason.GetDescription());
            Console.WriteLine("Animal's life time: {0} sec.\n----------------------------\n",
                InternalState.FullLifeTime.TotalSeconds);
        }

        public void Sleep()
        {
            int remainingTime = (int)InternalState.RemainingLifeTime.TotalSeconds;
            int sleepTime = SleepTimeRandom.Next(remainingTime/5, remainingTime/4);
            TimeSpan timeSpan = new TimeSpan(0, 0, sleepTime);
            
            InternalState.DecreaseRemainingLifeTime(timeSpan);
            InternalState.IncreaseFullLifeTime(timeSpan);
            
            Console.WriteLine("Animal is sleepping ... -{0} to life\n", timeSpan.TotalSeconds);
            Thread.Sleep(timeSpan);
        }
    }
}