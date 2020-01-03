using System;
using System.Collections.Generic;
using System.Text;

namespace LazyParser.CommandHandler
{
    public class OptionData : OptionDescription
    {
        public string Data { get; }

        public OptionData(string name, string shortName, string data) : base(name, shortName)
        {
            Data = data;
        }

        public OptionData(OptionDescription option, string data) : base(option.Name, option.ShortName)
        {
            Data = data;
        }
    }
}
