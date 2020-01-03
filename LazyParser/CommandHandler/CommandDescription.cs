using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LazyParser.CommandHandler
{
    public interface ICommandDescription
    {
        public string Name { get; }

        public string Help { get; }

        public IReadOnlyCollection<OptionDescription> PermissibleOptions { get; }

        public void Execute(string[] arguments, OptionCollection options);
    }
}
