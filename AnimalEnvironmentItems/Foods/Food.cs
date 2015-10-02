using System;

namespace AnimalEnvironmentItems.Foods
{
    public class Food
    {
        public Guid Id { get; private set; }

        public Food(FoodType type, FoodItem item, int n)
        {
            Id = Guid.NewGuid();
            Type = type;
            Item = item;
            N = n;
        }
        
        public FoodType Type { get; private set; }
        public FoodItem Item { get; private set; }
        public int N { get; private set; }
    }
}