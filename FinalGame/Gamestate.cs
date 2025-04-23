using System;
using System.Linq;
using System.Text;

namespace FinalGame
{
    public class GameState
    {
        public int Health { get; private set; } = 50;
        public int Wealth { get; private set; } = 50;
        public int Popularity { get; private set; } = 50;
        public DateTime GameDate { get; private set; } = DateTime.Today;
        public int TotalDecisionsMade { get; private set; } = 0;

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

            TotalDecisionsMade++;
            GameDate = GameDate.AddDays(1);
        }

        public void RestoreStats()
        {
            Health = 50;
            Wealth = 50;
            Popularity = 50;
            GameDate = DateTime.Today;
            TotalDecisionsMade = 0;
        }

        public string StatSummary()
        {
            return $"[H:{Health} | W:{Wealth} | P:{Popularity}] - {GameDate.ToShortDateString()}";
        }

        public string StatBar(StatType stat)
        {
            int value = GetStatValue(stat);
            int barLength = value / 2; 
            return new string('|', barLength).PadRight(50);
        }

        public int GetStatValue(StatType stat)
        {
            return stat switch
            {
                StatType.Health => Health,
                StatType.Wealth => Wealth,
                StatType.Popularity => Popularity,
                _ => 0
            };
        }
    public string DetailedReport()
        {
            string report = "\n===== Player Report =====\n";
            report += $"Health     [{Health}]: {StatBar(StatType.Health)}\n";
            report += $"Wealth     [{Wealth}]: {StatBar(StatType.Wealth)}\n";
            report += $"Popularity [{Popularity}]: {StatBar(StatType.Popularity)}\n";
            report += $"Date: {GameDate.ToShortDateString()}\n";
            report += $"Total decisions made: {TotalDecisionsMade}\n";
            return report;
        }

        public void DisplayFinalAdvice()
        {
            Console.WriteLine("\n===== Your Reign in Review =====");
            if (Health >= 60) Console.WriteLine("You ruled in strength and vigor.");
            else if (Health <= 20) Console.WriteLine("You neglected your body, and it cost you.");

            if (Wealth >= 60) Console.WriteLine("Your kingdom was prosperous.");
            else if (Wealth <= 20) Console.WriteLine("You left the treasury in ruins.");

            if (Popularity >= 60) Console.WriteLine("The people adored your rule.");
            else if (Popularity <= 20) Console.WriteLine("You ruled through fear and silence.");
        }

        public override string ToString()
        {
            return $"Health: {Health}, Wealth: {Wealth}, Popularity: {Popularity}, Date: {GameDate.ToShortDateString()}";
        }
    }
}
