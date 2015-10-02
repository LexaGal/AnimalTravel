using System;

namespace AnimalEnvironmentItems.Actions
{
    public class Action
    {
        public Guid Id { get; private set; }

        public Action(ActionType type, TimeSpan duration)
        {
            Id = Guid.NewGuid();
            Type = type;
            Duration = duration;
        }

        public ActionType Type{ get; private set; }
        public TimeSpan Duration { get; private set; }
    }
}