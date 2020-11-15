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
    public class OrdersController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductContext _ProductContext;
        private readonly ShoppingCartContext _ShoppingCartContext;
        private readonly OrdersContext _OrderContext;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly UserManager<ApplicationUser> _UserManager;

        public OrdersController(ILogger<HomeController> logger, ProductContext Productcontext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> SignInManager, ShoppingCartContext shoppingcart, OrdersContext orders)
        {
            _logger = logger;
            _ProductContext = Productcontext;
            _UserManager = userManager;
            _ShoppingCartContext = shoppingcart;
            _SignInManager = SignInManager;
            _OrderContext = orders;
        }

        public IActionResult PastOrders()
        {
            var pastOrders = new PastOrdersView();
            pastOrders.UserID = _UserManager.GetUserId(User);
            pastOrders.OrdersView = new List<OrderView>();


            var allMyOrders = _OrderContext.Orders.Where(x => x.UserID == pastOrders.UserID).ToList();

            foreach (var order in allMyOrders)
            {
                var tempPastOrder = new OrderView()
                {
                    Order = order
                };
                tempPastOrder.OrderLines = new List<OrderLine>();
                var ThisOrderLines = _OrderContext.OrderLines.Where(x => x.UserID == pastOrders.UserID && x.OrderID == order.OrderID).ToList();
                foreach (var line in ThisOrderLines)
                {
                    tempPastOrder.OrderLines.Add(line);
                    tempPastOrder.PricePaid += line.FullPrice;
                }
                pastOrders.OrdersView.Add(tempPastOrder);
            }

            return View(pastOrders);
        }

        public async Task<IActionResult> Reorder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order_id = await _OrderContext.Orders.FirstOrDefaultAsync(m => m.OrderID == id);
            if (order_id == null)
            {
                return NotFound();
            }

            var allMyOrderLines = _OrderContext.OrderLines.Where(x => x.OrderID == order_id.OrderID).ToList();

            foreach (var line in allMyOrderLines)
            {
                var ShoppingCartLine = new ShoppingCart
                {
                    ProductID = line.ProductID,
                    Quantity = line.Quantity,
                    UserID = line.UserID
                };
                await Create(ShoppingCartLine);
                }
            return RedirectToAction("Index", "ShoppingCart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId ,UserID, , ProductID, Quantity")] ShoppingCart NewLine)
        {

            if (ModelState.IsValid)
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
}
}