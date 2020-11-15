using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Electro_Shop.Data;
using Electro_Shop.Models;

namespace Electro_Shop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductContext _Productcontext;
        private readonly CategoryContext _Categorycontext;
        private readonly ShoppingCartContext _ShoppingCartcontext;

        public ProductsController(ProductContext Productcontext, CategoryContext categorycontext, ShoppingCartContext shoppingcartcontext)
        {
            _Productcontext = Productcontext;
            _Categorycontext = categorycontext;
            _ShoppingCartcontext = shoppingcartcontext;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _Productcontext.Product.ToListAsync());
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
                .FirstOrDefaultAsync(m => m.Id == product.CategoryId);
            if (category == null)
            {
                return NotFound();
            }

            var productCategory = (from p in _Productcontext.Product
                          join c in _Productcontext.Category on p.CategoryId equals c.Id
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
                          }).Where(x => x.Id == id).ToList();

            var productcategoryjoined = new ProdudctCategoryJoined()
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

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
