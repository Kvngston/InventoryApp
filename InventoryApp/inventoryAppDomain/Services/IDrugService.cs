using System;
using System.Collections.Generic;
using inventoryAppDomain.Entities;
using inventoryAppDomain.Entities.Enums;

namespace inventoryAppDomain.Services
{
    public interface IDrugService
    {

        List<Drug> GetAllDrugs();
        List<Drug> GetAllExpiringDrugs(TimeFrame timeFrame);
        List<Drug> GetAllExpiredDrugs();
        List<Drug> GetDrugsOutOfStock();

        Drug GetDrugById(int id);


        List<DrugCategory> AllCategories();
        void AddDrug(Drug drug);
        bool RemoveDrug(int id);
        Drug EditDrug(int id);
        int DateComparison(DateTime FirstDate, DateTime SecondDate);

        void AddDrugCategory(DrugCategory category);
        bool RemoveDrugCategory(int id);

        List<Drug> GetAvailableDrugs();
        Drug GetAvailableDrugsById(int id);

        List<Drug> GetAvailableDrugFilter(string searchQuery);

        void UpdateDrug(Drug drug);

        DrugCategory EditDrugCategory(int id);
        void UpdateDrugCategory(DrugCategory category);

    }
}