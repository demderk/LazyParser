using System;
using System.Collections.Generic;
using System.Text;

namespace LazyParser
{
    class CommandParameter
    {
        CommandParameter(string name, params string[] data)
        {
            Name = name;
            Data = data;
        }

        public readonly string Name;

        public readonly string[] Data;
    }
}
