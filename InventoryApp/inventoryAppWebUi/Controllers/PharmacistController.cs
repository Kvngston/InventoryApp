using System.Web;
using System.Web.Mvc;
using inventoryAppDomain.IdentityEntities;
using Microsoft.AspNet.Identity.Owin;

namespace inventoryAppWebUi.Controllers
{
    public class PharmacistController : Controller
    {
        private ApplicationUserManager _userManager;

        public PharmacistController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }        // GET
    }
}