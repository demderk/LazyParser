using System;
using System.Collections.Generic;
using System.Text;

namespace LazyParser
{
    interface IExecutable
    {
        void Execute(string[] arguments, Option[] options);

        string Help { get; }

        string Name { get; }

    }
}
