using ChromeHeart.Areas.Identity.Data;
using ChromeHeart.Data;
using ChromeHeart.Models;
using ChromeHeart.Service;
using ChromeHeart.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace ChromeHeart.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUserService userService;

        public ProductsController(IUserService userService)
        {
            this.userService = userService;
        }
        public IActionResult Insert([FromServices] DbWebFinal db, ProductViewModels x)
        {

            Products p = new Products();
            p.Name = x.Name;
            if (p.Name != null)
            {
                p.Price = x.Price;
                p.Count = x.Count;
                if (x.img != null)
                {
                    byte[] b = new byte[x.img.Length];
                    x.img.OpenReadStream().Read(b, 0, b.Length);
                    p.img = b;
                }
                db.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                { db.SaveChanges(); return View(); }
            }
        }
        public IActionResult Buy([FromServices] DbWebFinal db)
        {
            Orders o = new Orders();
            ProductViewModels p = new ProductViewModels();
            var userId = userService.GetUserId();
            var product = db.products.FirstOrDefault(x => x.id == x.id);
            o.name = product.Name;
            o.price = product.Price;
            o.count = 1;
            o.Userid = userId;
            if (o.Userid == null)
            {
                return RedirectToAction("Login", "Home");
            }
            o.UserName = db.Users.FirstOrDefault(x => x.Id == o.Userid).UserName;
            product.Count -= o.count;
            db.Add(o);
            db.SaveChanges();
            return RedirectToAction("ListOfOrders3", "Products");
        }
        public IActionResult ShowProducts([FromServices] DbWebFinal db)
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
        public IActionResult ListOfOrders([FromServices] DbWebFinal db)
        {
            var orders = db.orders.ToList();
            return View(orders);
        }
        public IActionResult ListOfOrders3([FromServices] DbWebFinal db)
        {
            var orders = db.orders.ToList();
            var userId = userService.GetUserId();
            var p = orders.Where(x => x.Userid == userId).ToList();


            return View(p);
        }
        public IActionResult Delete([FromServices] DbWebFinal db, int id)
        {
            var p = db.products.Find(id);
            db.Remove(p);
            db.SaveChanges();
            return RedirectToAction("ShowProducts");
        }
        public IActionResult Edit([FromServices] DbWebFinal db, int id)
        {
            var p = db.products.Find(id);
            ProductViewModels model = new ProductViewModels();
            model.id = p.id;
            model.Name = p.Name;
            model.Price = p.Price;
            model.Count = p.Count;
            model.imgBytes = p.img;
            return View(model);
        }
        public IActionResult Update([FromServices] DbWebFinal db, ProductViewModels models)
        {
            var p = db.products.Find(models.id);
            p.Name = models.Name;
            p.Price = models.Price;
            p.Count = models.Count;
            if (models.img != null)
            {
                byte[] b = new byte[models.img.Length];
                models.img.OpenReadStream().Read(b, 0, b.Length);
                p.img = b;
            }
            db.Update(p);
            db.SaveChanges();
            return RedirectToAction("ShowProducts");
        }

    }
}
