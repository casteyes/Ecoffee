using Microsoft.AspNetCore.Mvc;
using E_Coffee.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using E_Coffee.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using E_Coffee.Infrastructure;

namespace E_Coffee.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IWebHostEnvironment environment;
        private IProductRepository repository;
        public AdminController(IWebHostEnvironment env, IProductRepository repo)
        {
            environment = env;
            repository = repo;
        }
        public ViewResult Index()
        {
            var products = repository.Products.Select(p => Mapper.Map(p));
            return View(products);
        }

        public ViewResult Edit(int productId)
        {
            var product = repository.Products.Select(p => Mapper.Map(p)).FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Create Product Entity
                    var product = Mapper.Map(model);

                    if(model.ImageFile != null)
                    {
                        product.Image = SaveImage(model.ImageFile);
                    }

                    repository.SaveProduct(product);

                    //Update ProductID in the ViewModel
                    model.ProductID = product.ProductID;

                    TempData["message"] = $"{product.Name} has been saved";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ImageFile", ex.Message);
                    return View(model);
                }
            }
            else
            {
                // there is something wrong with the data values
                return View(model);
            }
        }

        /// <summary>
        /// Save upload to Images Folder
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        private string SaveImage(IFormFile upload)
        {
            //Check the extension of the upload as it could be a security vulnerability
            var extension = Path.GetExtension(upload.FileName.ToLower());
            switch (extension)
            {
                case ".jpg":
                case ".png":
                case ".jpeg":
                case ".gif":
                    //Do Nothing
                    break;
                default:
                    throw new Exception("Invalid Image was uploaded");
            }


            //Generate a Unique Filename
            var guid = Guid.NewGuid().ToString();

            // C:\App\wwwroot\images\products\1239192312931.jpg
            var filePath = $"{environment.WebRootPath}/images/products/{guid}{extension}";

            //Copy the contents of the upload to the image file
            using (var fileStream = System.IO.File.OpenWrite(filePath))
            {
                upload.CopyTo(fileStream);
            }

            //return relative url
            return $"/images/products/{guid}{extension}";
        }

        public ViewResult Create() => View("Edit", new ProductModel());
        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }
            return RedirectToAction("Index");
        }


    }

}
