using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace LazyParser
{
    public class Command
    {
        public Command(string cmd)
        {
            Parser(cmd);
        }

        public string Name { get; private set; }

        private List<string> arguments { get; } = new List<string>();
        public string[] Arguments => arguments.ToArray();

        private List<Option> options { get; } = new List<Option>();
        public Option[] Options => options.ToArray();

        public string[] OptionSource => Options.Select(x => x.ToString()).ToArray();

        private void Parser(string cmd)
        {
            string temp = string.Copy(cmd);
            Regex optionsRegex = new Regex(@"\s+[-]\w+(\s[""].+?[""]|\s[a-zA-z_0-9.,]+\s*?|\s*?)");
            Regex fullOptionsRegex = new Regex(@"\s+[-]{2}\w+(\s[""].+?[""]|\s[a-zA-z_0-9.,]+\s*?|\s*?)");
            Regex argumentsRegex = new Regex(@"\s([^""'\s-]+|[""'][^""']+[""'])\s*?");
            Regex quotesRegex = new Regex(@"([""].+?[""]|['].+?['])");

            //quotesRegex.Matches(cmd).Cast<Match>().Where(x => x.Value.Contains('-'));
            
            temp = temp.Trim(' ');
            Name = new Regex(@"\w+\s*?").Match(temp).Value.Trim(' ');
            options.AddRange(optionsRegex.Matches(temp).Cast<Match>().Select(x => new Option(x.Value.Trim(new char[] { ' ', '-' }))));
            temp = optionsRegex.Replace(temp, "");
            arguments.AddRange(argumentsRegex.Matches(temp).Cast<Match>().Select(x => x.Value.Trim(' ')));
        }

        public bool HasArgument(string argument)
        {
            return arguments.Contains(argument);
        }

        public bool HasOption(string option)
        {
            return options.Select(x => x.Name).Contains(option);
        }

    }
}
