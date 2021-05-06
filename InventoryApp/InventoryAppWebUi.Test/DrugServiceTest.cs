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
using AutoMapper;
using static Newtonsoft.Json.JsonConvert;

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
            _cartController = new DrugCartController(_mockDrugCart.Object, _mockDrug.Object);
        }
        [SetUp]
        public void Setup()
        {
            Mapper.Initialize(configuration => configuration.CreateMap<Drug, DrugViewModel>());
            Mapper.Initialize(configuration => configuration.CreateMap<DrugCategoryViewModel, DrugCategory>());
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
            var newDrugCategoryVm = new DrugCategoryViewModel
            {
                CategoryName = "Pills"
                
            };
            _mockDrug.Setup(v => v.AddDrugCategory(newDrugCategory));

            if (_dcontroller.SaveDrugCategory(newDrugCategoryVm) is JsonResult result)
            {
                var response = DeserializeObject<JsonResponse>(SerializeObject(result.Data))
                    ?.response;
                Assert.True(response != null && response.Equals("success"));
            }

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

     

        private class JsonResponse
        {
            public string status { get; set; }
            public string response { get; set; }
        }
    }
}
