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

        //[TestMethod]

        //public void Command2Test1()
        //{
        //    string testingString = "go \"data from\" analog -t login";

        //    string ename = "go";
        //    string[] args = new string[] { "data from", "analog" };
        //    string[] param = new string[] { "t login" };

        //    Command2 cmd = new Command2(testingString);

        //    Assert.AreEqual(ename, cmd.CommandName);
        //    CollectionAssert.AreEqual(args, cmd.Arguments);
        //    CollectionAssert.AreEqual(param, cmd.Parameters);
        //}

        //[TestMethod]

        //public void Command2Test2()
        //{
        //    string testingString = "execute \"login demderk.com AS demderk\" -tr 100 -wp 100";

        //    string ename = "execute";
        //    string[] args = new string[] { "login demderk.com AS demderk" };
        //    string[] param = new string[] { "tr 100", "wp 100" };

        //    Command2 cmd = new Command2(testingString);

        //    Assert.AreEqual(ename, cmd.CommandName);
        //    CollectionAssert.AreEqual(args, cmd.Arguments);
        //    CollectionAssert.AreEqual(param, cmd.Parameters);
        //}
        //[TestMethod]

        //public void Command2Test22()
        //{
        //    string testingString = "execute \"login demderk.com AS demderk\" -tr 100 -wp 100                                           ";

        //    string ename = "execute";
        //    string[] args = new string[] { "login demderk.com AS demderk" };
        //    string[] param = new string[] { "tr 100", "wp 100                                           " };

        //    Command2 cmd = new Command2(testingString);

        //    Assert.AreEqual(ename, cmd.CommandName);
        //    CollectionAssert.AreEqual(args, cmd.Arguments);
        //    CollectionAssert.AreEqual(param, cmd.Parameters);
        //}
        //[TestMethod]

        //public void Command2Test3()
        //{
        //    string testingString = "executeremote \"execute \\\"login demderk.com AS demderk\\\" -tr 100 -wp 100\" -ip 200.0.0.1";

        //    string ename = "executeremote";
        //    string[] args = new string[] { "execute \"login demderk.com AS demderk\" -tr 100 -wp 100" };
        //    string[] param = new string[] { "ip 200.0.0.1" };

        //    Command2 cmd = new Command2(testingString);

        //    Assert.AreEqual(ename, cmd.CommandName);
        //    CollectionAssert.AreEqual(args, cmd.Arguments);
        //    CollectionAssert.AreEqual(param, cmd.Parameters);
        //}
        //[TestMethod]

        //public void Command2Test4()
        //{
        //    string testingString = "open chrome \"google.com\"";

        //    string ename = "open";
        //    string[] args = new string[] { "chrome", "google.com" };
        //    string[] param = new string[] { "t login" };

        //    Command2 cmd = new Command2(testingString);

        //    Assert.AreEqual(ename, cmd.CommandName);
        //    CollectionAssert.AreEqual(args, cmd.Arguments);
        //    //CollectionAssert.AreEqual(param, cmd.Parameters);
        //}

        //[TestMethod]

        //public void Command2Test5()
        //{
        //    string testingString = "open chrome \'google   .com\'";

        //    string ename = "open";
        //    string[] args = new string[] { "chrome", "google   .com" };
        //    string[] param = new string[] { "t login" };

        //    Command2 cmd = new Command2(testingString);

        //    Assert.AreEqual(ename, cmd.CommandName);
        //    CollectionAssert.AreEqual(args, cmd.Arguments);
        //    //CollectionAssert.AreEqual(param, cmd.Parameters);
        //}

        //[TestMethod]

        //public void Command2Test6()
        //{
        //    string testingString = @"start explorer c:\\";

        //    string ename = "start";
        //    string[] args = new string[] { "explorer", @"c:\\" };
        //    string[] param = new string[] { "-1" };

        //    Command2 cmd = new Command2(testingString);

        //    Assert.AreEqual(ename, cmd.CommandName);
        //    CollectionAssert.AreEqual(args, cmd.Arguments);
        //    //CollectionAssert.AreEqual(param, cmd.Parameters);
        //}
    }



}
