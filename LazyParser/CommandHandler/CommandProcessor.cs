using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LazyParser.CommandHandler
{
    public class CommandProcessor
    {
        public List<ICommandDescription> Executables { get; } = new List<ICommandDescription>();


        public CommandProcessor()
        {

        }

        public CommandProcessor(params ICommandDescription[] executables)
        {
            Executables.AddRange(executables);
        }

        public void AddExecuteble(ICommandDescription executable)
        {
            Executables.Add(executable);

        }

        public void Invoke(Command command)
        {
            OptionCollection optionDatas = new OptionCollection();
            ICommandDescription foundcmd = Executables.Where(x => x.Name == command.Name).FirstOrDefault();
            if (foundcmd == null)
            {
                throw new CommandProcessorException("Command not found.");
            }
            List<string> requiredOptions = foundcmd.PermissibleOptions.Where(x => x.Required).Select(x => x.Name).ToList();
            Option[] commandOptions = command.GetOptions();
            foreach (var item in commandOptions)
            {
                OptionData foundedOption = null;
                var tempOption = foundcmd.PermissibleOptions.Where(x => (item.IsFullName ? x.Name : x.ShortName) == item.Name).FirstOrDefault();
                if (tempOption != null)
                {
                    foundedOption = new OptionData(tempOption, item.Data);
                }
                if (foundedOption != null)
                {
                    optionDatas.Add(foundedOption);
                    requiredOptions.Remove(foundedOption.Name);
                }
                else
                {
                    throw new CommandProcessorException($"Unknown option \"{item.Name}\"");
                }
            }
            if (requiredOptions.Count > 0)
            {
                throw new CommandProcessorException($"Options \"{string.Join(',',requiredOptions)}\" is required.");
            }
            foundcmd.Execute(command.GetArguments(), optionDatas);

        }
    }
}
