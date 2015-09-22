using System.Collections.Generic;

namespace ConsoleAnimal
{
    public class FoodValueEvaluator : Evaluator
    {
        public FoodValueEvaluator(KeyValuePair<int, int> evaluationalPair) : base(evaluationalPair)
        {}
        
        public override int Evaluate(int energyValue)
        {
            return EvaluationalPair.Value * energyValue / EvaluationalPair.Key;
        }
    }
}