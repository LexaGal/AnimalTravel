using System.Collections.Generic;
using AnimalEnvironmentItems;

namespace AnimalLib
{
    public class ActionsAnalyser
    {
        public IDictionary<ActionType, int> Dictionary { get; private set; }
        
        public ActionsAnalyser(IDictionary<ActionType, int> dictionary)
        {
            Dictionary = dictionary;
        }

        public int AnalyseAction(Action action)
        {
            switch (action.Type)
            {
                case ActionType.Rain:
                    return Dictionary[action.Type];
                case ActionType.Snow:
                    return Dictionary[action.Type];
                case ActionType.Sun:
                    return Dictionary[action.Type];
                
                default:
                    return 0;
            }
        }
    }
}