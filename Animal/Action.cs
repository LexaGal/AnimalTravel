using System;

namespace AnimalLib
{
    public class Action
    {
        public Action(ActionType type, TimeSpan duration)
        {
            Type = type;
            Duration = duration;
        }

        public ActionType Type{ get; private set; }
        public TimeSpan Duration { get; private set; }
    }
}