using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using inventoryAppDomain.Services;
using Moq;
using inventoryAppWebUi.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using NUnit.Compatibility;
using inventoryAppWebUi.Models;
using inventoryAppDomain.Entities;
using inventoryAppDomain.IdentityEntities;
using inventoryAppDomain.Entities.Enums;
using AutoMapper;
using Microsoft.AspNet.Identity;

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

    
    }
}