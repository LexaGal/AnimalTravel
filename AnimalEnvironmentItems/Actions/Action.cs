using System;

namespace AnimalEnvironmentItems.Actions
{
    public class Action
    {
        public Guid Id { get; private set; }

        public Action(ActionItem item, ActionType type, TimeSpan duration)
        {
            Id = Guid.NewGuid();
            Item = item;
            Duration = duration;
            Type = type;
        }

        public ActionItem Item { get; private set; }
        public ActionType Type { get; private set; }
        public TimeSpan Duration { get; private set; }
    }
}