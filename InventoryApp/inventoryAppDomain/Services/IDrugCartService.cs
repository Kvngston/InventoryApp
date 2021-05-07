using inventoryAppDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using inventoryAppDomain.Entities.Enums;

namespace inventoryAppDomain.Services
{
    public interface IDrugCartService
    {
        DrugCart CreateCart(string userId);
        DrugCart GetCart(string userId, CartStatus cartStatus);
        void AddToCart(Drug drug, string userId, int totalAmountOfPrescribedDrugs = 0, int amount = 1);
        void ClearCart(string cartId);
        List<DrugCartItem> GetDrugCartItems(string userId, CartStatus cartStatus);
        int RemoveFromCart(Drug drug, string userId);
   
        decimal GetDrugCartSumTotal(string userId);
        int GetDrugCartTotalCount(string userId);
        Drug GetDrugById(int id);
        Drug GetDrugByName(string Name);
        DrugCartItem GetDrugCartItemById(int id);
        DrugCart RefreshCart(string userId);
    }
}
