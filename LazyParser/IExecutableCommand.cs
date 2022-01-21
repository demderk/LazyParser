using System;
using System.Collections.Generic;
using System.Text;

namespace LazyParser
{
    public interface IExecutableCommand
    {
        void CommandExecute(string[] arguments, CommandOption[] options);

        string CommandHelp { get; }

        string CommandName { get; }

    }
}
