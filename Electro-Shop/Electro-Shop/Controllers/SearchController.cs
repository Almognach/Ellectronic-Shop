using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Electro_Shop.Models;
using Electro_Shop.Data;
using Electro_Shop.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Electro_Shop.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductContext _ProductContext;
        private readonly ShoppingCartContext _ShoppingCartContext;
        private readonly OrdersContext _OrderContext;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly UserManager<ApplicationUser> _UserManager;

        public SearchController(ILogger<HomeController> logger, ProductContext Productcontext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> SignInManager, ShoppingCartContext shoppingcart, OrdersContext orders)
        {
            _logger = logger;
            _ProductContext = Productcontext;
            _UserManager = userManager;
            _ShoppingCartContext = shoppingcart;
            _SignInManager = SignInManager;
            _OrderContext = orders;
        }

        public IActionResult Index([Bind("KeyID")] Search search)
        {

            var categories = _ProductContext.Category.Where(x => x.Name.Contains(search.KeyID)).ToList();

            var products_brand = _ProductContext.Product.Where(x => x.Brand.Contains(search.KeyID)).ToList();

            var products_model = _ProductContext.Product.Where(x => x.Model.Contains(search.KeyID)).ToList();

            var results = new List<Product>();

            foreach (var product in products_brand)
            {
                results.Add(product);
            };

            foreach (var product in products_model)
            {
                results.Add(product);
            };

            foreach (var category in categories)
            {
                var productInCategory = _ProductContext.Product.Where(x => x.CategoryId == category.Id).ToList();

                foreach (var product in productInCategory)
                {
                    results.Add(product);
                }
            }

            return View(results);
        }
        }
    }