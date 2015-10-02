using System;
using System.Collections.Generic;
using Action = AnimalEnvironmentItems.Actions.Action;

namespace FoodActionsLib.Functions
{
    public interface IActionFunction
    {
        IList<EventTime<Action>> ActionsTimes { get; }
        Action GetAction(TimeSpan eventTime);
    }
}