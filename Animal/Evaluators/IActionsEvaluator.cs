using System.Collections.Generic;
using AnimalEnvironmentItems.Actions;
using AnimalLib.EventsReacting;
using AnimalLib.State;

namespace AnimalLib.Evaluators
{
    public interface IActionsEvaluator
    {
        IDictionary<ActionItem, ActionReaction> ActionsReactions { get; }
        StateDelta Evaluate(Action action);
        int EvaluateSatiety(Action action);
        int EvaluateHappiness(Action action);
        int EvaluateHealth(Action action);
    }
}