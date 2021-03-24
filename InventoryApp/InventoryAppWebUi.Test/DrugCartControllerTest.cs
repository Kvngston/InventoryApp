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


namespace InventoryAppWebUi.Test
{
    /// <summary>
    /// Summary description for DrugCartControllerTest
    /// </summary>
    //[TestClass]
    public class DrugCartControllerTest
    {
        private Mock<ApplicationDbContext> _ctx;
        private Mock<ApplicationUserManager> _manager;
        private IEnumerable<Drug> Drugs;
        private IEnumerable<DrugCartItem> CartItems;
        private IEnumerable<DrugCategory> Categories;
        private IEnumerable<DrugCart> DrugCarts;
        private DrugCartService _mockDrugCart;

        public DrugCartControllerTest()
        {
         //_ctx = HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
         //   _manager = HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>();

        }
        [SetUp]
        public void Setup()
        {
            

         
               
         }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
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
            //_ctx = new Mock<ApplicationDbContext>();
            Mock<IDrugCartService> _mockDrug = new Mock<IDrugCartService>();
            _mockDrug.Setup(b => b.GetDrugById(88)).Returns(singleDrug);
            var controller = new DrugCartController(_mockDrug.Object);
            //_ctx.Setup(z => z.DrugCarts.Add(newCart)).Returns(newCart);
            
            var result = controller.GetDrug(88) as ViewResult;

            Assert.AreEqual(result.Model, singleDrug);
        }
        [Test]
        public void IndexTest()
        {
            var userId = Guid.NewGuid();

            Mock<IDrugCartService> _mockDrug = new Mock<IDrugCartService>();
            _mockDrug.Setup(z => z.ClearCart(userId.ToString()));
            var controller = new DrugCartController(_mockDrug.Object);

            var result = controller.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void AddToShoppingCartTest()
        {
            var drugCartId = 909;
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

            Mock<IDrugCartService> _mockDrug = new Mock<IDrugCartService>();
            _mockDrug.Setup(z => z.AddToCart(singleDrug, newUser.Id));
            var _controller = new DrugCartController(_mockDrug.Object);

            var result = _controller.AddToShoppingCart(singleDrug.Id);

            Assert.IsNotNull(result);
        }

        [Test]
        public void ClearCartTest()
        {
            _ctx = new Mock<ApplicationDbContext>();

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

            Mock<IDrugCartService> _mockDrug = new Mock<IDrugCartService>();
            _mockDrug.Setup(z => z.ClearCart(newCart.Id.ToString()));
            //var _drugCartService = new DrugCartService(_ctx.Object);
            //_drugCartService.ClearCart(newUser.Id);
            var _controller = new DrugCartController(_mockDrug.Object);
            var result = _controller.RemoveAllCart();
            Assert.IsNull(newdrugCartItems);
        }

        [Test]
        public void DrugCartTotalCountTest()
        {
            _ctx = new Mock<ApplicationDbContext>();
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
            Mock<IDrugCartService> _mockDrug = new Mock<IDrugCartService>();
              
            var result = _mockDrug.Setup(x => x.GetDrugCartTotalCount(newUser.Id));

            Assert.NotNull(result);
        }

        [Test]
        public void DrugCartSumTotalTest()
        {
            var drugCartId = 909;
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

            Mock<IDrugCartService> _mockDrug = new Mock<IDrugCartService>();
           var result = _mockDrug.Setup(z => z.GetDrugCartTotalCount(newUser.Id));
            //_mockDrug.Verify(v => v.GetDrugCartTotalCount(newUser.Id));
            Assert.IsNotNull(result);
        }



    }
}
