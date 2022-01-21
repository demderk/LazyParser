using System;
using LazyParser;

namespace ConsoleTest
{
    public class SimpleClassExample : IExecutableCommand
    {
        public SimpleClassExample() 
        {

        }

        private void TestVoid()
        {
            Console.WriteLine("Something special.");
        }

        public string CommandHelp => "Simple command help.";

        public string CommandName => "classcommand";

        public void CommandExecute(string[] arguments, CommandOption[] options)
        {
            TestVoid();
        }
    }
}
