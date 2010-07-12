using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using XpertSquare.Core.Model;
using XpertSquare.Data.Mocks;

namespace XpertSquare.Tests
{
    /// <summary>
    /// Summary description for XsUserTest
    /// </summary>
    [TestClass]
    public class XsUserTest
    {
        public XsUserTest()
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
        public void XsUser_Fields()
        {
            XsUser user = new XsUser();
            user.ID = 10;
            user.Username = "Marius";
            user.Password = "pwd@123";
            user.Email = "contact@mariusserban@.com";
            user.DisplayName = "Marius123";
            user.FirstName = "Marius";
            user.LastName = "Serban";
            user.Location = "Vancouver,BC";
            user.Description = "Blah blah";

            Assert.AreEqual(user.ID, 10);
            Assert.AreEqual(user.Username, "Marius");
            Assert.AreEqual(user.Password, "pwd@123");
            Assert.AreEqual(user.Email, "contact@mariusserban@.com");
            Assert.AreEqual(user.DisplayName, "Marius123");
            Assert.AreEqual(user.FirstName, "Marius");
            Assert.AreEqual(user.LastName, "Serban");
            Assert.AreEqual(user.Location, "Vancouver,BC");
            Assert.AreEqual(user.Description, "Blah blah");

        }

        [TestMethod]
        public void GetXsUserByID()
        {
            MockXsUserRepository userRepository = new MockXsUserRepository();

            XsUser user = userRepository.GetById(20);

            Assert.AreEqual(user.ID, 20);

        }
    }
}
