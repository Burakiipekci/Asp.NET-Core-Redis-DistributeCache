using Microsoft.AspNetCore.Mvc;

namespace IDistributeCache.Redis.App_WEB.Controllers
{
    public class Products : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
