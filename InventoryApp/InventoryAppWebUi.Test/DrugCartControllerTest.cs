using System;
using System.Text;
using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using inventoryAppDomain.Services;
using Moq;
using inventoryAppWebUi.Controllers;
using System.Web.Mvc;
using NUnit.Framework;
using inventoryAppDomain.Entities;
using inventoryAppDomain.IdentityEntities;
using Microsoft.AspNet.Identity.EntityFramework;
using inventoryAppDomain.Entities.Enums;
using inventoryAppDomain.Repository;
using System.Web;
using System.Threading.Tasks;

namespace InventoryAppWebUi.Test
{
   [TestFixture]
    public class DrugCartControllerTest
    {

        private readonly Mock<IDrugCartService> _mockDrugCart;
        private readonly DrugCartController _cartController;

        public DrugCartControllerTest()
        {
            _mockDrugCart = new Mock<IDrugCartService>();
            _cartController = new DrugCartController(_mockDrugCart.Object);
        }

        [Test]
        public void CreateCartTest()
        {
            var userId = Guid.NewGuid().ToString();

            var user = new List<ApplicationUser>
            {
                    new ApplicationUser
                    {
                        Id = userId, Email = "abc@abc.com", UserName = "abc@abc.com", PhoneNumber = "0908777"
                    },
                    new ApplicationUser
                    {
                        Id = "utsr", Email = "efg@efg.com", UserName = "efg@efg.com", PhoneNumber = "0908777"
                    }
            };

            var roles = new List<IdentityRole>
                                {
                                            new IdentityRole
                                            {
                                                Id = "zxy", Name = "Audit"
                                            },
                                            new IdentityRole
                                            {
                                                Id = "wvu", Name = "Pharmacist"
                                           }
                                 };
            var newDrug = new List<Drug>
                            {
                                new Drug
                                {
                                    Id = 45, DrugName = "drax", Price = 4000, Quantity = 55, CreatedAt = DateTime.Today, ExpiryDate = DateTime.Today.AddDays(9),  CurrentDrugStatus = DrugStatus.NOT_EXPIRED
                                },
                                new Drug
                                {

                                    Id = 77, DrugName = "antrax", Price = 7000, Quantity = 35, CreatedAt = DateTime.Today, ExpiryDate = DateTime.Today.AddDays(9),  CurrentDrugStatus = DrugStatus.NOT_EXPIRED
                                }
                            };
            var singleDrug = new Drug
            {

                Id = 88,
                DrugName = "antraxe",
                Price = 8000,
                Quantity = 35,
                CreatedAt = DateTime.Today,
                ExpiryDate = DateTime.Today.AddDays(9),
                CurrentDrugStatus = DrugStatus.NOT_EXPIRED
            };
            var newdrugCartItems = new List<DrugCartItem>
            {
                new DrugCartItem
                {
                    Id = 80, Amount = 4000, DrugId = 45, Drug = newDrug.Find(v => v.Id == 45), DrugCartId = 191
                }
            };

            var newCart = new DrugCart
            {
                Id = 191,
                CartStatus = CartStatus.ACTIVE,
                ApplicationUser = user.Find(z => z.Id == userId),
                ApplicationUserId = userId,
                DrugCartItems = newdrugCartItems
            };
            _mockDrugCart.Setup(b => b.GetDrugById(88)).Returns(singleDrug);
               
            var result = _cartController.GetDrug(88) as ViewResult;

            Assert.AreEqual(result.Model, singleDrug);
        }
     

