using System;
using System.Web.Mvc;
using inventoryAppDomain.Entities;
using inventoryAppDomain.Services;
using inventoryAppWebUi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InventoryAppWebUi.Test.Tests
{
    /// <summary>
    /// Summary description for NotificationsControllerTest
    /// </summary>
    [TestClass]
    public class NotificationsServiceTest
    {
        public NotificationsServiceTest()
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
        public void IndexTest()
        {
            Mock<INotificationService> _mockNotifications = new Mock<INotificationService>();
         

            var controller = new NotificationsController(_mockNotifications.Object);

            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllNotificationsTest()
        {
            Mock<INotificationService> _mockNotifications = new Mock<INotificationService>();


            var controller = new NotificationsController(_mockNotifications.Object);

            var result = controller.GetAllNotifications() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            int NotId = 99;

            var Notif = new Notification {
            Id = NotId,
            Title = "FakeNot",
            NotificationDetails = "FakeNotDetails",
            CreatedAt = DateTime.Now
            };

            Mock<INotificationService> _mockNotifications = new Mock<INotificationService>();


            var controller = new NotificationsController(_mockNotifications.Object);

            var result = controller.GetNotificationById(NotId) as ViewResult;

            Assert.AreNotSame(result, Notif);
        }
    }
}
