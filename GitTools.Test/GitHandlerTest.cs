using GitTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GitTools.Test
{
    
    
    /// <summary>
    ///This is a test class for GitHandlerTest and is intended
    ///to contain all GitHandlerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GitHandlerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void IsReusableTest()
        {
            GitHandler handler = new GitHandler();
            Assert.IsFalse(handler.IsReusable);
        }

        [TestMethod()]
        public void GetGitDirTest()
        {
            GitHandler handler = new GitHandler();

            var dir = handler.GetGitDir("/test.com/12 34.git");
            Assert.AreEqual("test.com/12 34.git", dir);

            dir = handler.GetGitDir("/git-scc.git/info/refs?service=git-upload-pack");
            Assert.AreEqual("git-scc.git", dir);

            dir = handler.GetGitDir("/12 34/git-scc.git/info/refs?service=git-upload-pack");
            Assert.AreEqual("12 34/git-scc.git", dir);

            dir = handler.GetGitDir("/test/12/34/git-scc.git/info/refs?service=git-upload-pack");
            Assert.AreEqual("test/12/34/git-scc.git", dir);
        }

    }
}
