using System;

namespace FoodActionsLib.Functions
{
    public class EventTime<T> where T: class
    {
        public T Event { get; private set; }
        public TimeSpan Time { get; private set; }

        public EventTime(T @event, TimeSpan time)
        {
            Event = @event;
            Time = time;
        }
    }
}