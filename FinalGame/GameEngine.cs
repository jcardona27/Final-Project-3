using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;

namespace FinalGame
{
    public class GameEngine
    {
        private static readonly Lazy<GameEngine> _instance = new(() => new GameEngine());
        public static GameEngine Instance => _instance.Value;

        private GameState _state = new();
        private List<Scenario> _scenarios = new();

        private GameEngine()
        {
            _scenarios = ScenarioLoader.LoadFromJson("FinalGame/Scenarios.json");
        }

        public async Task StartGameAsync()
        {
            ConsoleHelper.PrintDivider();

            foreach (var method in typeof(ScenarioLoader).GetMethods())
            {
                var attr = method.GetCustomAttribute<ScenarioAttribute>();
                if (attr != null)
                {
                    Console.WriteLine($"[Scenario Pack: {attr.Tag}]\n");
                }
            }

            await Task.Delay(500);

            foreach (var scenario in _scenarios)
            {
                ConsoleHelper.PrintDivider();
                Console.WriteLine(scenario.Text);

                for (int i = 0; i < scenario.Options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {scenario.Options[i].Description}");
                }

                Console.Write("Choose an option (1 or 2): ");
                string? input = Console.ReadLine();
                int choice;

                while (!int.TryParse(input, out choice) || choice < 1 || choice > scenario.Options.Count)
                {
                    Console.Write("Invalid choice. Try again (1 or 2): ");
                    input = Console.ReadLine();
                }

                scenario.Options[choice - 1].ApplyEffect(_state);
                Console.WriteLine($"\nCurrent Stats: {_state}");

                if (_state.Health <= 0 || _state.Wealth <= 0 || _state.Popularity <= 0)
                {
                    Console.WriteLine("\nGame Over! One of your stats dropped to zero.");
                    return;
                }

                await Task.Delay(500);
            }

            Console.WriteLine("\nYou survived all the scenarios!");
        }
    }
}
