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
using inventoryAppDomain.Entities.Enums;

namespace InventoryAppWebUi.Test
{
   [TestFixture]
    public class DrugControllerTest
    {
        private readonly Mock<IDrugService> _mockDrug;
        private readonly Mock<ISupplierService> _mockSupp;
        private readonly Mock<IDrugCartService> _mockDrugCart;
        private readonly DrugController _dcontroller;
        private readonly DrugCartController _cartController;

        public DrugControllerTest()
        {
            _mockDrug = new Mock<IDrugService>();
            _mockSupp = new Mock<ISupplierService>();
            _mockDrugCart = new Mock<IDrugCartService>();
            _dcontroller = new DrugController(_mockDrug.Object, _mockSupp.Object);
            _cartController = new DrugCartController(_mockDrugCart.Object);
        }

        [Test]
        public void FilteredDrugListTest()
        {
            var searchString = "";
          
            _mockDrug.Setup(q => q.GetAvailableDrugFilter(searchString));

            var result = _dcontroller.FilteredDrugsList(searchString) as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void SaveDrugTest()
        {
            var newDrugCategory = new DrugCategory
            {
                CategoryName = "Pills",
                Id = 99
            };
            var drugId = 222;
            var newDrug = new Drug
            {
                Id = drugId,
                Quantity = 45,
                Price = 55,
                SupplierTag = "afghi",
                ExpiryDate = DateTime.Today.AddDays(25),
                DrugName = "purft",
                CreatedAt = DateTime.Today,
                CurrentDrugStatus = DrugStatus.NOT_EXPIRED,
                DrugCategoryId = 99
            };
            var newDrugVM = new DrugViewModel
            {
                Id = drugId,
                Quantity = 45,
                Price = 55,
                SupplierTag = "afghi",
                ExpiryDate = DateTime.Today.AddDays(25),
                DrugName = "purft"
            };

            _mockDrug.Setup(q => q.AddDrug(newDrug));

            var result = _dcontroller.SaveDrug(newDrugVM);

            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void SaveDrugCategoryTest()
        {
            var newDrugCategory = new DrugCategory
            {
                CategoryName = "Pills",
                Id = 99
            };
            _mockDrug.Setup(v => v.AddDrugCategory(newDrugCategory));

            var result = _dcontroller.AddDrugCategory() as ViewResult;
    
            Assert.AreNotEqual(newDrugCategory, result.Model);
        }

        [Test]
        public void RemoveDrugCategoryTest()
        {
            var newDrugCategory = new DrugCategory
            {
                CategoryName = "Pills",
                Id = 99
            };

            _mockDrug.Setup(z => z.RemoveDrugCategory(newDrugCategory.Id));

            var result = _dcontroller.RemoveDrugCategory(newDrugCategory.Id) as ViewResult;

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void RemoveDrugTest()
        {
            var drugId = 222;
            var newDrug = new Drug
            {
                Id = drugId,
                Quantity = 45,
                Price = 55,
                SupplierTag = "afghi",
                ExpiryDate = DateTime.Today.AddDays(25),
                DrugName = "purft",
                CreatedAt = DateTime.Today,
                CurrentDrugStatus = DrugStatus.NOT_EXPIRED,
                DrugCategoryId = 99
            };

            _mockDrug.Setup(z => z.RemoveDrug(newDrug.Id));

            var result = _cartController.GetDrug(drugId) as ViewResult;

            Assert.That(result != null);
        }

        [Test]
        public void AddDrugTest()
        {
            var result = _dcontroller.AllDrugs() as ViewResult;

            Assert.AreNotEqual("AllDrugs", result.Model);
        }

        [Test]
        public void EditDrugTest()
        {
            var drugId = 222;
            var newDrugVm = new DrugViewModel
            {
                Id = drugId,
                Quantity = 45,
                Price = 7000,
                SupplierTag = "abcs",
                DrugName = "abcvn"     
            };

            var newDrug = new Drug
            {
                Id = drugId,
                Quantity = 45,
                Price = 55,
                SupplierTag = "afghi",
                ExpiryDate = DateTime.Today.AddDays(25),
                DrugName = "purft",
                CreatedAt = DateTime.Today,
                CurrentDrugStatus = DrugStatus.NOT_EXPIRED,
                DrugCategoryId = 99
            };

            var _acontroller = new DrugCartController(_mockDrugCart.Object);

            _mockDrug.Setup(n => n.EditDrug(newDrugVm.Id)).Returns(newDrug);

            var result = _dcontroller.UpdateDrug(newDrug.Id);
            var target = _acontroller.GetDrug(newDrug.Id) as ViewResult;
          
            Assert.That(newDrug, Is.EqualTo(target.Model));
        }

    }
}
