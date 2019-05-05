using System;
using System.Collections.Generic;
using System.Text;

namespace LazyParser
{
    public class CommandOld
    {
        public CommandOld(string command)
        {
            Parse(command);
        }

        public string Name { get; protected set; }

        public string[] Args { get; protected set; }

        public string[] Params { get; protected set; }

        private void Parse(string command)
        {
            while (command.Contains("  "))
            {
                command.Replace("  ", "");
            }
        }
    }
}
