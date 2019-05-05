using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static UsefullThings.Things;

namespace LazyParser
{

    public class Command
    {

        public struct ParamData
        {
            public string Name { get; internal set; }

            public string[] Data { get; internal set; }

            public string DataString
            {
                get
                {
                    return string.Join(" ", Data);
                }
            }
        }

        public Command(string command)
        {
            ParseCommand(command);
        }

        public string CommandName { get; private set; }
        // First word.

        public string[] Arguments { get; private set; }
        // Words without "-".

        public ParamData[] Parameters { get; private set; }
        // Words with "-".  
        // LOL i know.

        private void ParseCommand(string command)
        {
            string cmd = command;
            string originalCommand = command;
            List<string> args = new List<string>();
            List<string> param = new List<string>();
            string name = cmd.SubstringByChar(0, ' ', true,out cmd);

            if (cmd.SubstrCount("\\\"") % 2 == 0)
            {
                cmd = cmd.Replace("\\\"", ((char)0x001F).ToString());                
            }

            //Main algorithm 

            for (int i = 0; i < cmd.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(cmd))
                {
                    break;
                }
                if (cmd[i] == '"')
                {
                    args.Add(cmd.SubstringByChar('"', '"', true,out cmd));
                    i = -1;
                }
                else if (cmd[i] == '\'')
                {
                    args.Add(cmd.SubstringByChar('\'', '\'', true,out cmd));
                    i = -1;
                }
                else if (cmd[i] == '-')
                {
                    if (cmd.IndexOf("-", i + 1) != -1)
                    {
                        param.Add(cmd.Substring(i + 1, cmd.IndexOf("-", i + 1) - i - 2));
                        cmd = cmd.Remove(i, cmd.IndexOf("-", i + 1) - i - 1);
                        i = -1;
                    }
                    else
                    {
                        param.Add(cmd.Substring(i + 1, cmd.Length - i - 1));
                        cmd = cmd.Remove(i, cmd.Length - i);
                        i = -1;
                    }
                }
            }

            while (cmd.Contains("  "))
            {
                cmd = cmd.Replace("  ", " ");
            }

            cmd = cmd.Trim();

            if (!string.IsNullOrWhiteSpace(cmd))
            {
                args.AddRange(cmd.Split(' '));
            }

            // Finding and replacing temp \" symbol

            for (int i = 0; i < args.Count; i++)
            {
                if (args[i].Contains(((char)0x001F).ToString()))
                {
                    args[i] = args[i].Replace(((char)0x001F).ToString(), "\"");
                }
            }

            for (int i = 0; i < param.Count; i++)
            {
                if (param[i].Contains(((char)0x001F).ToString()))
                {
                    param[0] = param[0].Replace(((char)0x001F).ToString(), "\"");
                }
            }

            // Sorting

            List<(int pos, string dat)> tempItemPos = new List<(int pos, string dat)>();

            foreach (var item in args)
            {
                tempItemPos.Add((originalCommand.IndexOf(item), item));
            }

            tempItemPos = tempItemPos.OrderBy(x => x.pos).ToList();
            Arguments = tempItemPos.Select(x => x.dat).ToArray();
            tempItemPos.Clear();

            foreach (var item in param)
            {
                tempItemPos.Add((originalCommand.IndexOf(item), item));
            }

            // ParamData layout

            tempItemPos = tempItemPos.OrderBy(x => x.pos).ToList();
            List<ParamData> paramList = new List<ParamData>();
            foreach (var item in tempItemPos)
            {
                List<string> allData = new List<string>(item.dat.Split(' '));
                paramList.Add(new ParamData() { Name = allData[0], Data = allData.Skip(1).ToArray() });
            }
            Parameters = paramList.ToArray();
            tempItemPos.Clear();

            CommandName = name;

        }
    }
}
