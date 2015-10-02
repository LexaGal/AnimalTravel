using System;
using System.Collections.Generic;
using AnimalEnvironmentItems.Foods;

namespace FoodActionsLib.Functions
{
    public interface IFoodFunction
    {
        IList<EventTime<Food>> FoodsTimes { get; }
        Food GetFood(TimeSpan eventTime);
    }
}