using RockyHockey.Common;
using RockyHockey.MoveCalculationFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandHandler = new CommandHandler(new ConsoleLogger());

            // Intro
            Console.Write(
                $"===================================================={Environment.NewLine}" +
                $"                  Rocky.Hockey                      {Environment.NewLine}" +
                $"===================================================={Environment.NewLine}");

            Console.Write(
                $"{Environment.NewLine}" +
                $"Available commands:{Environment.NewLine}" +
                $"{Environment.NewLine}");

            int commandNumber = 0;
            foreach (string commandName in Enum.GetNames(typeof(ConsoleCommands)))
            {
                Console.WriteLine($"{commandNumber} : {commandName}");
                commandNumber++;
            }
            Console.WriteLine();

            while (true)
            {
                var command = Console.ReadLine();
                commandHandler.HandleInput(command);
            }
        }
    }
}
