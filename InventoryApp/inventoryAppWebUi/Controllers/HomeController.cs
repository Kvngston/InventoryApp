using inventoryAppDomain.Services;
using inventoryAppWebUi.Models;
using System.Web.Mvc;
using inventoryAppDomain.Entities.Enums;
using Microsoft.AspNet.Identity;
using System;

namespace inventoryAppWebUi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ISupplierService _supplierService;
        private readonly IDrugService _drugService;
        private readonly IDrugCartService _drugCartService;
        private readonly IOrderService _orderService;

        public HomeController( ISupplierService supplierService, IDrugService drugService, IDrugCartService drugCartService, IOrderService orderService)
        {
            _supplierService = supplierService;
            _drugService = drugService;
            _drugCartService = drugCartService;
            _orderService = orderService;
        }

        public ActionResult Index(string paymentCompleted)
        {
            //check if user already has as cart
            if (Request.IsAuthenticated && User.Identity.IsAuthenticated)
            {
                var cart = _drugCartService.GetCart(User.Identity.GetUserId(),CartStatus.ACTIVE);
                if (cart == null)
                {
                    cart = _drugCartService.CreateCart(User.Identity.GetUserId());   
                }
            }
            var totalNumberOfSupplier = new IndexPageViewModel
            {
                TotalNumberOfSupplier = _supplierService.TotalNumberOfSupplier(),
                TotalNumberOfDrugs = _drugService.GetAllDrugs().Count,
                TotalRevenue = _orderService.GetTotalRevenue(),
                TotalSales = _orderService.GetTotalSales()

            };
            if (paymentCompleted != null)
            {
                Console.WriteLine(paymentCompleted);
                if (paymentCompleted.Equals("True"))
                {
                    TempData["dispensed"] = "dispensed";
                    ViewBag.Dispensed = "dispensed";
                }
                else
                {
                    TempData["failure"] = "failure";
                    ViewBag.Failure = "failure";
                }
            }
            return View(totalNumberOfSupplier);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}