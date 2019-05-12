using System;
using LazyParser;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "LazyParser Console";
            while (true)
            {
                try
                {
                    Console.Write("> ");
                    string cin = Console.ReadLine();
                    if (cin == "cls")
                    {
                        Console.Clear();
                        continue;
                    }
                    LazyParser.Command inputCommand = new Command(cin, Command.EmptyParameter.Default);
                    // Default - add EP, Ignore - ignore EP, Throw - throw when EP  (EP - empty parameter (eg. "-" , "--"))
                    Console.WriteLine($"Command name: {inputCommand.CommandName}");
                    Console.WriteLine("Arguments:");
                    foreach (var item in inputCommand.Arguments)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("Params:");
                    foreach (var item in inputCommand.Parameters)
                    {
                        Console.WriteLine($"Name: {item.Name} |==| Data: {item.DataString} |==| Double density: {item.DoubleDensity}");
                    }
                    
                }
                catch (ExceptionLazyParserExternal ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = default;
                }
            }
        }
    }
}
