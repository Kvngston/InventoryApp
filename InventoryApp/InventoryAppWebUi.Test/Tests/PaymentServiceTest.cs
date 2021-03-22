using System.Web.Mvc;
using inventoryAppDomain.Services;
using inventoryAppWebUi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InventoryAppWebUi.Test.Tests
{
    /// <summary>
    /// Summary description for PaymentControllerTest
    /// </summary>
    [TestClass]
    public class PaymentServiceTest
    {
        public PaymentServiceTest()
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
        public void TestMethod1()
        {
            var OrderId = 12;
            Mock<IPaymentService> _mockPayment = new Mock<IPaymentService>();
         

            var controller = new PaymentController(_mockPayment.Object);

            var result = controller.Index(OrderId) as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
