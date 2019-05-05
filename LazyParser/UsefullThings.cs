using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsefullThings
{
    public static class Things
    {
        static public string SubstringByWord(this string where, string from, string to)
        {
            string str = where;
            int wantedSymbolIndex = 0;
            string workingOn = from;
            int resultStartPoint = -1;
            for (int i = 0; i < str.Length; i++)
            {
                var item = str[i];
                if (item == workingOn[wantedSymbolIndex])
                {
                    if ((workingOn.Length - 1) > wantedSymbolIndex)
                    {
                        wantedSymbolIndex++;
                    }
                    else if ((workingOn.Length - 1) == wantedSymbolIndex)
                    {
                        if (resultStartPoint == -1)
                        {
                            if (str.Length - 1 >= i + 1)
                            {
                                wantedSymbolIndex = 0;
                                workingOn = to;
                                resultStartPoint = i + 1;
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            var strs = str.Substring(resultStartPoint, (i - workingOn.Length - 1) - resultStartPoint + 2);
                            return strs;
                        }
                    }
                }
            }
            return null;
        }

        static public string SubstringByChar(this string where, char from, char to)
        {
            string str = where;
            bool fromSymbolFound = false;
            char findSymbol = from;
            StringBuilder returnString = new StringBuilder();
            foreach (char item in str)
            {
                if (item == findSymbol)
                {
                    if (!fromSymbolFound)
                    {
                        fromSymbolFound = true;
                        findSymbol = to;
                    }
                    else
                    {
                        return returnString.ToString();
                    }
                }
                if (fromSymbolFound && item != from)
                {
                    returnString.Append(item);
                }
            }
            return null;
        }

        static public string SubstringByChar(this string where, char from, char to, bool remove,out string formatedString)
        {
            if (!remove)
            {
                SubstringByChar(where, from, to);
            }
            string originalWhere = where;
            int startOn = -1;
            int length = 0;
            bool fromSymbolFound = false;
            char findSymbol = from;
            StringBuilder returnString = new StringBuilder();
            for (int i = 0; i < where.Length; i++)
            {
                if (where[i] == findSymbol)
                {
                    if (!fromSymbolFound)
                    {
                        fromSymbolFound = true;
                        findSymbol = to;
                        startOn = i;
                        length++;
                    }
                    else
                    {
                        where = where.Remove(startOn, ++length);
                        formatedString = where;
                        return returnString.ToString();
                    }
                }
                if (fromSymbolFound && where[i] != from)
                {
                    length++;
                    returnString.Append(where[i]);
                }
            }
            formatedString = originalWhere;
            return null;
        }

        static public string SubstringByChar(this string where, int from, char to)
        {
            string str = where;
            char findSymbol = str[from];
            StringBuilder returnString = new StringBuilder();
            for (int i = from; i < str.Length; i++)
            {
                if (str[i] == to)
                {
                    return returnString.ToString();
                }
                returnString.Append(str[i]);
            }
            return null;
        }

        static public string SubstringByChar(this string where, int from, char to, bool remove,out string formatedString)
        {

            if (!remove)
            {
                SubstringByChar(where, from, to);
            }
            string originalWhere = where;
            char findSymbol = where[from];
            StringBuilder returnString = new StringBuilder();
            for (int i = from; i < where.Length; i++)
            {
                if (where[i] == to)
                {
                    where = where.Remove(from, i);
                    formatedString = where;
                    return returnString.ToString();
                }
                returnString.Append(where[i]);
            }
            formatedString = originalWhere;
            return null;
        }

        static public string RemoveByChar(this string where, char from, char to, bool remove)
        {
            if (!remove)
            {
                SubstringByChar(where, from, to);
            }
            int startOn = -1;
            int length = 0;
            bool fromSymbolFound = false;
            char findSymbol = from;
            StringBuilder returnString = new StringBuilder();
            for (int i = 0; i < where.Length; i++)
            {
                if (where[i] == findSymbol)
                {
                    if (!fromSymbolFound)
                    {
                        fromSymbolFound = true;
                        findSymbol = to;
                        startOn = i;
                        length++;
                    }
                    else
                    {
                        return where.Remove(startOn, ++length);
                    }
                }
                if (fromSymbolFound && where[i] != from)
                {
                    length++;
                    returnString.Append(where[i]);
                }
            }
            return null;
        }

        static public int SubstrCount(this string str,string subString)
        {
            return str.Split(new string[] { subString }, StringSplitOptions.None).Count() - 1;
        }


    }
}
