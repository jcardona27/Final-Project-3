using System;
using System.Threading.Tasks;

namespace FinalGame
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ConsoleHelper.PrintTitle("Welcome to Reigns: Console Edition!");
            Console.WriteLine("Rule wisely. Survive as long as you can.\n");

            await GameEngine.Instance.StartGameAsync();
        }
    }
}
