using System;

namespace FinalGame
{
    public static class ConsoleHelper
    {
        public static void PrintTitle(string text)
        {
            Console.WriteLine("\n" + new string('=', text.Length + 8));
            Console.WriteLine("  " + text);
            Console.WriteLine(new string('=', text.Length + 8) + "\n");
        }

        public static void PrintDivider()
        {
            Console.WriteLine(new string('-', 40));
        }
    }
}

