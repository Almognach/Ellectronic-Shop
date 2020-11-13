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
    public class ShoppingCartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductContext _ProductContext;
        private readonly ShoppingCartContext _ShoppingCartContext;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly UserManager<ApplicationUser> _UserManager;

        public ShoppingCartController(ILogger<HomeController> logger, ProductContext Productcontext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> SignInManager, ShoppingCartContext shoppingcart)
        {
            _logger = logger;
            _ProductContext = Productcontext;
            _UserManager = userManager;
            _ShoppingCartContext = shoppingcart;
            _SignInManager = SignInManager;
        }
        
        public IActionResult Index()
        {
            var ShoppingCartView = new ShoppingCartView();
            ShoppingCartView.Product_Quantity = new List<Product_Quantity>();

            var userID = ShoppingCartView.UserId = _UserManager.GetUserId(User);

            var Product_IDs = _ShoppingCartContext.ShoppingCartLines.Where(x => x.UserID == userID).ToList();

            foreach (var item in Product_IDs)
            {
                var product = new List<Product>();
                product = _ProductContext.Product.Where(x => x.Id == item.ProductID).ToList();

                ShoppingCartView.Product_Quantity.Add(new Product_Quantity
                {
                    Product = product[0],
                    Quantity = item.Quantity,
                    FinalPrice = product[0].Price * item.Quantity
                });
                ShoppingCartView.FinalPrice += product[0].Price * item.Quantity;
            }       
            return View(ShoppingCartView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, UserID, ProductID, Quantity")] ShoppingCart NewLine)
        {
            if (ModelState.IsValid)
            {
                _ShoppingCartContext.Add(NewLine);
                await _ShoppingCartContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
              return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delete_item = await _ShoppingCartContext.ShoppingCartLines
                .FirstOrDefaultAsync(m => m.ProductID == id && m.UserID == _UserManager.GetUserId(User));
            if (delete_item == null)
            {
                return NotFound();
            }

            //var delete_item = await _ShoppingCartContext.ShoppingCartLines.FindAsync(id);
            _ShoppingCartContext.ShoppingCartLines.Remove(delete_item);
            await _ShoppingCartContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
