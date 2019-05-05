//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace LazyParser
//{
//    public class CommandParse : Command
//    {

//        public CommandParse()
//        {

//        }
//        public CommandParse(string command)
//        {
//            Parse(command);
//        }

//        public void Parse(string command)
//        {

//            string cmdUnFiltred = command;
//            bool cmdUnFiltredQuotes = true;
//            while (command.IndexOf("  ") != -1)
//            {
//                command = command.Replace("  ", " ");
//            }

//            if ((command.LastIndexOf(" ") + 1) == command.Length)
//            {
//                command = command.Remove(command.LastIndexOf(" "));
//                cmdUnFiltred = cmdUnFiltred.Remove(cmdUnFiltred.LastIndexOf(" "));
//            }

//            if (command.IndexOf(" ") == 0)
//            {
//                command = command.Remove(0, 1);
//                cmdUnFiltred = cmdUnFiltred.Remove(0, 1);
//            }
//            List<string> result = new List<string>();
//            if (command.IndexOf(" ") != -1)
//            {

//                Name = command.Substring(0, command.IndexOf(" "));
//                string cmd = command.Substring(command.IndexOf(" "));
//                while (cmd.IndexOf(" ") != -1)
//                {
//                    if (cmd.IndexOf(" ", 1) != -1)
//                    {
//                        result.Add(cmd.Substring(1, cmd.IndexOf(" ", 1) - 1));
//                        cmd = cmd.Remove(0, cmd.IndexOf(" ", 1));
//                    }
//                    else
//                    {
//                        if (cmd.Replace(" ", "") != "")
//                        {
//                            result.Add(cmd.Replace(" ", ""));
//                        }
//                        cmd = cmd.Remove(0);
//                    }
//                }
//                List<int> indexesForRemove = new List<int>();
//                for (int i = 0; i < result.Count; i++)
//                {
//                    if (result[i].IndexOf("-") == 0)
//                    {
//                        for (int i2 = 1; i2 < result.Count - i; i2++)
//                        {
//                            if (result[i + i2].IndexOf("-") != 0)
//                            {
//                                result[i] = result[i] + " " + result[i + i2];
//                                indexesForRemove.Add(i + i2);
//                            }
//                            else
//                            {
//                                break;
//                            }
//                        }
//                    }
//                }
//                indexesForRemove.Sort();
//                indexesForRemove.Reverse();
//                foreach (int item in indexesForRemove)
//                {
//                    result.RemoveAt(item);
//                }
//                indexesForRemove.Clear();

//                string cmdBuffer = cmdUnFiltredQuotes ? cmdUnFiltred : command;

//                for (int i = 0; i < result.Count; i++)
//                {
//                    if (result[i].Contains("\""))
//                    {
//                        int sPos = cmdBuffer.IndexOf("\"");
//                        int ePos = cmdBuffer.IndexOf("\"", cmdBuffer.IndexOf("\"") + 1) + 1;
//                        if (sPos == -1 || ePos == 0)
//                        {
//                            break;
//                        }

//                        int strLength = ePos - sPos;
//                        string strresult = cmdBuffer.Substring(sPos, strLength);
//                        int onarrend = 0;

//                        for (int i2 = i + 1; i2 < result.Count; i2++)
//                        {
//                            if (result[i2].Contains("\""))
//                            {
//                                if (result[i2].IndexOf("\"") + 1 != result[i2].Length)
//                                {
//                                    result[i2] = result[i2].Substring(result[i2].IndexOf("\"") + 1, result[i2].Length - 1 - result[i2].IndexOf("\"")).Replace(" ", "");
//                                    onarrend = i2 - 1;
//                                    break;
//                                }
//                                else
//                                {
//                                    onarrend = i2;
//                                    break;
//                                }
//                            }
//                        }



//                        for (int i2 = i + 1; i2 <= onarrend; i2++)
//                        {
//                            indexesForRemove.Add(i2);
//                        }
//                        result[i] = strresult;
//                        cmdBuffer = cmdBuffer.Remove(sPos, strLength);
//                        indexesForRemove.Sort();
//                        indexesForRemove.Reverse();
//                        foreach (int item in indexesForRemove)
//                        {
//                            result.RemoveAt(item);
//                        }
//                        indexesForRemove.Clear();

//                    }
//                }

//                List<string> args = new List<string>();
//                List<string> param = new List<string>();

//                foreach (var item in result)
//                {
//                    if (item.IndexOf("-") == 0)
//                    {
//                        param.Add(item);
//                    }
//                    else
//                    {
//                        args.Add(item);
//                    }
//                }

//                Args = args.ToArray();
//                Params = param.ToArray();
//            }
//            else
//            {
//                Name = command;
//            }
//        }
//    }
//}
