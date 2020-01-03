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
        [Fact]
        public void Test1() 
        {
            
        }

        [Theory]
        [ClassData(typeof(CommandsForTests))]
#pragma warning disable IDE0051 // Remove unused private members
        void CommandsTest(string inputCmd, string inputName, string inputArguments, string inputOptions)
#pragma warning restore IDE0051 // Remove unused private members
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
            new object[] {"sudo chown -R pi:www-data /var/www/html","sudo","chown", "-R pi:www-data /var/www/html"},
            new object[] { "wget -O check_apache.html http://127.0.0.1", "wget", "","-O check_apache.html http://127.0.0.1"},
            new object[] { @"echo ""<?php phpinfo ();?>"" > /var/www/html/index.php","echo",@"""<?php phpinfo ();?>"",>,/var/www/html/index.php",""},
            new object[] { "sudo mysql --user=root","sudo","mysql","--user=root"},
            new object[] { "ddtest --full-argument-name","ddtest","","--full-argument-name"},
            new object[] { "sudo python web_control.py", "sudo", "python,web_control.py", ""},
            new object[] {@"sudo date --set=""30 December 2013 10:00:00""","sudo","date",@"--set=""30 December 2013 10:00:00"""},
            new object[] {@"ln -s /var/www/ ~/www","ln","","-s /var/www/ ~/www"},
            new object[] {"apt-cache search nvidia-[0-9]","apt-cache","search,nvidia-[0-9]",""},
            new object[] {"sudo add-apt-repository ppa:graphics-drivers","sudo","add-apt-repository,ppa:graphics-drivers",""},
            new object[] { "sudo apt-get install nvidia-XYXYX", "sudo", "apt-get,install,nvidia-XYXYX", ""},
            new object[] { "dpkg -L nvidia-driver-390", "dpkg", "", "-L nvidia-driver-390"},
            new object[] {"sudo chown -R $USER:$USER /var/www/sampledomain.com/html","sudo","chown","-R $USER:$USER /var/www/sampledomain.com/html"},
           // new object[] {},
        };

        public IEnumerator<object[]> GetEnumerator() => AllTests.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
