using System;
using System.Collections.Generic;
using AnimalEnvironmentItems;
using AnimalLib;
using FoodActionsLib;

namespace Actor
{
    public class Actor
    {
        public FoodValueEvaluator SetFoodValueEvaluator(IDictionary<FoodItem, FoodDigestion> dictionary)
        {
            return new FoodValueEvaluator(dictionary);
        }

        public ActionsAnalyser SetActionsAnalyser(IDictionary<ActionType, int> dictionary)
        {
            return new ActionsAnalyser(dictionary);
        }

        public Animal SetAnimal(FoodValueEvaluator energyValueEvaluator,
            ActionsAnalyser happinessEvaluator, int lifeTime, int remainingLifeTimeLimit)
        {
            return new Animal(energyValueEvaluator, happinessEvaluator, new TimeSpan(0, 0, lifeTime), 
                new TimeSpan(0, 0, remainingLifeTimeLimit));
        }

        public EnvironmentArea SetEnvironmentArea(Animal animal)
        {
            return new EnvironmentArea(animal);
        }
    }
}