using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using static UsefullThings.Things;

namespace LazyParser
{
    public class ExceptionLazyParserExternal : Exception
    {
        public ExceptionLazyParserExternal()
        {
        }

        public ExceptionLazyParserExternal(string message) : base(message)
        {
        }

        public ExceptionLazyParserExternal(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExceptionLazyParserExternal(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class Command
    {
        public enum EmptyParameter { Throw, Ignore, Default }

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

            public bool DoubleDensity;
        }

        public Command(string command, EmptyParameter emptyParameterAction)
        {
            EmptyParameterAction = emptyParameterAction;
            ParseCommand(command);
        }

        public Command(string command) : this(command, EmptyParameter.Default)
        {

        }

        private readonly EmptyParameter EmptyParameterAction;

        public string CommandName { get; private set; }
        // First word.

        public string[] Arguments { get; private set; }
        // Words without "-".

        private List<ParamData> LocalParamList { get; set; } = new List<ParamData>();

        public ParamData[] Parameters
        {
            get
            {
                return LocalParamList.ToArray();
            }
        }
        // Words with "-".  
        // LOL, i know.

        private void ParseCommand(string command)
        {
            string cmd = command;
            string originalCommand = command;
            List<string> args = new List<string>();
            List<string> param = new List<string>();
            string name = cmd.SubstringByChar(0, ' ', true, out cmd);

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
                    args.Add(cmd.SubstringByChar('"', '"', true, out cmd));
                    i = -1;
                }
                else if (cmd[i] == '\'')
                {
                    args.Add(cmd.SubstringByChar('\'', '\'', true, out cmd));
                    i = -1;
                }
                else if (cmd[i] == '-')
                {
                    ParamData tempParam = new ParamData();
                    Int16 DDAddend = 1;
                    // "-" shift
                    bool emptyParam = (i + 2 > cmd.Length || cmd[i + 1] == ' ');
                    bool emptyDDParam = false;
                    if (!emptyParam && i + 2 <= cmd.Length && cmd[i+1] == '-')
                    {
                        emptyDDParam = (i + 3 > cmd.Length || cmd[i + 2] == ' ');
                    }

                    if (emptyParam || emptyDDParam)
                    {
                        switch (EmptyParameterAction)
                        {
                            case EmptyParameter.Throw:
                                throw new ExceptionLazyParserExternal("Empty or whitespaces param");
                            case EmptyParameter.Ignore:
                                cmd = cmd.Remove(i, emptyDDParam ? 2 : 1);
                                break;
                            case EmptyParameter.Default:
                                LocalParamList.Add(new ParamData { Name = emptyDDParam ? "--" : "-", Data = new string[0] });
                                cmd = cmd.Remove(i, emptyDDParam ? 2 : 1);
                                break;
                            default:
                                throw new Exception("Unknown action");
                        }
                        i--;
                        continue;
                    }

                    if (cmd[i + 1] == '-')
                    {
                        tempParam.DoubleDensity = true;
                        DDAddend++;
                    }
                    string data;

                    if (cmd.IndexOf("-", i + DDAddend) != -1)
                    {
                        // other "-" after that "-"
                        data = cmd.Substring(i + DDAddend, cmd.IndexOf("-", i + DDAddend) - i - (DDAddend + 1));
                        cmd = cmd.Remove(i, cmd.IndexOf("-", i + DDAddend) - i - DDAddend + 1);
                        i = -1;
                    }
                    else
                    {
                        data = cmd.Substring(i + DDAddend, cmd.Length - i - DDAddend);
                        cmd = cmd.Remove(i, cmd.Length - i);
                        i = -1;
                    }

                    List<string> dataList = new List<string>(data.Split(' '));
                    dataList.RemoveAll(x => string.IsNullOrWhiteSpace(x));
                    tempParam.Name = dataList[0];
                    tempParam.Data = dataList.Skip(1).ToArray();
                    LocalParamList.Add(tempParam);
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

            tempItemPos.Clear();
            CommandName = name;

        }
    }
}
