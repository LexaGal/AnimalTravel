namespace AnimalLib.State
{
    public class StateDelta
    {
        public int Happiness { get; private set; }
        public int Health { get; private set; }
        public int Satiety { get; private set; }

        public StateDelta(int happiness, int health, int satiety)
        {
            Happiness = happiness;
            Health = health;
            Satiety = satiety;
        }
    }
}