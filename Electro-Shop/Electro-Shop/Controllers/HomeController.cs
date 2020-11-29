using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Electro_Shop.Models;
using Electro_Shop.Data;

namespace Electro_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductContext _Pcontext;

        public HomeController(ILogger<HomeController> logger, ProductContext Pcontext)
        {
            _logger = logger;
            _Pcontext = Pcontext;
        }

        public IActionResult Index()
        {
            var TopProducts = _Pcontext.Product.OrderByDescending(x => x.SalesCounter).Take(12).ToList();

            var BestSellers = new BestSeller{ Name = null, Products = TopProducts };
         
            return View(BestSellers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
