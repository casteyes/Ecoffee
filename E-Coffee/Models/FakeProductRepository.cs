using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Coffee.Models
{
    public class FakeProductRepository /* : IProductRepository*/
    {
        public IEnumerable<Product> Products => new List<Product>{
             new Product { Name = "Arabica Coffee", Price = 25 },
             new Product { Name = "Robusta Coffee", Price = 15 },
             new Product { Name = "Liberica Coffee", Price = 40 }
        };

    }
}