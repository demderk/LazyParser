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

        private List<string> Arguments { get; } = new List<string>();

        private List<CommandOption> Options { get; } = new List<CommandOption>();


        private void Parser(string inputCommand)
        {
            string tempCommand = string.Copy(inputCommand);
            Regex optionsRegex = new Regex(@"\s+[-][^\s-]+(\s[""].+?[""]|(\s[^-][^\s]+)+|\s*?)");
            Regex fullOptionsRegex = new Regex(@"\s+[-]{2}[^\s]+(\s[""].+?[""]|(\s[^-][^\s]+)+|\s*?)");
            Regex argumentsRegex = new Regex(@"\s([^""'\s]+|[""'][^""']+[""'])\s*?");
            Regex quotesRegex = new Regex(@"([""].+?[""]|['].+?['])\s*?");

            string[] quotesArray = quotesRegex.Matches(tempCommand).Cast<Match>().Select(x => x.Value).ToArray();
            tempCommand = quotesRegex.Replace(tempCommand, ((char)0x001A).ToString());
            // Clearing second spaces.
            while (tempCommand.Contains("  "))
            {
                tempCommand = tempCommand.Replace("  ", " ");
            }
            for (int i = 0; i < quotesArray.Length; i++)
            {
                int index = tempCommand.IndexOf((char)0x001A);
                tempCommand = tempCommand.Remove(index, 1).Insert(index, quotesArray[i]);
            }
            string[] problemQuotes = Array.Empty<string>();
            // Finding problem quotes. e.g. " -s simple ".
            if (quotesArray.Length > 0)
            {
                problemQuotes = FindProblemQuotes(quotesRegex.Matches(tempCommand), tempCommand);
            }
            foreach (var item in problemQuotes)
            {
                tempCommand = tempCommand.Replace(item, "");
            }
            tempCommand = tempCommand.Trim(' ');

            Name = new Regex(@"\S+\s*?").Match(tempCommand).Value.Trim(' ');
            Options.AddRange(optionsRegex.Matches(tempCommand).Cast<Match>().Select(x => new CommandOption(x.Value.Trim(new char[] { ' ', '-' }))));
            Options.AddRange(fullOptionsRegex.Matches(tempCommand).Cast<Match>().Select(x => new CommandOption(x.Value.Trim(new char[] { ' ', '-' }), true)));
            tempCommand = optionsRegex.Replace(tempCommand, "");
            tempCommand = fullOptionsRegex.Replace(tempCommand, "");

            SortedDictionary<int, string> tempArgumentsDictionary = new SortedDictionary<int, string>();
            string[] simpleArguments = argumentsRegex.Matches(tempCommand).Cast<Match>().Select(x => x.Value.Trim(' ')).Except(new string[] { "--", "&&", "||" }).ToArray();
            foreach (var item in simpleArguments)
            {
                tempArgumentsDictionary.Add(inputCommand.IndexOf(item), item);
            }
            foreach (var item in problemQuotes)
            {
                tempArgumentsDictionary.Add(inputCommand.IndexOf(item), item);
            }
            Arguments.AddRange(tempArgumentsDictionary.Values.ToArray());
        }

        private string[] FindProblemQuotes(MatchCollection matches, string command)
        {
            List<string> result = new List<string>();
            foreach (Match item in matches)
            {
                var value = item.Value;
                for (int i = (item.Index - 1); i >= 0; i--)
                {
                    if (i == (item.Index - 1))
                    {
                        if (command[i] != ' ')
                        {
                            break;
                        }
                        else continue;
                    }
                    if (command[i] == '-')
                    {
                        break;
                    }
                    else if (command[i] == ' ')
                    {
                        result.Add(value);
                        break;
                    }
                    else if (i == 0)
                    {
                        result.Add(value);
                        break;
                    }
                }
            }
            return result.ToArray();
        }

        public string[] GetOptionSource()
        {
            return GetOptions().Select(x => x.ToString()).ToArray();
        }

        public string[] GetArguments()
        {
            return Arguments.ToArray();
        }

        public CommandOption[] GetOptions()
        {
            return Options.ToArray();
        }

        public bool HasArgument(string argument)
        {
            return Arguments.Contains(argument);
        }

        public string GetArgument(string argument)
        {
            return HasArgument(argument) ? argument : null;
        }

        public bool HasOption(string optionName)
        {
            return Options.Select(x => x.Name).Contains(optionName);
        }

        public bool HasOption(string optionName, bool isFull)
        {
            foreach (var item in Options)
            {
                if (item.Name == optionName && item.IsFullName == isFull)
                {
                    return true;
                }
            }
            return false;
        }

        public CommandOption[] GetOptions(string optionName)
        {
            List<CommandOption> result = new List<CommandOption>();
            foreach (var item in Options)
            {
                if (item.Name == optionName)
                {
                    result.Add(item);
                }
            }
            return result.ToArray();
        }

        public CommandOption[] GetOptions(string optionName, bool isFull)
        {
            List<CommandOption> result = new List<CommandOption>();
            foreach (var item in Options)
            {
                if (item.Name == optionName && item.IsFullName == isFull)
                {
                    result.Add(item);
                }
            }
            return result.ToArray();
        }

        public CommandOption GetFirstOption(string optionName)
        {
            return GetOptions(optionName).FirstOrDefault();
        }

        public CommandOption GetFirstOption(string optionName, bool isFull)
        {
            return GetOptions(optionName, isFull).FirstOrDefault();
        }

    }
}
