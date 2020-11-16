using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Electro_Shop.Models
{
    public class ProductDetails
    {
        public ProdudctCategoryJoined productcategory { get; set; }
        public  Category category { get; set; }
        public ShoppingCart shoppingCart{ get; set; }
    }
}

public class ProdudctCategoryJoined

{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Description { get; set; }
    public System.Uri PathToImage { get; set; }
    public double Price { get; set; }
    public Electro_Shop.Models.Supplier Supplier { get; set; }
    public int InStock { get; set; }
    public int SalesCounter { get; set; }
    public string CategoryName { get; set; }
}