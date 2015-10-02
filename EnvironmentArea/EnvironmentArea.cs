using System;
using System.Linq;
using AnimalEnvironmentItems;
using AnimalEnvironmentItems.Actions;
using AnimalEnvironmentItems.Foods;
using FoodActionsLib.Functions;
using Action = AnimalEnvironmentItems.Actions.Action;

namespace FoodActionsLib
{
    public class EnvironmentArea
    {
        private readonly Random _creationTimeRandom; 
        public IActionFunction ActionFunction { get; private set; }
        public IFoodFunction FoodFunction { get; private set; }
        
        public EnvironmentArea(IActionFunction actionFunction, IFoodFunction foodFunction)
        {
            ActionFunction = actionFunction;
            FoodFunction = foodFunction;
            _creationTimeRandom = new Random();
        }

        public TimeSpan GetTimeBeforeFood()
        {
            return new TimeSpan(0, 0, _creationTimeRandom.Next(3, 6));
        }

        public TimeSpan GetTimeBeforeAction()
        {
            return new TimeSpan(0, 0, _creationTimeRandom.Next(5, 8));
        }

        public Food CreateFood()
        {
            return FoodFunction.GetFood(
                new TimeSpan(0, 0, _creationTimeRandom.Next(1, (int) FoodFunction.FoodsTimes.Max(f => f.Time.TotalSeconds))));
            //return new Food((FoodType) _creationTimeRandom.Next(0, 5), (FoodItem) _creationTimeRandom.Next(0, 5), _creationTimeRandom.Next(1, 5));
        }

        public Action CreateAction()
        {
            return ActionFunction.GetAction(
                new TimeSpan(0, 0, _creationTimeRandom.Next(1, (int)ActionFunction.ActionsTimes.Max(a => a.Time.TotalSeconds))));
            //return new Action((ActionType)_creationTimeRandom.Next(0, 5), new TimeSpan(0, 0, _creationTimeRandom.Next(0, 5)));
        }
    }
}