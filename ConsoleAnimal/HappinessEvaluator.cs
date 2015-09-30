using System.Collections.Generic;

namespace ConsoleAnimal
{
    public class HappinessEvaluator : Evaluator
    {
        public HappinessEvaluator(KeyValuePair<int, int> evaluationalPair)
            : base(evaluationalPair)
        { }

        public override int Evaluate(int value)
        {
            return EvaluationalPair.Value * value / EvaluationalPair.Key;
        }
    }
}