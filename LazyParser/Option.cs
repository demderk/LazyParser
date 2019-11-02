using System;
using System.Collections.Generic;
using System.Text;

namespace LazyParser
{
    public class Option
    {
        public Option(string option)
        {
            string[] optionInfo = option.Split(' ');
            Name = optionInfo[0];
            Data = string.Join(' ',optionInfo[1..]);
        }

        public string Name { get; }

        public bool IsFullName { get; }

        public string Data { get; }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(IsFullName ? "--" : "-");
            result.Append(Name);
            result.Append(Data.Length > 0 ? $" {Data}" : "");
            return result.ToString();
        }
    }
}
