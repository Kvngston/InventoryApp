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
using AutoMapper;

namespace InventoryAppWebUi.Test
{
 [TestFixture]
    public class OrderControllerTest
    {
        private readonly Mock<IOrderService> _mockOrder;
        private readonly Mock<IDrugCartService> _mockdrugCart;
        private readonly OrderController _controller;

        public OrderControllerTest()
        {
            _mockOrder = new Mock<IOrderService>();
            _mockdrugCart = new Mock<IDrugCartService>();
            _controller = new OrderController(_mockOrder.Object, _mockdrugCart.Object);
        }
        [SetUp]
        public void Setup()
        {
            Mapper.Initialize(configuration => configuration.CreateMap<OrderViewModel, Order>());
         
        }

        [Test]
        public void Checkout_Is_Complete_Test()
        {
            var result = _controller.CheckoutComplete() as ViewResult;

            Assert.That(result.ViewBag.CheckoutCompleteMessage, Is.EqualTo("Drug Dispensed"));
        }
        [Test]
        public void Checkout_Success_Test()
        {
            var orderVM = new OrderViewModel
            {
                Email = "abc@abc.com",
                FirstName = "dab",
                LastName = "bad",
                PhoneNumber = "0908123"
            };
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

            _mockOrder.Setup(z => z.CreateOrder(newOrder, newUser.Id)).Returns(newOrder);
            _mockdrugCart.Setup(v => v.RefreshCart(userId));
            var result = _controller.Checkout(orderVM) as RedirectResult;
          
            Assert.That(result?.Url, Is.Not.Null);
        }

        [Test]
        public void Checkout_Failure_Test()
        {
            var orderVM = new OrderViewModel
            {
                Email = "abc@abc.com",
                FirstName = "dab",
                LastName = "bad",
                PhoneNumber = "0908123"
            };
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
       
            var newdrugCartItems = new List<DrugCartItem>
            {
                new DrugCartItem
                {
                    Id = 80, Amount = 4000, DrugId = 45, Drug = singleDrug, DrugCartId = 191
                }
            };
            var newOrder = new Order
            {
                OrderId = 123,
                Email = "abc@abc.com",
                FirstName = "abc",
                LastName = "bbc",
                Price = 4000,
                PhoneNumber = "09034",
                CreatedAt = DateTime.Now,
                OrderItems = newdrugCartItems
            };
            
            _mockdrugCart.Setup(a => a.GetDrugCartItems(userId, CartStatus.ACTIVE)).Returns(newdrugCartItems);
            _mockOrder.Setup(z => z.CreateOrder(Mapper.Map<OrderViewModel, Order>(orderVM), userId)).Returns(newOrder);
            _mockdrugCart.Setup(v => v.RefreshCart(userId));

            var result = _controller.Checkout(orderVM) as RedirectResult;

            Assert.That(result?.Url, Is.Null);
        }
    }
}
