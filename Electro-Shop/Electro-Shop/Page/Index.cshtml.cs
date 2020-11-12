using System.Collections.Generic;
using System.Linq;
using Electro_Shop.Data;
using Electro_Shop.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Electro_Shop.Page
{

    
    public class IndexModel : PageModel
    {
        private ProductContext productdb;
        public List<Product> Products;

        public IndexModel(ProductContext _productdb)
        {
            productdb = _productdb;
        }


        public void OnGet()
        {
            Products = productdb.Product.ToList();

        }

    }
}
