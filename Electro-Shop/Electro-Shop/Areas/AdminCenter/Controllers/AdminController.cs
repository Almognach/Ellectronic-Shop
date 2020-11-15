using Electro_Shop.Areas.Identity.Data;
using Electro_Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electro_Shop.Areas.AdminCenter.Controllers
{
    [AdminAuthorizationRequirement]
    public class AdminController : Controller
    {
        private readonly ProductContext _ProductContext;
        private readonly CategoryContext _CategoryContext;
        private readonly SupplierContext _SupplierContext;
        private readonly AuthDbContext _UserContext;
        private readonly ContactUsContext _MessagesContext;
        private readonly OrdersContext _OrdersContext;
        private readonly UserManager<ApplicationUser> _userManager;


        private ActionResult AdminView(string name, object model = null)
        {
            return View("/Areas/AdminCenter/Views/Admin/" + name + ".cshtml", model);
        }

        public AdminController(ProductContext Productcontext, CategoryContext categorycontext,
            SupplierContext SupplierContext, AuthDbContext UserContext, ContactUsContext MessagesContext,
            OrdersContext OrdersContext, UserManager<ApplicationUser> userManager)
        {
            _ProductContext = Productcontext;
            _CategoryContext = categorycontext;
            _SupplierContext = SupplierContext;
            _UserContext = UserContext;
            _MessagesContext = MessagesContext;
            _OrdersContext = OrdersContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return AdminView("Index");
        }

        public async Task<IActionResult> ProductsAsync()
        {
            return AdminView("Products", await _ProductContext.Product.ToListAsync());
        }

        public async Task<IActionResult> CategoriesAsync()
        {
            return AdminView("Categories", await _CategoryContext.Category.ToListAsync());
        }

        public async Task<IActionResult> SuppliersAsync()
        {
            return AdminView("Suppliers", await _SupplierContext.Supplier.ToListAsync());
        }

        public async Task<IActionResult> UsersAsync()
        {
            return AdminView("Users", await _UserContext.Users.ToListAsync());
        }

        public async Task<IActionResult> MessagesAsync()
        {
            return AdminView("Messages", await _MessagesContext.ContactUsSubmit.ToListAsync());
        }

        public async Task<IActionResult> OrdersAsync()
        {
            return AdminView("Orders", await _OrdersContext.OrderLines.ToListAsync());
        }
    }
}
