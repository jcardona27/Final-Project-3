using System;

namespace FinalGame
{
    public class GameState
    {
        public int Health { get; private set; } = 50;
        public int Wealth { get; private set; } = 50;
        public int Popularity { get; private set; } = 50;
        public DateTime GameDate { get; private set; } = DateTime.Today;

        public void Update(StatType stat, int amount)
        {
            switch (stat)
            {
                case StatType.Health:
                    Health += amount;
                    break;
                case StatType.Wealth:
                    Wealth += amount;
                    break;
                case StatType.Popularity:
                    Popularity += amount;
                    break;
            }
        }

        public override string ToString()
        {
            return $"Health: {Health}, Wealth: {Wealth}, Popularity: {Popularity}, Date: {GameDate.ToShortDateString()}";
        }
    }
}
