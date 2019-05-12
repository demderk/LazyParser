using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LazyParser;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        // [TestMethod]
        //public void CommandTest()
        //{
        //    string testingString = "";

        //    string name = "";
        //    string[] args = new string[] { };
        //    Command.ParamData[] param = new Command.ParamData[] {};

        //    Command cmd = new Command(testingString);

        //    Assert.AreEqual(name, cmd.CommandName);
        //    CollectionAssert.AreEqual(args, cmd.Arguments);
        //    CollectionAssert.AreEqual(param, cmd.Parameters);
        //}

        public void CommandTest()
        {
            string testingString = "executecmd \"explorer c:\\\\\"";

            string name = "executecmd";
            string[] args = new string[] { "explorer c:\\\\" };
            Command.ParamData[] param = new Command.ParamData[0];

            Command cmd = new Command(testingString);

            Assert.AreEqual(name, cmd.CommandName);
            CollectionAssert.AreEqual(args, cmd.Arguments);
            CollectionAssert.AreEqual(param, cmd.Parameters);
        }

        [TestMethod]
        public void CommandTest1()
        {
            string testingString = "git commit -a -S -m \"Testing success\"";

            string name = "git";
            string[] args = new string[] { "commit" };
            Command.ParamData[] param = new Command.ParamData[] 
            {
                new Command.ParamData { Name="a",Data=new string[0] },
                new Command.ParamData { Name="S",Data=new string[0] },
                new Command.ParamData { Name="m",Data=new string[] { "Testing", "success" } }
            };

            Command cmd = new Command(testingString);

            Assert.AreEqual(name, cmd.CommandName);
            CollectionAssert.AreEqual(args, cmd.Arguments);
            CollectionAssert.AreEqual(param, cmd.Parameters);
        }
    }



}
