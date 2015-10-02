using System.Collections.Generic;
using AnimalEnvironmentItems.Actions;

namespace AnimalLib.Evaluators
{
    public class ActionsEvaluator : IActionsEvaluator
    {
        public IDictionary<ActionType, int> ActionsEvaluations { get; private set; }
        
        public ActionsEvaluator(IDictionary<ActionType, int> actionsEvaluations)
        {
            ActionsEvaluations = actionsEvaluations;
        }

        public int EvaluateAction(Action action)
        {
            switch (action.Type)
            {
                case ActionType.Rain:
                    return ActionsEvaluations[action.Type];
                case ActionType.Snow:
                    return ActionsEvaluations[action.Type];
                case ActionType.Sun:
                    return ActionsEvaluations[action.Type];
                default:
                    return 0;
            }
        }
    }
}