using System.Web.Mvc;
using inventoryAppDomain.Entities.Enums;
using inventoryAppDomain.Services;
using inventoryAppWebUi.Controllers;
using Moq;
using NUnit.Framework;

namespace InventoryAppWebUi.Test.Tests
{
    /// <summary>
    /// Summary description for ReportControllerTest
    /// </summary>
    //[TestClass]
    public class ReportServiceTest
    {
        public ReportServiceTest()
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

        [Test]
        public void IndexTest()
        {
            //Arrange
            var newTimeFrame = new TimeFrame();

            Mock<IReportService> _mockReport = new Mock<IReportService>();

            var controller = new ReportController(_mockReport.Object);
            //Act
            var result = controller.Index(newTimeFrame) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
