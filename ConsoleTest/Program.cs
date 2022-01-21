using System;
using LazyParser;
using System.Collections.Generic;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Working...");

            // Simple command example.

            //while (true)
            //{
            //    string inputCommand = Console.ReadLine();
            //    Command command = new Command(inputCommand);
            //    GetInfo(command);
            //}

            // Simple command processor example.

            CommandProcessor commandProcessor = new CommandProcessor();
            SimpleClassExample simpleClass = new SimpleClassExample();
            commandProcessor.AddExecuteble(simpleClass);
            while (true)
            {
                commandProcessor.Invoke(Console.ReadLine() ?? "");
            }
        }

        // Simple command example.

        static void GetInfo(Command command)
        {
            Console.Write("Command name: ");
            Console.WriteLine(command.Name + "\n");

            Console.WriteLine("Command arguments: ");
            Console.WriteLine(String.Join(',', command.GetArguments()) + "\n");

            Console.Write("Command options");
            foreach (var item in command.GetOptions())
            {
                Console.WriteLine("Name: " + item.Name);
                Console.WriteLine("IsFullName: " + item.IsFullName);
                Console.WriteLine("Data: " + item.Data + "\n");

            }
            Console.WriteLine("-end-");
        }
    }
}
