using System;

namespace AnimalLib.State
{
    public class InternalState
    {
        public TimeSpan RemainingLifeTime { get; private set; } // seconds
        public TimeSpan RemainingLifeTimeLimit { get; private set; } // seconds
        public TimeSpan FullLifeTime { get; private set; } // seconds
        public int Happiness { get; private set; } // %
        public int Health { get; private set; } // %
        public int Satiety { get; private set; } // %

        public InternalState(TimeSpan remainingLifeTime, TimeSpan remainingLifeTimeLimit)
        {
            RemainingLifeTime = remainingLifeTime;
            RemainingLifeTimeLimit = remainingLifeTimeLimit;
            FullLifeTime = new TimeSpan(0);
            Happiness = 50;
            Health = 100;
            Satiety = 50;
        }

        public void IncreaseHappiness(int happiness)
        {
            Happiness = (Happiness += happiness)%100;
        }

        public void DecreaseHappiness(int happiness)
        {
            Happiness -= happiness;
            if (Happiness < 0)
            {
                Happiness = 0;
            }
        }

        public void IncreaseHealth(int health)
        {
            Happiness = (Happiness += health)%100;
        }

        public void DecreaseHealth(int satiety)
        {
            Health -= satiety;
            if (Satiety < 0)
            {
                Satiety = 0;
            }
        }

        public void IncreaseSatiety(int satiety)
        {
            Satiety = (Satiety += satiety)%100;
        }

        public void DecreaseSatiety(int satiety)
        {
            Satiety -= satiety;
            if (Satiety < 0)
            {
                Satiety = 0;
            }
        }

        public void IncreaseRemainingLifeTime(TimeSpan timeSpan)
        {
            RemainingLifeTime = RemainingLifeTime.Add(timeSpan);
        }

        public void DecreaseRemainingLifeTime(TimeSpan timeSpan)
        {
            RemainingLifeTime = RemainingLifeTime.Subtract(timeSpan);
            if (RemainingLifeTime.TotalSeconds < 0)
            {
                RemainingLifeTime = new TimeSpan(0);
            }
        }

        public void IncreaseFullLifeTime(TimeSpan timeSpan)
        {
            FullLifeTime = FullLifeTime.Add(timeSpan);
        }
    }
}