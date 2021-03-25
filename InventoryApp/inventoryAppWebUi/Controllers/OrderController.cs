using inventoryAppDomain.Entities;
using inventoryAppDomain.IdentityEntities;
using inventoryAppDomain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using inventoryAppDomain.Entities.Enums;
using inventoryAppWebUi.Models;
using Microsoft.AspNet.Identity;

namespace inventoryAppWebUi.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IDrugCartService _drugCartService;
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService, IDrugCartService drugCartService)
        {
            _drugCartService = drugCartService;
            _orderService = orderService;
        }
        
        public ActionResult Invoice(string payWithCash = null)
        {
            if (payWithCash != null)
            {
                ViewBag.PayWithCash = true;
            }
            return View();
        }
        
        
        [HttpPost]
        public ActionResult Checkout(OrderViewModel viewModel)
        {
            var userId = User.Identity.GetUserId();
            var items = _drugCartService.GetDrugCartItems(userId,CartStatus.ACTIVE);

            if (!items.Any())
            {
                ModelState.AddModelError("", @"Your cart is empty");
            }

            if (ModelState.IsValid)
            {
                var order = _orderService.CreateOrder(Mapper.Map<OrderViewModel, Order>(viewModel), userId);
                _drugCartService.RefreshCart(userId);
                return RedirectToAction("ProcessPayment", "Payment", new {orderId = order.OrderId});
                // TempData["dispensed"] = "dispensed";
                // return RedirectToAction("AvailableDrugs", "Drug");
            }
            return View("Invoice", viewModel);

        }
        
        [HttpPost]
        public ActionResult CheckoutWithCash(OrderViewModel viewModel)
        {
            var userId = User.Identity.GetUserId();
            var items = _drugCartService.GetDrugCartItems(userId,CartStatus.ACTIVE);

            if (!items.Any())
            {
                ModelState.AddModelError("", @"Your cart is empty");
            }

            if (ModelState.IsValid)
            {
                var order = _orderService.CreateOrder(Mapper.Map<OrderViewModel, Order>(viewModel), userId);
                _drugCartService.RefreshCart(userId);
                TempData["dispensed"] = "dispensed";
                return RedirectToAction("FilteredDrugsList", "Drug");
            }
            return View("Invoice", viewModel);

        }

        public ActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Drug Dispensed";
            return View();
        }
    }
}