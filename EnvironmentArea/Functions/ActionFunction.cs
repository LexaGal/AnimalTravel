using System;
using System.Collections.Generic;
using System.Linq;
using Action = AnimalEnvironmentItems.Actions.Action;

namespace FoodActionsLib.Functions
{
    public class ActionFunction : IActionFunction
    {
        public ActionFunction(IList<EventTime<Action>> actionsTimes)
        {
            ActionsTimes = actionsTimes;
        }

        public IList<EventTime<Action>> ActionsTimes { get; private set; }

        public Action GetAction(TimeSpan eventTime)
        {
            double min = ActionsTimes.Min(a => Math.Abs(eventTime.TotalSeconds - a.Time.TotalSeconds));
            return ActionsTimes
                .First(a =>
                    Math.Abs(eventTime.TotalSeconds - (a.Time.TotalSeconds + min)) < 0.1 ||
                    Math.Abs(eventTime.TotalSeconds - (a.Time.TotalSeconds - min)) < 0.1
                    ).Event;
        }
    }
}