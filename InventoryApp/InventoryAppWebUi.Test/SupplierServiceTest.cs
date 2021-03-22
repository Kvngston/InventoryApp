using System;
using System.Text;
using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using inventoryAppDomain.Services;
using Moq;
using inventoryAppWebUi.Controllers;
using System.Web.Mvc;
using NUnit.Framework;
using NUnit.Compatibility;
using inventoryAppDomain.Entities;
using inventoryAppWebUi.Models;

namespace InventoryAppWebUi.Test
{
    /// <summary>
    /// Summary description for SupplierControllerTest
    /// </summary>
    //[TestClass]
    public class SupplierControllerTest
    {
        public SupplierControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

      
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
        public void AllSuppliersTest()
        {
            Mock<ISupplierService> _mockSupplier = new Mock<ISupplierService>();


            var controller = new SupplierController(_mockSupplier.Object);

            var result = controller.AllSuppliers() as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void SaveTest()
        {
            var suppId = 998;
            var newSupp = new Supplier
            {
                Id = suppId,
                Email = "Abc@abc.com",
                SupplierName = "Obi",
                //GrossAmountOfDrugsSupplied = 213,
                TagNumber = "abcdef",
                Website = "Https://www.abc.com"
                
            };

            Mock<ISupplierService> _mockSupplier = new Mock<ISupplierService>();
            _mockSupplier.Setup(v => v.AddSupplier(newSupp));
            var controller = new SupplierController(_mockSupplier.Object);

            //controller.Save(newSupp);
            var target = controller.SupplierAndDrugDetails(suppId);

            Assert.AreNotEqual(newSupp, target);
        }

    }
}
