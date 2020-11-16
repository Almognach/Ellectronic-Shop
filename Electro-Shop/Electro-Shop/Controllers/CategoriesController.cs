using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Electro_Shop.Data;
using Electro_Shop.Models;
using Microsoft.AspNetCore.Authorization;

namespace Electro_Shop.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ProductContext _Productcontext;

        public CategoriesController(ProductContext context)
        {
            _Productcontext = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                return Redirect("Admin/Categories");
            } else
            {
                return View(await _Productcontext.Category.ToListAsync());
            }
        }

        //// GET: Categories/Details/5
        //[AdminAuthorizationRequirement]
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var category = await _context.Category
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(category);
        //}

        // GET: Categories/Create
        [AdminAuthorizationRequirement]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminAuthorizationRequirement]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _Productcontext.Add(category);
                await _Productcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        [AdminAuthorizationRequirement]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _Productcontext.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminAuthorizationRequirement]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _Productcontext.Update(category);
                    await _Productcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        [AdminAuthorizationRequirement]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _Productcontext.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [AdminAuthorizationRequirement]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _Productcontext.Category.FindAsync(id);
            _Productcontext.Category.Remove(category);
            await _Productcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _Productcontext.Category.Any(e => e.Id == id);
        }

        public IActionResult ListProducts(int categoryId)
        {

            return RedirectToAction("Index", "Products", categoryId);
        }

        public async Task<IActionResult> List(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var category = await _Productcontext.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            var categoryProducts = new BestSeller();

            var Categories = _Productcontext.Category.ToList();
            categoryProducts.Category = category;

            categoryProducts.Products = _Productcontext.Product.Where(x => x.CategoryId == category.Id).ToList();
            return View(categoryProducts);
        }

    }
}
