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
using inventoryAppDomain.Entities.Enums;

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


        [Test]
        public void AllSuppliersTest()
        {
            Mock<ISupplierService> _mockSupplier = new Mock<ISupplierService>();

            var controller = new SupplierController(_mockSupplier.Object);

            var result = controller.AllSuppliers() as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void SupplierDetailsTest()
        {
            var suppId = 998;
            var newSupp = new Supplier
            {
                Id = suppId,
                Email = "Abc@abc.com",
                SupplierName = "Obi",
                GrossAmountOfDrugsSupplied = 213,
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

        [Test]
        public void AddNewSupplierTest()
        {
            var suppId = 998;
            var newSuppList = new List<Supplier>
            {
                new Supplier
            {
                Id = 45,
                Email = "bac@bnc.com",
                SupplierName = "sla",
                GrossAmountOfDrugsSupplied = 213,
                TagNumber = "ghdijklmen",
                Website = "Https://www.abc.com"

            }
        }; 
                
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
           
            var controller = new SupplierController(_mockSupplier.Object);

            _mockSupplier.Setup(v => v.AddSupplier(newSupp));
            var target = _mockSupplier.Setup(x => x.GetAllSuppliers()).Returns(newSuppList);

           var result = controller.AddSupplier();

            Assert.That(newSuppList.Contains(newSupp));
        }
        [Test]
        public void ProcessSupplierTest()
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
            _mockSupplier.Setup(v => v.ProcessSupplier(suppId, SupplierStatus.Active));
            var controller = new SupplierController(_mockSupplier.Object);

            //controller.Save(newSupp);
            var target = controller.ProcessSupplier(suppId);

            Assert.AreNotEqual(newSupp, target);
        }
        [Test]
        public void UpdateSupplierTest()
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
            _mockSupplier.Setup(v => v.UpdateSupplier(newSupp));
            var controller = new SupplierController(_mockSupplier.Object);

            //controller.Save(newSupp);
            var target = controller.EditSupplier(suppId);

            Assert.AreNotEqual(newSupp, target);
        }
 
        [Test]
        public void DrugsBySupplierTest()
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
            _mockSupplier.Setup(v => v.GetAllDrugsBySupplier(newSupp.TagNumber));
            var controller = new SupplierController(_mockSupplier.Object);

            //controller.Save(newSupp);
            var target = controller.SupplierAndDrugDetails(suppId);

            Assert.AreNotEqual(newSupp, target);
        }

    }
}
