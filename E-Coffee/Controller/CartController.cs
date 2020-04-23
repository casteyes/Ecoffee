using System.Linq;
using Microsoft.AspNetCore.Mvc;
using E_Coffee.Models;
using E_Coffee.Models.ViewModels;


namespace E_Coffee.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private Cart cart;
        public CartController(IProductRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }
        public ViewResult Index(string returnUrl)
        {
            var model = new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            };
            return View(model);
        }
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            var product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            var product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}

