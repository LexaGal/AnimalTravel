using System;
using AnimalEnvironmentItems.Foods;
using AnimalLib.EventsReacting;
using Action = AnimalEnvironmentItems.Actions.Action;

namespace AnimalLib
{
    public interface IAnimal
    {
        bool WillStayAlive(TimeSpan timeSpan);
        void SearchForFood(TimeSpan timeSpan);
        EventReaction EatFood(Food food);
        EventReaction ReactForAction(Action action);
        void Die(DeathReason dieReason);
        void Sleep();
    }
}