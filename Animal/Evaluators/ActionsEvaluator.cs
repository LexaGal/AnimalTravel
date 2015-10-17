using System;
using System.Collections.Generic;
using AnimalEnvironmentItems.Actions;
using AnimalLib.EventsReacting;
using AnimalLib.State;
using Action = AnimalEnvironmentItems.Actions.Action;

namespace AnimalLib.Evaluators
{
    public class ActionsEvaluator : IActionsEvaluator
    {
        public IDictionary<ActionItem, ActionReaction> ActionsReactions { get; private set; }

        public ActionsEvaluator(IDictionary<ActionItem, ActionReaction> actionsReactions)
        {
            ActionsReactions = actionsReactions;
        }

        public StateDelta Evaluate(Action action)
        {
            int satiety = EvaluateSatiety(action);
            int happiness = EvaluateHappiness(action);
            int health = EvaluateHealth(action);
            return new StateDelta(happiness, health, satiety);
        }

        public int EvaluateSatiety(Action action)
        {
            return 0;
        }

        public int EvaluateHappiness(Action action)
        {
            switch (action.Type)
            {
                case ActionType.Fine:
                    return
                        (int)
                            (ActionsReactions[action.Item].HappinessPlus*action.Duration.TotalSeconds*2 -
                             ActionsReactions[action.Item].HappinessMinus*action.Duration.TotalSeconds);
                case ActionType.Sad:
                    return
                        (int)
                            (ActionsReactions[action.Item].HappinessPlus*action.Duration.TotalSeconds/2 -
                             ActionsReactions[action.Item].HappinessMinus*action.Duration.TotalSeconds);
                case ActionType.Neutral:
                    return
                        (int)
                            (ActionsReactions[action.Item].HappinessPlus*action.Duration.TotalSeconds -
                             ActionsReactions[action.Item].HappinessMinus*action.Duration.TotalSeconds);
                case ActionType.Harmful:
                    return
                        (int) ((-1)*Math.Abs((ActionsReactions[action.Item].HappinessPlus*action.Duration.TotalSeconds -
                             ActionsReactions[action.Item].HappinessMinus*action.Duration.TotalSeconds*2)));
                default:
                    return 0;
            }
        }

        public int EvaluateHealth(Action action)
        {
            switch (action.Type)
            {
                case ActionType.Fine:
                    return
                        (int)
                            (ActionsReactions[action.Item].HealthPlus*action.Duration.TotalSeconds*2 -
                             ActionsReactions[action.Item].HealthMinus*action.Duration.TotalSeconds);
                case ActionType.Sad:
                    return
                        (int)
                            (ActionsReactions[action.Item].HealthPlus*action.Duration.TotalSeconds/2 -
                             ActionsReactions[action.Item].HealthMinus*action.Duration.TotalSeconds);
                case ActionType.Neutral:
                    return
                        (int)
                            (ActionsReactions[action.Item].HealthPlus*action.Duration.TotalSeconds -
                             ActionsReactions[action.Item].HealthMinus*action.Duration.TotalSeconds);
                case ActionType.Harmful:
                    return
                        (int)
                            ((-1)*Math.Abs((ActionsReactions[action.Item].HealthPlus*action.Duration.TotalSeconds -
                              ActionsReactions[action.Item].HealthMinus*action.Duration.TotalSeconds*2)));
                default:
                    return 0;
            }
        }
    }
}