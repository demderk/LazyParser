using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace LazyParser.CommandHandler
{
    public class HelpCommand : ICommandDescription
    {
        public CommandProcessor Processor { get; }

        public HelpCommand(CommandProcessor processor)
        {
            Processor = processor;
        }

        public string Name => "help";

        public string Help => "Get help.";

        public IReadOnlyCollection<OptionDescription> PermissibleOptions => new List<OptionDescription>() 
        {

        }.AsReadOnly();

        public void Execute(string[] arguments, OptionCollection options)
        {
            if (arguments.Length > 0)
            {
                List<ICommandDescription> optionDescriptions = new List<ICommandDescription>();
                foreach (var item in arguments)
                {
                    optionDescriptions.AddRange(Processor.Executables.Where(x => x.Name == item));
                }
                if (optionDescriptions.Count > 0)
                {
                    Console.WriteLine(String.Format("{0} {1,32} {2,32}\n", "Name", "Permissible options", "Help"));
                    foreach (var item in optionDescriptions)
                    {
                        Console.WriteLine(String.Format("{0} {1,32} {2,32}", item.Name, string.Join(',', item.PermissibleOptions.Select(x => x.Name)), item.Help));
                    }
                }
                else
                {
                    throw new CommandProcessorException("Not found.");
                }
            }
            else
            {
                Console.WriteLine(String.Format("{0} {1,32}\n", "Name", "Help"));
                foreach (var item in Processor.Executables)
                {
                    Console.WriteLine(String.Format("{0} {1,32}", item.Name, item.Help));
                }
            }
        }
    }
}
