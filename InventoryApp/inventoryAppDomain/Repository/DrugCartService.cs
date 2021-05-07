using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using inventoryAppDomain.Entities;
using inventoryAppDomain.Entities.Enums;
using inventoryAppDomain.IdentityEntities;
using inventoryAppDomain.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace inventoryAppDomain.Repository
{
    public class DrugCartService : IDrugCartService
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly ApplicationUserManager userManager;

        public DrugCartService()
        {
            _dbContext = HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
            userManager = HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>();
        }

        public DrugCart CreateCart(string userId)
        {
            var user = userManager.Users.FirstOrDefault(applicationUser => applicationUser.Id == userId);
            var cart = new DrugCart()
            {
                ApplicationUser = user,
                ApplicationUserId = userId,
                DrugCartItems = new List<DrugCartItem>(),
                CartStatus = CartStatus.ACTIVE
            };

            _dbContext.DrugCarts.Add(cart);
            _dbContext.SaveChanges();

            return cart;
        }

        public DrugCart GetCart(string userId, CartStatus cartStatus)
        {
            var user = userManager.Users.FirstOrDefault(applicationUser => applicationUser.Id == userId);

            if (user != null)
                return _dbContext.DrugCarts.Include(cart => cart.DrugCartItems)
                    .FirstOrDefault(cart => cart.ApplicationUserId == userId && cart.CartStatus == cartStatus);
            
            return null;
        }


        public void AddToCart(Drug drug, string userId, int totalAmountOfPrescribedDrugs = 0, int amount = 1)
        {
            var drugCart = GetCart(userId, CartStatus.ACTIVE);
            var cartItem = _dbContext.DrugCartItems.FirstOrDefault(item => item.DrugId == drug.Id && item.DrugCartId == drugCart.Id);
            if (cartItem == null)
            {
                cartItem = new DrugCartItem
                {
                    DrugCartId = drugCart.Id,
                    DrugCart = drugCart,
                    Drug = drug,
                    PrescribedAmount = totalAmountOfPrescribedDrugs * drug.PricePerUnit,
                    Amount = amount
                };
                _dbContext.DrugCartItems.Add(cartItem);
            }
            else
            {
                if (cartItem.Amount < cartItem.Drug.Quantity)
                    cartItem.Amount++;
            }
            _dbContext.SaveChanges();
        }

        public int RemoveFromCart(Drug drug, string userId)
        {
            var drugCart = GetCart(userId, CartStatus.ACTIVE);
            var cartItem = _dbContext.DrugCartItems.FirstOrDefault(item => item.DrugId == drug.Id);
            if (cartItem != null)
            {
                if (cartItem.Amount > 1)
                {
                    cartItem.Amount--;
                }
                else
                {
                    drugCart.DrugCartItems.Remove(cartItem);
                    _dbContext.Entry(drugCart).State = EntityState.Modified;
                    _dbContext.DrugCartItems.Remove(cartItem);
                }
            }
            _dbContext.SaveChanges();
            if (cartItem != null) return cartItem.Amount;
            return 0;
        }

        public Drug GetDrugById(int Id)
        {
            var drug = _dbContext.Drugs.FirstOrDefault(x => x.Id == Id);
            return drug;
        }

        public Drug GetDrugByName(string Name)
        {
            var drug = _dbContext.Drugs.FirstOrDefault(x => x.DrugName == Name);
            return drug;
        }

        public DrugCartItem GetDrugCartItemById(int id)
        {
            var drug = _dbContext.DrugCartItems.Include(item => item.Drug).FirstOrDefault(x => x.Id == id);
            return drug;
        }

        public DrugCart RefreshCart(string userId)
        {
            var drugCart = GetCart(userId, CartStatus.MOST_RECENT);
            var newCart = CreateCart(userId);
            drugCart.CartStatus = CartStatus.USED;
            _dbContext.Entry(drugCart).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return newCart;
        }

        public List<DrugCartItem> GetDrugCartItems(string userId, CartStatus cartStatus)
        {
            var cart = GetCart(userId,cartStatus);
            return _dbContext.DrugCartItems.Include(item => item.Drug).Where(item => item.DrugCartId == cart.Id).ToList();
        }
        public void ClearCart(string userId)
        {
            var cart = GetCart(userId,CartStatus.ACTIVE);
            _dbContext.DrugCartItems.RemoveRange(cart.DrugCartItems);
            cart.DrugCartItems = new List<DrugCartItem>();
            _dbContext.Entry(cart).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public decimal GetDrugCartSumTotal(string userId)
        {
            var cart = GetCart(userId,CartStatus.ACTIVE);
            if (cart.DrugCartItems.Count == 0)
            {
                return 0;
            }
            var sum = _dbContext.DrugCartItems.Where(c => c.DrugCartId == cart.Id)
                .Select(c => (c.Drug.Price * c.Amount) + c.PrescribedAmount).Sum();
            
            return sum;
        }
        public int GetDrugCartTotalCount(string userId)
        {
            var cart = GetCart(userId, CartStatus.ACTIVE);
            var total = _dbContext.DrugCartItems.Count(c => c.DrugCartId == cart.Id); ;

       
            return total;
        }
    }
}