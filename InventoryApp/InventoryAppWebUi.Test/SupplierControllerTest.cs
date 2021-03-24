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
    [TestFixture]
    public class SupplierControllerTest
    {
        private readonly Mock<ISupplierService> _mockSupplier;
        private readonly SupplierController _controller;

        public SupplierControllerTest()
        {
            _mockSupplier = new Mock<ISupplierService>();
            _controller = new SupplierController(_mockSupplier.Object);
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
            _mockSupplier.Setup(v => v.AddSupplier(newSupp));
       
            var target = _controller.SupplierAndDrugDetails(suppId);

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
                GrossAmountOfDrugsSupplied = 213,
                TagNumber = "abcdef",
                Website = "Https://www.abc.com",
                Status = SupplierStatus.Active,
            };

            var newSuppVM = new SupplierViewModel
            {
                Id = suppId,
                Email = "Abc@abc.com",
                SupplierName = "Obi",
                TagNumber = "abcdef",
                Website = "Https://www.abc.com",
                Status = SupplierStatus.Active

            };

            _mockSupplier.Setup(v => v.AddSupplier(newSupp));
            _mockSupplier.Setup(x => x.GetAllSuppliers()).Returns(newSuppList);

           var result = _controller.Save(newSuppVM);

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

            _mockSupplier.Setup(v => v.ProcessSupplier(suppId, SupplierStatus.Active));

            var target = _controller.ProcessSupplier(suppId);

            Assert.That(target, Is.Not.EqualTo(null));
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

            _mockSupplier.Setup(v => v.UpdateSupplier(newSupp));
     
            var target = _controller.EditSupplier(suppId);

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

            _mockSupplier.Setup(v => v.GetAllDrugsBySupplier(newSupp.TagNumber));
            var target = _controller.SupplierAndDrugDetails(suppId);

            Assert.AreNotEqual(newSupp, target);
        }

    }
}
