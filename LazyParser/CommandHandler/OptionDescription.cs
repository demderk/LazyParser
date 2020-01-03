using System;
using System.Collections.Generic;
using System.Text;

namespace LazyParser.CommandHandler
{
    public class OptionDescription
    {
        public string Name { get; }

        public string ShortName { get; }

        public bool Required { get; }

        public OptionDescription(string name, string shortName)
        {
            Name = name;
            ShortName = shortName;
        }

        public OptionDescription(string name, string shortName, bool required)
        {
            Name = name;
            ShortName = shortName;
            Required = required;
        }
    }
}
