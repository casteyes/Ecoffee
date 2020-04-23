using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using E_Coffee.Models;
using E_Coffee.Models.ViewModels;

namespace E_Coffee.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(string category, int page = 1)
        {
            //Fetches the Data from the database query
            var products = repository.Products
                                     .Where(p => category == null || p.Category == category)
                                     .OrderBy(p => p.ProductID)
                                     .Skip((page - 1) * PageSize)
                                     .Take(PageSize);
            
            //Count the total number of products in the Db
            var total = 0;
            if(category == null)
            {
                total = repository.Products.Count();
            }
            else
            {
                total = repository.Products.Where(e => e.Category == category).Count();
            }

            //Construct a ViewModel and return it
            var model = new ProductsListViewModel
            {
                Products = products,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = total
                },
                CurrentCategory = category
            };

            return View(model);
        }
    }
}
