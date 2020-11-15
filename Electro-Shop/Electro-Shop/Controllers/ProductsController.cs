using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Electro_Shop.Data;
using Electro_Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Electro_Shop.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Electro_Shop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductContext _Productcontext;
        private readonly CategoryContext _Categorycontext;
        private readonly ShoppingCartContext _ShoppingCartcontext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(ProductContext Productcontext, CategoryContext categorycontext, ShoppingCartContext shoppingcartcontext, UserManager<ApplicationUser> userManager)
        {
            _Productcontext = Productcontext;
            _Categorycontext = categorycontext;
            _ShoppingCartcontext = shoppingcartcontext;
            _userManager = userManager;
        }

        // GET: Products
        public async Task<IActionResult> Index(int ? categoryId = null)
        {
            // Safe to delete, really:
            //var user = new ApplicationUser { Id = "30f2a51f-b0bd-4820-90e6-64ce38a2d559", Email = "Admin@ElectroShop.co.il" };
            //var role = await _userManager.GetRolesAsync(user);
            //foreach (var claim in User.Claims)
            //{
            //    Console.WriteLine(claim);
            //}
            // Until here --------------------------
            // TODO: User category ID to filter the products that will be listed
            
            var role = await _userManager.GetRolesAsync(new ApplicationUser { Id = _userManager.GetUserId(User) });
            if (role.FirstOrDefault() == "Admin") //Identity.Name == "Admin")
            {
                return View("/Areas/AdminCenter/Views/Categories/AdminIndex", await _Productcontext.Product.ToListAsync());

            } else
            {
                return View(await _Productcontext.Product.ToListAsync());
            }

            // TODO: Create authorize attribute derivative and change role check methodology
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _Productcontext.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var category = await _Categorycontext.Category
                .FirstOrDefaultAsync(m => m.Id == product.Category.Id);
            if (category == null)
            {
                return NotFound();
            }

            var productCategory = (from p in _Productcontext.Product
                          join c in _Productcontext.Category on p.Category.Id equals category.Id
                          select new
                          {
                           p.Id,
                           p.Brand,
                           p.Model,
                           p.Description,
                           p.PathToImage,
                           p.Price,
                           p.Supplier,
                           p.InStock,
                           p.SalesCounter,
                           c.Name
                          }).ToList();

            var productcategoryjoined = new ProdudctCategoryJoined
            {
                Id = productCategory[0].Id,
                Brand = productCategory[0].Brand,
                Model = productCategory[0].Model,
                Description = productCategory[0].Description,
                PathToImage = productCategory[0].PathToImage,
                Price = productCategory[0].Price,
                Supplier = productCategory[0].Supplier,
                InStock = productCategory[0].InStock,
                SalesCounter = productCategory[0].SalesCounter,
                CategoryName = productCategory[0].Name
            };

            var productDetails = new ProductDetails
                {
                category = category,
                productcategory = productcategoryjoined,
                shoppingCart = new ShoppingCart()
            };

            return View(productDetails);
        }

        [Authorize(Roles = "Admin")]
        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,Model,Price,Description,SalesCounter,InStock,PathToImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                _Productcontext.Add(product);
                await _Productcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _Productcontext.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Model,Price,Description,SalesCounter,InStock,PathToImage")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _Productcontext.Update(product);
                    await _Productcontext.SaveChangesAsync();
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

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _Productcontext.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _Productcontext.Product.FindAsync(id);
            _Productcontext.Product.Remove(product);
            await _Productcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _Productcontext.Product.Any(e => e.Id == id);
        }
    }
}
