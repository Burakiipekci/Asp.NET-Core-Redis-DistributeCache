using IDistributeCache.Redis.App_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace IDistributeCache.Redis.App_WEB.Controllers
{
    public class ProductsController : Controller
    {
        private IDistributedCache _distributeCache;
        public ProductsController(IDistributedCache distributeCache)
        {
            _distributeCache = distributeCache;
        }
        public async Task<IActionResult> Index()
        {
            DistributedCacheEntryOptions cache = new DistributedCacheEntryOptions();
            cache.AbsoluteExpiration = DateTime.Now.AddMinutes(30);
            Product product = new Product { Id = 1, Name = "Kalem", Price = 100 };
            string jsonProduct = JsonConvert.SerializeObject(product);
            await _distributeCache.SetStringAsync("product:1", jsonProduct);





            return View();
        }

        public  IActionResult Show()
        {
          
            string jsonProducts = _distributeCache.GetString("product:1"); 
           Product convert= JsonConvert.DeserializeObject<Product>(jsonProducts);
            ViewBag.convert = convert;


            return View();
        }
        public IActionResult Remove()
        {
            DistributedCacheEntryOptions cache = new DistributedCacheEntryOptions();
            cache.AbsoluteExpiration = DateTime.Now.AddMinutes(1);

            _distributeCache.SetString("test", "burak", cache);


            return View();
        }
        public IActionResult ImageCache()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/1400x1050elektrikli-araba-fiyatlari-2023.jpg");
            byte[] imageByte= System.IO.File.ReadAllBytes(path);

            _distributeCache.Set("image", imageByte);
            return View();
        }
        public IActionResult ImageShow()
        {
            byte[] imageByte=_distributeCache.Get("image");
            return File(imageByte, "image.jpg");

        }
    }
}
