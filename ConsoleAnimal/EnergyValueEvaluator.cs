using System;
using System.Collections.Generic;

namespace ConsoleAnimal
{
    public abstract class Evaluator
    {
        public KeyValuePair<int, int> EvaluationalPair { get; private set; }

        protected Evaluator(KeyValuePair<int, int> evaluationalPair)
        {
            EvaluationalPair = evaluationalPair;
        }

        public abstract int Evaluate(int value);
    }
}