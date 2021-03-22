using System;
using System.Text;
using System.Collections.Generic;

using inventoryAppDomain.Services;
using Moq;
using inventoryAppWebUi.Controllers;
using System.Web.Mvc;
using inventoryAppDomain.Entities;
using inventoryAppWebUi.Models;
using NUnit.Framework;

namespace InventoryAppWebUi.Test
{
    /// <summary>
    /// Summary description for UnitTest3
    /// </summary>
    //[TestClass]
    public class DrugControllerTest
    {
        public DrugControllerTest()
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
        public void AvailableDrugsTest()
        {
          
            Mock<IDrugService> _mockDrug = new Mock<IDrugService>();
            Mock<ISupplierService> _mockSupp = new Mock<ISupplierService>();
            var _controller = new DrugController(_mockDrug.Object, _mockSupp.Object);

            var result = _controller.AvailableDrugs() as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void DrugCategoriesTest()
        {
           
            Mock<IDrugService> _mockDrug = new Mock<IDrugService>();
            Mock<ISupplierService> _mockSupp = new Mock<ISupplierService>();
            var _controller = new DrugController(_mockDrug.Object, _mockSupp.Object);

            var result = _controller.ListDrugCategories() as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void AllDrugsTest()
        {
           
            Mock<IDrugService> _mockDrug = new Mock<IDrugService>();
            Mock<ISupplierService> _mockSupp = new Mock<ISupplierService>();
            var _controller = new DrugController(_mockDrug.Object, _mockSupp.Object);

            var result = _controller.AllDrugs() as ViewResult;

            Assert.AreNotEqual("AllDrugs", result.ViewName);
        }

        [Test]
        public void EditDrugTest()
        {
            int DrugId = 98;
            var newDrug = new DrugViewModel
            {
                Id = DrugId,
                Quantity = 45,
                Price = 7000,
                SupplierTag = "abcs",
                DrugName = "abcvn"
                
            };


            Mock<IDrugService> _mockDrug = new Mock<IDrugService>();
            Mock<ISupplierService> _mockSupp = new Mock<ISupplierService>();
            var _controller = new DrugController(_mockDrug.Object, _mockSupp.Object);

            var result = _controller.UpdateDrug(newDrug.Id);

            Assert.AreEqual(newDrug, result);
        }

    }
}
