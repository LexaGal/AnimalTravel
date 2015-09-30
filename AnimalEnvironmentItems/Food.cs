using System;

namespace AnimalEnvironmentItems
{
    public class Food
    {
        public Food(FoodType type, FoodItem item, int n)
        {
            Type = type;
            Item = item;
            N = n;
        }
        
        public FoodType Type { get; private set; }
        public FoodItem Item { get; private set; }
        public int N { get; private set; }
    }
}