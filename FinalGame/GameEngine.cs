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
        private int _turn = 1;
        private readonly Random _random = new();

        private GameEngine()
        {
            _scenarios = ScenarioLoader.LoadFromJson("Scenarios.json");
        }

        public async Task StartGameAsync()
        {
            ConsoleHelper.PrintDivider();
            Console.WriteLine("Loading scenarios...\n");

            foreach (var method in typeof(ScenarioLoader).GetMethods())
            {
                var attr = method.GetCustomAttribute<ScenarioAttribute>();
                if (attr != null)
                {
                    Console.WriteLine($"[Scenario Pack: {attr.Tag}]\n");
                }
            }

            await Task.Delay(1000);
            ConsoleHelper.PrintDivider();

            foreach (var scenario in _scenarios)
            {
                Console.WriteLine($"\nTurn #{_turn++}");
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

                try
                {
                    scenario.Options[choice - 1].ApplyEffect(_state);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] {ex.Message}");
                }

                Console.WriteLine("\n" + _state);

                DisplayStatWarnings();
                DisplayMiniEvent();

                if (IsGameOver(out string reason))
                {
                    Console.WriteLine("\nGame Over!");
                    Console.WriteLine(reason);
                    break;
                }

                await Task.Delay(750);
            }

            Console.WriteLine("\nFinal Summary:");
            Console.WriteLine($"You survived {_turn - 1} turns.");
            Console.WriteLine(_state);
            ConsoleHelper.PrintDivider();

            await SaveGameAsync();
            Console.WriteLine("Game session saved.");
        }

        private bool IsGameOver(out string reason)
        {
            if (_state.Health <= 0)
            {
                reason = "You collapsed in the throne room. A physician arrived too late.";
                return true;
            }
            if (_state.Wealth <= 0)
            {
                reason = "Your kingdom went bankrupt. The nobles fled.";
                return true;
            }
            if (_state.Popularity <= 0)
            {
                reason = "An angry mob stormed the palace gates.";
                return true;
            }

            reason = string.Empty;
            return false;
        }

        private void DisplayStatWarnings()
        {
            if (_state.Health < 20)
                Console.WriteLine("⚠️ Your health is dangerously low!");
            if (_state.Wealth < 20)
                Console.WriteLine("⚠️ Your kingdom's wealth is critically low!");
            if (_state.Popularity < 20)
                Console.WriteLine("⚠️ The people are starting to turn against you!");
        }

        private void DisplayMiniEvent()
        {
            var miniEvents = new List<string>
            {
                "A crow flies overhead. Ominous.",
                "You hear whispers in the court halls.",
                "A beggar blesses your name in the streets.",
                "Lightning strikes a tree just outside the castle.",
                "A guard sneezes... twice. Weird."
            };

            Console.WriteLine("Side Note: " + miniEvents[_random.Next(miniEvents.Count)]);
        }

        private async Task SaveGameAsync()
        {
            try
            {
                string json = JsonSerializer.Serialize(_state);
                await File.WriteAllTextAsync("gamestate.json", json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Could not save game: " + ex.Message);
            }
        }
    }
}
