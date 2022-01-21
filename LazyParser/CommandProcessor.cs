using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LazyParser
{
    public class CommandProcessor
    {
        private List<IExecutableCommand> Executables = new List<IExecutableCommand>();

        public CommandProcessor()
        {

        }

        public CommandProcessor(params IExecutableCommand[] executables)
        {
            Executables.AddRange(executables);
        }

        public void AddExecuteble(IExecutableCommand executable)
        {
            Executables.Add(executable);
        }

        public void RemoveExecuteble(IExecutableCommand executable)
        {
            Executables.Remove(executable);
        }

        public IExecutableCommand[] GetExecutables()
        {
            return Executables.ToArray();
        }

        public void Invoke(Command command)
        {
            Executables.Where(x => x.CommandName == command.Name)
                       .FirstOrDefault()
                       ?.CommandExecute(command.GetArguments(), command.GetOptions());
        }

        public void Invoke(string str)
        {
            Command command = new Command(str);
            Invoke(command);
        }
    }
}
