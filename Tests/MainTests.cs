using System;
using LazyParser;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace LazyParser.Tests
{
    public class MainTests
    {
        [Theory]
        [InlineData("help", "help", "", "")]
        [InlineData(@"cd D:\OpenServer", "cd", @"D:\OpenServer", "")]
        [InlineData("edit -b chrome -au aimp -o libre", "edit", "", "-b chrome,-au aimp,-o libre")]
        [InlineData("git commit", "git", "commit", "")]
        [InlineData(@"bind l ""sv_cheats 1""", "bind", @"l,""sv_cheats 1""", "")]
        [InlineData("git commit ban", "git", "commit,ban", "")]
        [InlineData(" git commit    ban ", "git", "commit,ban", "")]
        [InlineData("   git    commit    ban", "git", "commit,ban", "")]
        [InlineData(" git   commit     ban    ", "git", "commit,ban", "")]
        [InlineData("git commit -ban", "git", "commit", "-ban")]
        [InlineData("git commit 123456789", "git", "commit,123456789", "")]
        [InlineData(@"git commit -a -s -m ""simple test""", "git", "commit", "-a,-s,-m \"simple test\"")]
        [InlineData(@"git commit ""Penaut butter jelly time"" -a -s -m ""simple test""", "git", @"commit,""Penaut butter jelly time""", "-a,-s,-m \"simple test\"")]
        [InlineData(@"module migrate -f 192.168.0.1 -t 192.168.31.74", "module", "migrate", "-f 192.168.0.1,-t 192.168.31.74")]
        [InlineData(@"module migrate -f 192.168.0.1 -t 192.168.31.74 -- ineedthisargument", "module", "migrate,ineedthisargument", "-f 192.168.0.1,-t 192.168.31.74")]
        [InlineData(@"test "" -t big bad     -boy "" -t", "test", @""" -t big bad     -boy """, "-t")]
        private void CommandsTest(string inputCmd, string inputName, string inputArguments, string inputOptions)
        {
            string[] arguments = inputArguments.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            string[] parameters = inputOptions.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToArray();

            Command cmd = new Command(inputCmd);
            
            Assert.Equal(inputName, cmd.Name);
            Assert.Equal(arguments, cmd.Arguments);
            Assert.Equal(parameters, cmd.OptionSource);
        }
    }
}
