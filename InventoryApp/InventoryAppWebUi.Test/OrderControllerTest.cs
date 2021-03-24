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
using inventoryAppWebUi.Models;
using inventoryAppDomain.Entities;
using inventoryAppDomain.IdentityEntities;
using inventoryAppDomain.Entities.Enums;

namespace InventoryAppWebUi.Test
{
    /// <summary>
    /// Summary description for UnitTest2
    /// </summary>
    //[TestClass]
    public class OrderControllerTest
    {
        public OrderControllerTest()
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
        public void CheckoutCompleteTest()
        {
            Mock<IDrugCartService> _mockDrugCart = new Mock<IDrugCartService>();
            Mock<IOrderService> _mockOrder = new Mock<IOrderService>();

            var controller = new OrderController(_mockOrder.Object, _mockDrugCart.Object);

            var result = controller.CheckoutComplete() as ViewResult;

            Assert.IsNotNull(result);
        }
        [Test]
        public void CheckoutTest()
        {
            //var orderVM = new OrderViewModel
            //{
            //    Email = "abc@abc.com",
            //    FirstName = "dab",
            //    LastName = "bad",
            //    PhoneNumber = "0908123"
            //};
            var singleDrug = new Drug
            {

                Id = 88,
                DrugName = "antraxe",
                Price = 4000,
                Quantity = 35,
                CreatedAt = DateTime.Today,
                ExpiryDate = DateTime.Today.AddDays(9),
                CurrentDrugStatus = DrugStatus.NOT_EXPIRED
            };
            var userId = Guid.NewGuid().ToString();
            var newUser = new ApplicationUser
            {
                Id = userId, Email = "bac@bac.com", UserName = "bac@bac.com", PhoneNumber = "0908123"
            };
            var newdrugCartItems = new List<DrugCartItem>
            {
                new DrugCartItem
                {
                    Id = 80, Amount = 4000, DrugId = 45, Drug = singleDrug, DrugCartId = 191
                }


            };
            var newOrder = new Order
            {
                OrderId = 123, Email = "abc@abc.com", FirstName = "abc", LastName = "bbc", Price = 4000, PhoneNumber = "09034", CreatedAt = DateTime.Now, OrderItems = newdrugCartItems
            };

            Mock<IDrugCartService> _mockDrugCart = new Mock<IDrugCartService>();
            Mock<IOrderService> _mockOrder = new Mock<IOrderService>();

            _mockOrder.Setup(z => z.CreateOrder(newOrder, newUser.Id));

            Assert.AreEqual(newOrder.OrderItems, newdrugCartItems);
        }
    }
}
