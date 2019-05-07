using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LazyParser;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]

        public void Command2Test1()
        {
            string testingString = "executecmd \"explorer c:\\\\\"";

            string ename = "go";
            string[] args = new string[] { "data from", "analog" };
            string[] param = new string[] { "t login" };

            Command cmd = new Command(testingString);

            Assert.AreEqual(ename, cmd.CommandName);
            CollectionAssert.AreEqual(args, cmd.Arguments);
            CollectionAssert.AreEqual(param, cmd.Parameters);
        }

    }



}
