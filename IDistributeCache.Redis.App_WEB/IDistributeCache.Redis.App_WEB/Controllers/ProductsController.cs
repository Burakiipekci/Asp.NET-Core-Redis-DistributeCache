using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace IDistributeCache.Redis.App_WEB.Controllers
{
    public class ProductsController : Controller
    {
        private IDistributedCache _distributeCache;
        public ProductsController(IDistributedCache distributeCache)
        {
            _distributeCache = distributeCache;
        }
        public IActionResult Index()
        {
            DistributedCacheEntryOptions cache= new DistributedCacheEntryOptions();
            cache.AbsoluteExpiration=DateTime.Now.AddMinutes(1);

            _distributeCache.SetString("test", "burak", cache);


            return View();
        }

        public IActionResult Show()
        {
            string name = _distributeCache.GetString("test");
            ViewBag.Name = name;


            return View();
        }
        public IActionResult Remove()
        {
            DistributedCacheEntryOptions cache = new DistributedCacheEntryOptions();
            cache.AbsoluteExpiration = DateTime.Now.AddMinutes(1);

            _distributeCache.SetString("test", "burak", cache);


            return View();
        }
    }
}
