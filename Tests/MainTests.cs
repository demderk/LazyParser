using System;
using LazyParser;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace LazyParser.Tests
{
    public class MainTests
    {
        [Theory]
        [ClassData(typeof(CommandsForTests))]
        private void CommandsTest(string inputCmd, string inputName, string inputArguments, string inputOptions)
        {
            string[] arguments = inputArguments.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            string[] parameters = inputOptions.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToArray();

            Command cmd = new Command(inputCmd);

            Assert.Equal(inputName, cmd.Name);
            Assert.Equal(arguments, cmd.GetArguments());
            Assert.Equal(parameters, cmd.GetOptionSource());
        }
    }

    public class CommandsForTests : IEnumerable<object[]>
    {

        // *** MASK ***
        // InputCommand, Command name, Arguments, Options.
        // Tip: Arguments and Options split are ",".

        public List<object[]> AllTests { get; set; } = new List<object[]>()
        {
            new object[] {"help","help","",""},
            new object[] { @"cd D:\OpenServer", "cd", @"D:\OpenServer", "" },
            new object[] {"edit -b chrome -au aimp -o libre", "edit", "", "-b chrome,-au aimp,-o libre"},
            new object[] {"git commit", "git", "commit", ""},
            new object[] {@"bind l ""sv_cheats 1""", "bind", @"l,""sv_cheats 1""", ""},
            new object[] {"git commit ban", "git", "commit,ban", ""},
            new object[] {" git commit    ban ", "git", "commit,ban", ""},
            new object[] {"   git    commit    ban", "git", "commit,ban", ""},
            new object[] {" git   commit     ban    ", "git", "commit,ban", ""},
            new object[] {"git commit -ban", "git", "commit", "-ban"},
            new object[] {"git commit 123456789", "git", "commit,123456789", ""},
            new object[] {@"git commit -a -s -m ""simple test""", "git", "commit", "-a,-s,-m \"simple test\""},
            new object[] {@"git commit ""Penaut butter jelly time"" -a -s -m ""simple test""", "git", @"commit,""Penaut butter jelly time""", "-a,-s,-m \"simple test\""},
            new object[] {@"git commit                   ""Penaut butter jelly time""    -a -s -m                   ""simple test""", "git", @"commit,""Penaut butter jelly time""", "-a,-s,-m \"simple test\""},
            new object[] {@"git commit                   ""   Penaut   butter jelly time""    -a -s -m                   ""   simple test  """, "git", @"commit,""   Penaut   butter jelly time""", "-a,-s,-m \"   simple test  \""},
            new object[] {@"module migrate -f 192.168.0.1 -t 192.168.31.74", "module", "migrate", "-f 192.168.0.1,-t 192.168.31.74"},
            new object[] {@"module migrate -f 192.168.0.1 -t 192.168.31.74 -- ineedthisargument", "module", "migrate,ineedthisargument", "-f 192.168.0.1,-t 192.168.31.74"},
            new object[] {@"test "" -t big bad     -boy "" -t", "test", @""" -t big bad     -boy """, "-t"},
            new object[] {@"remote send 'git commit -aS -m "" git is beautiful""' -d","remote",@"send,'git commit -aS -m "" git is beautiful""'","-d"},
            new object[] {@"sudo apt-get install cowsay","sudo","apt-get,install,cowsay",""},
            new object[] {@"thereargs argument1 argument2 argument3","thereargs","argument1,argument2,argument3",""},
            new object[] {@"quotes ""hello im quote one"" ""hello im -p -r -o -b -l -e -m quote""","quotes",@"""hello im quote one"",""hello im -p -r -o -b -l -e -m quote""",""},
             new object[] {"ip -o -f inet addr show","ip","","-o,-f inet addr show,"},
            // new object[] {},
        };

        public IEnumerator<object[]> GetEnumerator() => AllTests.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
