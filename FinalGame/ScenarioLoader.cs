using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace FinalGame
{
    public static class ScenarioLoader
    {
        
        public static List<Scenario> LoadFromJson(string path)
        {
            try
            {
                string fullPath = Path.Combine(AppContext.BaseDirectory, path);
                

                string json = File.ReadAllText(fullPath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var loaded = JsonSerializer.Deserialize<List<Scenario>>(json, options);
                return loaded ?? new List<Scenario>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Failed to load {path}: {ex.Message}");
                return LoadDefault();
            }
        }

      
        [Scenario("starter")]
        public static List<Scenario> LoadDefault()
        {
            var scenarios = new List<Scenario>
            {
                new Scenario
                {
                    Text = "A healer offers you a potion.",
                    Options = new List<ScenarioOption>
                    {
                        new ScenarioOption
                        {
                            Description = "Drink it.",
                            Effects = new Dictionary<StatType, int>
                            {
                                { StatType.Health, 10 },
                                { StatType.Wealth, -5 }
                            }
                        },
                        new ScenarioOption
                        {
                            Description = "Refuse politely.",
                            Effects = new Dictionary<StatType, int>
                            {
                                { StatType.Popularity, 5 }
                            }
                        }
                    }
                }
            };

            ValidateScenarioData(scenarios);
            return scenarios;
        }

      
        public static int CountScenarios(List<Scenario> scenarios)
        {
            return scenarios?.Count ?? 0;
        }

      
        public static void ValidateScenarioData(List<Scenario> scenarios)
        {
            if (scenarios == null || scenarios.Count == 0)
            {
                Console.WriteLine("⚠️ Warning: No scenarios available.");
                return;
            }

            foreach (var s in scenarios)
            {
                if (string.IsNullOrWhiteSpace(s.Text))
                {
                    Console.WriteLine("⚠️ Found a scenario with no text.");
                }

                if (s.Options.Count != 2)
                {
                    Console.WriteLine($"⚠️ Scenario \"{s.Text}\" should have exactly 2 options.");
                }

                foreach (var option in s.Options)
                {
                    if (string.IsNullOrWhiteSpace(option.Description))
                    {
                        Console.WriteLine($"⚠️ An option in \"{s.Text}\" is missing a description.");
                    }
                }
            }
        }

        
        
        

     

    }
}
