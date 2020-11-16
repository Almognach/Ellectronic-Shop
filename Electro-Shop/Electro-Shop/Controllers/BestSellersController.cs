using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Electro_Shop.Data;
using Electro_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Electro_Shop.Controllers
{
    public class BestSellersController : Controller
    {
        private readonly CategoryContext _Ccontext;
        private readonly ProductContext _Pcontext;
        // GET: /<controller>/



        public BestSellersController(CategoryContext Ccontext, ProductContext Pcontext)
        {
            _Ccontext = Ccontext;
            _Pcontext = Pcontext;

        }

        // GET: Categories
        public IActionResult Index()
        {
            var BestSellers = new List<BestSeller>();
           
            var Categories = _Ccontext.Category.ToList();

            foreach (var item in Categories)
            {
                var TopProducts = _Pcontext.Product.Where(x => x.CategoryId == item.Id).OrderByDescending(x => x.SalesCounter).Take(4).ToList();

                BestSellers.Add(new BestSeller { Category = item, Products = TopProducts });
            }
            return View(BestSellers);
        }

    }
}