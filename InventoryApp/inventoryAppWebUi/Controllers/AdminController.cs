using System.Web.Mvc;

namespace inventoryAppWebUi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return null;
        }
    }
}