using System;
using AnimalEnvironmentItems.Foods;
using Action = AnimalEnvironmentItems.Actions.Action;

namespace FoodActionsLib
{
    public interface IEnvironmentArea
    {
        TimeSpan GetTimeBeforeFood();
        TimeSpan GetTimeBeforeAction();
        Food CreateFood();
        Action CreateAction();
    }
}