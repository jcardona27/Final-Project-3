using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FinalGame
{
    public static class ScenarioLoader
    {
        public static List<Scenario> LoadFromJson(string path)
        {
            try
            {
                string json = File.ReadAllText(path);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<Scenario>>(json, options) ?? new List<Scenario>();
            }
            catch (Exception)
            {
                Console.WriteLine("⚠️ Failed to load Scenarios.json. Falling back to default.");
                return LoadDefault();
            }
        }

        [Scenario("starter")]
        public static List<Scenario> LoadDefault()
        {
            return new List<Scenario>
            {
                new Scenario
                {
                    Text = "A healer offers you a potion.",
                    Options = new List<ScenarioOption>
                    {
                        new ScenarioOption
                        {
                            Description = "Drink it.",
                            Effects = new Dictionary<StatType, int> { { StatType.Health, 10 }, { StatType.Wealth, -5 } }
                        },
                        new ScenarioOption
                        {
                            Description = "Refuse politely.",
                            Effects = new Dictionary<StatType, int> { { StatType.Popularity, 5 } }
                        }
                    }
                }
            };
        }
    }
}
