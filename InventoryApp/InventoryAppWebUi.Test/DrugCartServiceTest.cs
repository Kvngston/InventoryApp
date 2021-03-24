using System;
using System.Text;
using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using inventoryAppDomain.Services;
using Moq;
using inventoryAppWebUi.Controllers;
using System.Web.Mvc;
using NUnit.Framework;
using inventoryAppDomain.Entities;

namespace InventoryAppWebUi.Test
{
    /// <summary>
    /// Summary description for DrugCartControllerTest
    /// </summary>
    //[TestClass]
    public class DrugCartControllerTest
    {
        public DrugCartControllerTest()
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
            var userId = Guid.NewGuid();

            Mock<IDrugCartService> _mockDrug = new Mock<IDrugCartService>();
            _mockDrug.Setup(z => z.ClearCart(userId.ToString()));
            var controller = new DrugCartController(_mockDrug.Object);

            var result = controller.Index() as ViewResult;

            Assert.AreNotEqual("Index", result.ViewName);
        }

        [Test]
        public void AddToShoppingCartTest()
        {
            var drugCartId = 909;
            //var newDrugCart = new DrugCart
            //{
            //    Id = drugCartId,
              
            //    ApplicationUserId = "4be7ca48-dc53-4d18-84fa-44c73507e4a5"

            //};
            Mock<IDrugCartService> _mockDrug = new Mock<IDrugCartService>();
            //_mockDrug.Setup(z => z.RefreshCart(userId.ToString()));
            var controller = new DrugCartController(_mockDrug.Object);
            var result = controller.AddToShoppingCart(drugCartId) as ViewResult;

            Assert.AreNotEqual("FilteredDrugsList", result.ViewName);
        }
    }
}
