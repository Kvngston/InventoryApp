using System.Web.Mvc;
using inventoryAppDomain.Services;

namespace inventoryAppWebUi.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        
        // GET
        public ActionResult Index()
        {
            var roles = _roleService.GetAllRoles();
            return View(roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string roleName)
        {
            var role = _roleService.Create(roleName);
            return RedirectToAction("Index");
        }
    }
}