using System;
using System.Collections.Generic;
using System.Linq;
using AnimalEnvironmentItems.Foods;

namespace FoodActionsLib.Functions
{
    public class FoodFunction : IFoodFunction
    {
        public FoodFunction(IList<EventTime<Food>> foodsTimes)
        {
            FoodsTimes = foodsTimes;
        }

        public IList<EventTime<Food>> FoodsTimes { get; private set; }

        public Food GetFood(TimeSpan eventTime)
        {
            double min = FoodsTimes.Min(f => Math.Abs(eventTime.TotalSeconds - f.Time.TotalSeconds));
            return FoodsTimes
                .First(f => 
                    Math.Abs(eventTime.TotalSeconds - (f.Time.TotalSeconds + min)) < 0.1 ||
                    Math.Abs(eventTime.TotalSeconds - (f.Time.TotalSeconds - min)) < 0.1
                    ).Event;
        }
    }
}