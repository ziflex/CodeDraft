#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace CodeDraft.CLI.Helpers
{
    public static class ConsoleHelper
    {
        public static void Write(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void Error(string message)
        {
            Write(message, ConsoleColor.Red);
        }

        public static void Warning(string message)
        {
            Write(message, ConsoleColor.Yellow);
        }

        public static void Success(string message)
        {
            Write(message, ConsoleColor.Blue);
        }

        public static void Info(string message)
        {
            Write(message, Console.ForegroundColor);
        }

        public static string Ask(string message, IEnumerable<string> options)
        {
            bool hasOptions = false;
            bool complete = false;
            string answer = string.Empty;

            if (options != null && options.Any())
            {
                hasOptions = true;
            }

            {
                Console.WriteLine("{0} {1}", message, hasOptions ? string.Join(",", options) : string.Empty);
                answer = Console.ReadLine().Trim();

                if (hasOptions)
                {
                    complete =
                        options.Any(o => string.Compare(o, answer, StringComparison.InvariantCultureIgnoreCase) == 0);
                }
                else
                {
                    complete = !string.IsNullOrWhiteSpace(answer);
                }
            }
            while (!complete)
            {
                ;
            }
            return answer;
        }
    }
}