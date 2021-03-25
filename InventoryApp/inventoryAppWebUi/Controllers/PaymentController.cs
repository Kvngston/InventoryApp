using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using inventoryAppDomain.Entities.Dtos;
using inventoryAppDomain.Services;
using inventoryAppWebUi.Models;

namespace inventoryAppWebUi.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        public async Task<ActionResult> ProcessPayment(int orderId)
        {
            try
            {
                var result = await _paymentService.InitiatePayment(orderId);
                return Redirect(result.checkoutUrl);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<ActionResult> VerifyPayment(string paymentReference)
        {
            try
            {
                var response = await _paymentService.VerifyPayment(paymentReference);
                TempData["dispensed"] = "dispensed";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}