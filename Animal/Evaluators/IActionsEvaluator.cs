using System.Collections.Generic;
using AnimalEnvironmentItems.Actions;

namespace AnimalLib.Evaluators
{
    public interface IActionsEvaluator
    {
        IDictionary<ActionType, int> ActionsEvaluations { get; }
        int EvaluateAction(Action action);
    }
}