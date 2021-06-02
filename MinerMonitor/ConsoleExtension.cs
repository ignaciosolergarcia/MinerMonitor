using System;

namespace MinerMonitor
{
    static class ConsoleExtension
    {
        public static void WriteLineWithColorAndRestorePrevious(ConsoleColor color, string text, params object[] args) 
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text, args);
            Console.ForegroundColor = previousColor;
        }

        public static void WriteWithColorAndRestorePrevious(ConsoleColor color, string text, params object[] args)
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text, args);
            Console.ForegroundColor = previousColor;
        }
    }
}
