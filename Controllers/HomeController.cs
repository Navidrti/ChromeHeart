using ChromeHeart.Areas.Identity.Data;
using ChromeHeart.Data;
using ChromeHeart.Models;
using ChromeHeart.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Shop([FromServices] DbWebFinal db)
        {
            var products = db.products.ToList();
            List<ProductViewModels> lstProducts = new List<ProductViewModels>();
            products.ForEach(x =>
            {
                ProductViewModels p = new ProductViewModels();
                p.id = x.id;
                p.Name = x.Name;
                p.Price = x.Price;
                p.Count = x.Count;
                p.imgBytes = x.img;
                lstProducts.Add(p);
            });
            return View(lstProducts);
        }
        public IActionResult Team()
        {
            return View();
        }
        public IActionResult Honors()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Single([FromServices] DbWebFinal db)
        {
            return View();
        }
        public IActionResult CheckOut()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult AdminDash()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Detail(int id, [FromServices] DbWebFinal db)
        {
            Products p = db.products.FirstOrDefault(c=>c.id == id);
            return View(p);
        }
    }
}
