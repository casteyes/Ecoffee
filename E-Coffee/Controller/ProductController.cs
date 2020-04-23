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
               => View(new ProductsListViewModel {
                   Products = repository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                   PagingInfo = new PagingInfo
                   {
                       CurrentPage = page,
                       ItemsPerPage = PageSize,
                       TotalItems = category == null ?
                        repository.Products.Count() :
                        repository.Products.Where(e =>
                        e.Category == category).Count()
                   },
                   CurrentCategory = category
               });


    }
}
