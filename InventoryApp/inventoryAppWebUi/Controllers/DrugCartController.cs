using inventoryAppDomain.Services;
using inventoryAppWebUi.Models;
using System.Web.Mvc;
using inventoryAppDomain.Entities.Enums;
using Microsoft.AspNet.Identity;
using System;

namespace inventoryAppWebUi.Controllers
{
    public class DrugCartController : Controller
    {
        private readonly IDrugCartService _drugCartService;

        
        public DrugCartController(IDrugCartService drugCartService)
        {
            _drugCartService = drugCartService;
           
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var drugCartCountTotal = _drugCartService.GetDrugCartTotalCount(userId);
            var drugCartViewModel = new DrugCartViewModel
            {
                CartItems = _drugCartService.GetDrugCartItems(userId, CartStatus.ACTIVE),
                DrugCartItemsTotal = drugCartCountTotal,
                DrugCartTotal = _drugCartService.GetDrugCartSumTotal(userId),
            };
            return View(drugCartViewModel);
        }
        public ActionResult GetDrug(int id)
        {
            var drug = _drugCartService.GetDrugById(id);
            return View(drug);
        }

        public ActionResult AddToShoppingCart(int id)
        {
            var userId = User.Identity.GetUserId();
            var selectedDrug = _drugCartService.GetDrugById(id);
            try
            {
                if (selectedDrug == null)
                {
                    return HttpNotFound();
                }

                _drugCartService.AddToCart(selectedDrug, userId);
                return RedirectToAction("FilteredDrugsList", "Drug");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("FilteredDrugsList", "Drug");
            }
         
        }


        public ActionResult RemoveFromShoppingCart(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var cartItem = _drugCartService.GetDrugCartItemById(id);
                var selectedItem = _drugCartService.GetDrugById(cartItem.Drug.Id);

                if (selectedItem != null)
                {
                    _drugCartService.RemoveFromCart(selectedItem, userId);
                }

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ViewBag.Error = e.Message;
                return RedirectToAction("Index", "DrugCart");
            }
        }

        public ActionResult RemoveAllCart()
        {
            var userId = User.Identity.GetUserId();
            _drugCartService.ClearCart(userId);
            return RedirectToAction("Index");
        }
    }
}
