using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LazyParser
{
    class CommandProcessor
    {
        private List<IExecutable> Executables => new List<IExecutable>();


        public CommandProcessor()
        {

        }

        public CommandProcessor(params IExecutable[] executables)
        {
            Executables.AddRange(executables);
        }

        public void AddExecuteble(IExecutable executable)
        {
            Executables.Add(executable);
        }

        public void Invoke(Command command)
        {
            foreach (var item in Executables.Where(x => x.Name == command.Name))
            {
                item.Execute(command.GetArguments(), command.GetOptions());
            }
        }
    }
}
