using AutoMapper;
using inventoryAppDomain.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using inventoryAppDomain.Entities;
using inventoryAppDomain.IdentityEntities;
using inventoryAppWebUi.Models;
using inventoryAppDomain.Entities.Enums;

namespace inventoryAppWebUi.Controllers
{
    [Authorize]
    public class DrugController : Controller
    {
        private readonly IDrugService _drugService;
        private readonly ISupplierService _supplierService;
        
        public DrugController(IDrugService drugService, ISupplierService supplierService)
        {
            _drugService = drugService;
            _supplierService = supplierService;

        }
        // GET: Drug
        public ActionResult AllDrugs()
        {
            return View(_drugService.GetAllDrugs());
        }
        public ActionResult AvailableDrugs()
        {
            var drugs = _drugService.GetAvailableDrugs();
          
            return View(drugs);
        }
        public ActionResult FilteredDrugsList(string searchString)
        {
            var drugs = _drugService.GetAvailableDrugs();
            var drugFilter = _drugService.GetAvailableDrugFilter(searchString);
            if (string.IsNullOrWhiteSpace(searchString) || string.IsNullOrEmpty(searchString))
            {
                var drugsVM = new DrugSearchViewModel
                {
                    Drugs = drugs,
                    SearchString = searchString
                };
                return View(drugsVM);
            }
            var drugsearchVM = new DrugSearchViewModel
            {
                Drugs = drugFilter,
                SearchString = searchString
            };
            return View(drugsearchVM);
        }




        public ActionResult AddDrugForm()
        {
            var drugCategory = new DrugViewModel()
            {
                DrugCategory = _drugService.AllCategories()
            };
            
            return PartialView("_DrugPartial", drugCategory);
        }

        public ActionResult UpdateDrug(int id)
        {

            var drugInDb = Mapper.Map<DrugViewModel>(_drugService.EditDrug(id));

            if (drugInDb == null) return HttpNotFound("No drug found");

            drugInDb.DrugCategory = _drugService.AllCategories();

            return PartialView("_DrugPartial", drugInDb);
        }

        public ActionResult SaveDrug(DrugViewModel drug)
        {
            if (!ModelState.IsValid)
            {
                drug.DrugCategory = _drugService.AllCategories();
                TempData["failed"] = "failed";
                return View("AddDrugForm", drug);
            }


            try
            {
                var supplierInDb = _supplierService.GetSupplierWithTag(drug.SupplierTag);

                if (supplierInDb == null)
                {
                    //If the supplier tag is not in the Db
                    ModelState.AddModelError("SupplierTag", "Supplier Tag isn't registered yet");
                    drug.DrugCategory = _drugService.AllCategories();
                    TempData["failed"] = "failed";
                    return PartialView("_DrugPartial", drug);
                }
                else
                {
                    //Add a new drug
                    if (drug.Id == 0)
                    {
                        var expiryDate = _drugService.DateComparison(DateTime.Today, drug.ExpiryDate);

                        //DRUG HAS EXPIRED 
                        if (expiryDate >= 0)
                        {
                            ModelState.AddModelError("ExpiryDate", "Must be later than today");
                            drug.DrugCategory = _drugService.AllCategories();
                            TempData["failed"] = "failed";
                            return PartialView("_DrugPartial", drug);
                        }

                        //SUPPLIER IS INACTIVE
                        if (supplierInDb.Status == SupplierStatus.InActive)
                        {
                            ModelState.AddModelError("SupplierTag", "Supplier has been deactivated");
                            drug.DrugCategory = _drugService.AllCategories();
                            TempData["failed"] = "failed";
                            return PartialView("_DrugPartial", drug);
                        }

                        // DRUG IS NOT GREATER THAN 0
                        if (drug.Quantity <= 0)
                        {
                            ModelState.AddModelError("Quantity", "Quantity should be greater than zero");
                            drug.DrugCategory = _drugService.AllCategories();
                            TempData["failed"] = "failed";
                            return PartialView("_DrugPartial", drug);
                        }

                        // DRUG PRICE IS LESS THAN 0
                        if (drug.Price <= 0)
                        {
                            ModelState.AddModelError("Price", "Price should be greater than zero");
                            drug.DrugCategory = _drugService.AllCategories();
                            TempData["failed"] = "failed";
                            return PartialView("_DrugPartial", drug);
                        }



                        _drugService.AddDrug(Mapper.Map<DrugViewModel, Drug>(drug));
                    }
                    else
                    {
                        // update existing drug
                        //NOTE
                        // check expiry date for drugs
                        var getDrugInDb = _drugService.EditDrug(drug.Id);
                        _drugService.UpdateDrug(Mapper.Map(drug, getDrugInDb));
                    }
                    TempData["added"] = "added";

                }
            }
            catch (Exception)
            {

                throw new HttpException("Something went wrong");
            }
            //return RedirectToAction("AddDrugForm");
            return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);

        }

        //Get
        [HttpGet]
        public ActionResult AddDrugCategory()
        {
            return View();
        }

        //Post
        [HttpPost]
        public ActionResult SaveDrugCategory(DrugCategory category)
        {
            if (ModelState.IsValid)
            {
                _drugService.AddDrugCategory(category);
                TempData["categoryAdded"] = "added";
                return View("AddDrugCategory");
            }
            TempData["failedToAddCategory"] = "failed";
            return View("AddDrugCategory", category);
        }

        public ActionResult RemoveDrug(int id)
        {
            _drugService.RemoveDrug(id);

            return RedirectToAction("AllDrugs");
        }

        public ActionResult ListDrugCategories()
        {
            return View(_drugService.AllCategories());
        }

        public ActionResult RemoveDrugCategory(int id)
        {
            _drugService.RemoveDrugCategory(id);
            return RedirectToAction("ListDrugCategories");
        }
    }
}