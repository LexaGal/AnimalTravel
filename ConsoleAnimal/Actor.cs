using System;
using System.Collections.Generic;

namespace ConsoleAnimal
{
    public class Actor
    {
        public FoodValueEvaluator SetFoodValueEvaluator(int energyValue, int lifeTime)
        {
            return new FoodValueEvaluator(new KeyValuePair<int, int>(energyValue, lifeTime));
        }

        public HappinessEvaluator SetHappinessEvaluator(int energyValue, int lifeTime)
        {
            return new HappinessEvaluator(new KeyValuePair<int, int>(energyValue, lifeTime));
        }

        public Animal SetAnimal(FoodValueEvaluator energyValueEvaluator,
            HappinessEvaluator happinessEvaluator, int lifeTime)
        {
            return new Animal(energyValueEvaluator, happinessEvaluator, new TimeSpan(0, 0, lifeTime));
        }

        public EnvironmentArea SetEnvironmentArea(Animal animal)
        {
            return new EnvironmentArea(animal);
        }
    }
}