        [Test]
        public void AddToShoppingCartTest()
        {
            var newUser = new ApplicationUser
            {
                Id = "utsr",
                Email = "efg@efg.com",
                UserName = "efg@efg.com",
                PhoneNumber = "0908777"
            };
            var singleDrug = new Drug
            {
                Id = 88,
                DrugName = "antraxe",
                Price = 8000,
                Quantity = 35,
                CreatedAt = DateTime.Today,
                ExpiryDate = DateTime.Today.AddDays(9),
                CurrentDrugStatus = DrugStatus.NOT_EXPIRED
            };


            _mockDrugCart.Setup(z => z.AddToCart(singleDrug, newUser.Id));

            var result = _cartController.AddToShoppingCart(singleDrug.Id);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void ClearCartTest()
        {   
            var newUser = new ApplicationUser
            {
                Id = "utsr",
                Email = "efg@efg.com",
                UserName = "efg@efg.com",
                PhoneNumber = "0908777"
            };
            var singleDrug = new Drug
            {
                Id = 88,
                DrugName = "antraxe",
                Price = 8000,
                Quantity = 35,
                CreatedAt = DateTime.Today,
                ExpiryDate = DateTime.Today.AddDays(9),
                CurrentDrugStatus = DrugStatus.NOT_EXPIRED
            };
            var newdrugCartItems = new List<DrugCartItem>
            {
                new DrugCartItem
                {
                    Id = 80, Amount = 4000, DrugId = 45, Drug = singleDrug, DrugCartId = 191
                }
            };

            var newCart = new DrugCart
            {
                Id = 191,
                CartStatus = CartStatus.ACTIVE,
                ApplicationUser = newUser,
                ApplicationUserId = newUser.Id,
                DrugCartItems = newdrugCartItems
            };

            _mockDrugCart.Setup(z => z.ClearCart(newCart.Id.ToString()));

            var result = _cartController.RemoveAllCart();

            Assert.That(newdrugCartItems, Is.Null);
        }

        [Test]
        public void DrugCartTotalCountTest()
        {
            
            var newUser = new ApplicationUser
            {
                Id = "utsr",
                Email = "efg@efg.com",
                UserName = "efg@efg.com",
                PhoneNumber = "0908777"
            };
            var singleDrug = new Drug
            {
                Id = 88,
                DrugName = "antraxe",
                Price = 8000,
                Quantity = 35,
                CreatedAt = DateTime.Today,
                ExpiryDate = DateTime.Today.AddDays(9),
                CurrentDrugStatus = DrugStatus.NOT_EXPIRED
            };
            var newdrugCartItems = new List<DrugCartItem>
            {
                new DrugCartItem
                {
                    Id = 80, Amount = 4000, DrugId = 45, Drug = singleDrug, DrugCartId = 191
                }
            };

            var newCart = new DrugCart
            {
                Id = 191,
                CartStatus = CartStatus.ACTIVE,
                ApplicationUser = newUser,
                ApplicationUserId = newUser.Id,
                DrugCartItems = newdrugCartItems
            };

            _mockDrugCart.Setup(x => x.GetDrugCartTotalCount(newUser.Id));

        }

        [Test]
        public void DrugCartSumTotalTest()
        {
            var newUser = new ApplicationUser
            {
                Id = "utsr",
                Email = "efg@efg.com",
                UserName = "efg@efg.com",
                PhoneNumber = "0908777"
            };
            var singleDrug = new Drug
            {

                Id = 88,
                DrugName = "antraxe",
                Price = 8000,
                Quantity = 35,
                CreatedAt = DateTime.Today,
                ExpiryDate = DateTime.Today.AddDays(9),
                CurrentDrugStatus = DrugStatus.NOT_EXPIRED
            };
            var newdrugCartItems = new List<DrugCartItem>
            {
                new DrugCartItem
                {
                    Id = 80, Amount = 4000, DrugId = 45, Drug = singleDrug, DrugCartId = 191
                }
            };

            var newCart = new DrugCart
            {
                Id = 191,
                CartStatus = CartStatus.ACTIVE,
                ApplicationUser = newUser,
                ApplicationUserId = newUser.Id,
                DrugCartItems = newdrugCartItems
            };
            _mockDrugCart.Setup(z => z.GetDrugCartTotalCount(newUser.Id));
        }
        [Test]
        public void RemoveFromShoppingCartTest()
        {
            var newUser = new ApplicationUser
            {
                Id = "utsr",
                Email = "efg@efg.com",
                UserName = "efg@efg.com",
                PhoneNumber = "0908777"
            };
            var singleDrug = new Drug
            {

                Id = 88,
                DrugName = "antraxe",
                Price = 8000,
                Quantity = 35,
                CreatedAt = DateTime.Today,
                ExpiryDate = DateTime.Today.AddDays(9),
                CurrentDrugStatus = DrugStatus.NOT_EXPIRED
            };
            var newdrugCartItems = new List<DrugCartItem>
            {
                new DrugCartItem
                {
                    Id = 80, Amount = 4000, DrugId = 45, Drug = singleDrug, DrugCartId = 191
                }
            };

            var newCart = new DrugCart
            {
                Id = 191,
                CartStatus = CartStatus.ACTIVE,
                ApplicationUser = newUser,
                ApplicationUserId = newUser.Id,
                DrugCartItems = newdrugCartItems
            };
            _mockDrugCart.Setup(b => b.RemoveFromCart(singleDrug, newUser.Id));

            var result = _cartController.RemoveFromShoppingCart(80) as ViewResult;

            Assert.That(result, Is.EqualTo(null));
        }
    }
}
