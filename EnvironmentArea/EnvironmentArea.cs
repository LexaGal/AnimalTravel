using System;
using AnimalEnvironmentItems;
using AnimalLib;

namespace FoodActionsLib
{
    public class EnvironmentArea
    {
        private readonly Random _random = new Random();
        public Animal Animal { get; private set; }

        public EnvironmentArea(Animal animal)
        {
            Animal = animal;
        }

        public TimeSpan GetTimeBeforeFood()
        {
            return new TimeSpan(0, 0, _random.Next(3, 6));
        }

        public Food CreateFood()
        {
            return new Food((FoodType)_random.Next(0, 5), (FoodItem)_random.Next(0, 3), _random.Next(1, 5));
        }

        public FoodReaction GiveFood(Food food)
        {
            return Animal.EatFood(food);
        }
    }
}