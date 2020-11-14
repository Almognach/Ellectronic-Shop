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
        private readonly OrdersContext _OrderContext;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly UserManager<ApplicationUser> _UserManager;

        public ShoppingCartController(ILogger<HomeController> logger, ProductContext Productcontext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> SignInManager, ShoppingCartContext shoppingcart, OrdersContext orders)
        {
            _logger = logger;
            _ProductContext = Productcontext;
            _UserManager = userManager;
            _ShoppingCartContext = shoppingcart;
            _SignInManager = SignInManager;
            _OrderContext = orders;
        }

        public IActionResult Index()
        {
            var ShoppingCartView = new ShoppingCartView();
            ShoppingCartView.Product_Quantity = new List<Product_Quantity>();

            var userID = ShoppingCartView.UserId = _UserManager.GetUserId(User);

            var Product_IDs = _ShoppingCartContext.ShoppingCartLines.Where(x => x.UserID == userID).ToList();

            var list_id = new List<int>();

            foreach (var item in Product_IDs)
            {
                var product = new List<Product>();
                product = _ProductContext.Product.Where(x => x.Id == item.ProductID).ToList();

                if (list_id.Contains(item.ProductID))
                {
                    var exists = ShoppingCartView.Product_Quantity.Where(x => x.Product.Id == item.ProductID).ToList();
                    ShoppingCartView.Product_Quantity.Remove(exists[0]);
                    exists[0].Quantity += item.Quantity;
                    exists[0].FinalPrice = product[0].Price * exists[0].Quantity;
                    ShoppingCartView.Product_Quantity.Add(exists[0]);
                }
                else
                {
                    list_id.Add(item.ProductID);
                    ShoppingCartView.Product_Quantity.Add(new Product_Quantity
                    {
                        Product = product[0],
                        Quantity = item.Quantity,
                        FinalPrice = product[0].Price * item.Quantity
                    });
                }
                ShoppingCartView.FinalPrice += product[0].Price * item.Quantity;
            }
            return View(ShoppingCartView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId ,UserID, , ProductID, Quantity")] ShoppingCart NewLine)
        {
            
            if (ModelState.IsValid && NewLine.Quantity > 0)
            {
                for (int i = 0; i < NewLine.Quantity; i++)
                {
                    ShoppingCart OneNewLine = new ShoppingCart
                    {
                        ProductID = NewLine.ProductID,
                        UserID = NewLine.UserID,
                        Quantity = 1
                    };
                    _ShoppingCartContext.Add(OneNewLine);
                    await _ShoppingCartContext.SaveChangesAsync();
                }

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

        public async Task<IActionResult> CheckOut()
        {

            var ShoppingCartView = new ShoppingCartView();
            ShoppingCartView.Product_Quantity = new List<Product_Quantity>();

            var userID = ShoppingCartView.UserId = _UserManager.GetUserId(User);

            var Product_IDs = _ShoppingCartContext.ShoppingCartLines.Where(x => x.UserID == userID).ToList();

            if (Product_IDs.Count < 1)
            {
                return RedirectToAction(nameof(Index));
            }


            var list_id = new List<int>();

            foreach (var item in Product_IDs)
            {
                var product = new List<Product>();
                product = _ProductContext.Product.Where(x => x.Id == item.ProductID).ToList();

                if (list_id.Contains(item.ProductID))
                {
                    var exists = ShoppingCartView.Product_Quantity.Where(x => x.Product.Id == item.ProductID).ToList();
                    ShoppingCartView.Product_Quantity.Remove(exists[0]);
                    exists[0].Quantity += item.Quantity;
                    exists[0].FinalPrice = product[0].Price * exists[0].Quantity;
                    ShoppingCartView.Product_Quantity.Add(exists[0]);
                }
                else
                {
                    list_id.Add(item.ProductID);
                    ShoppingCartView.Product_Quantity.Add(new Product_Quantity
                    {
                        Product = product[0],
                        Quantity = item.Quantity,
                        FinalPrice = product[0].Price * item.Quantity
                    });
                }
                ShoppingCartView.FinalPrice += product[0].Price * item.Quantity;
            }

            Order NewOrder = new Order
            {
                UserID = userID,
                TimeStamp = DateTime.Now.AddHours(2).ToString("g")
            };
            _OrderContext.Add(NewOrder);
            await _OrderContext.SaveChangesAsync();

            NewOrder = await _OrderContext.Orders.Where(x => x.UserID == userID).OrderByDescending(x => x.TimeStamp).FirstOrDefaultAsync();
            if (NewOrder == null)
            {
                return NotFound();
            }
            var UserOrderLines = new List<OrderLine>();
            foreach (var item in ShoppingCartView.Product_Quantity)
            {
                var newOrderLine = new OrderLine
                {
                    OrderID = NewOrder.OrderID,
                    UserID = userID,
                    Quantity = item.Quantity,
                    ProductID = item.Product.Id,
                    FullPrice = item.FinalPrice
                };

                _OrderContext.Add(newOrderLine);
                await _OrderContext.SaveChangesAsync();

                UserOrderLines.Add(newOrderLine);

                for (int i = 0; i < item.Quantity; i++)
                {
                    await Delete(item.Product.Id);
                    await AddSalesCounter(item.Product.Id, item.Product);
                }


            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddSalesCounter(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            product.SalesCounter += 1;
            if (ModelState.IsValid)
            {
                try
                {
                    _ProductContext.Update(product);
                    await _ProductContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    private bool ProductExists(int id)
    {
        return _ProductContext.Product.Any(e => e.Id == id);
    }
}
}