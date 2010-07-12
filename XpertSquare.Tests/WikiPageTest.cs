using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using XpertSquare.Core.Model;

namespace XpertSquare.Tests
{
    /// <summary>
    /// Summary description for WikiPageTest
    /// </summary>
    [TestClass]
    public class WikiPageTest
    {
        public WikiPageTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void WikiPage_Fields()
        {
            WikiArticle page = new WikiArticle();

            page.ID = 10;
            page.Title = "Abc";
            page.Content = "Blah blah";

            Assert.AreEqual(page.ID, 10);
            Assert.AreEqual(page.Title, "Abc");
            Assert.AreEqual(page.Content, "Blah blah");
        }       

        [TestMethod]
        public void WikiPage_TwoIdenticalTagsAreAddedOnce()
        {
            WikiArticle page = new WikiArticle();

            XsTag tagOne = new XsTag();
            tagOne.Name = "TagOne";

            XsTag tagTwo = new XsTag();
            tagTwo.Name = "TagOne";

            page.AddTag(tagOne);
            page.AddTag(tagTwo);
            
            Assert.AreEqual(page.Tags.Count,1);

        }
    }
}
