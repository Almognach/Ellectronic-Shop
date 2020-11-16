using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Electro_Shop.Data;
using Electro_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Electro_Shop.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly ILogger<ContactUsController> _logger;
        private readonly BranchContext _context;
        private readonly ContactUsContext _Contactcontext;


        public ContactUsController(ILogger<ContactUsController> logger, BranchContext context, ContactUsContext Ccontext)
        {
            _logger = logger;
            _context = context;
            _Contactcontext = Ccontext;

        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                return Redirect("Admin/Messages");
            }
            else
            {
                var _Branches = new List<Branch>();
                _Branches = await _context.branches.ToListAsync();
                var _Submit = new ContactUsSubmit();
                var _ContactUsPage = new ContactUs
                {
                    Branches = _Branches,
                    Submit = _Submit
                };
                return View(_ContactUsPage);
            }
        }

        public async Task<IActionResult> List()
        {
            return View(await _Contactcontext.ContactUsSubmit.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,PhoneNumber,Email,IssueWith")] ContactUsSubmit submit)
        {

            if (ModelState.IsValid)
            {
                //var submits = _Contactcontext.ContactUsSubmit.ToList();
                _Contactcontext.Add(submit);
                //var submits2 = _Contactcontext.ContactUsSubmit.ToList();
                await _Contactcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var _Branches = new List<Branch>();
            _Branches = await _context.branches.ToListAsync();
            var _Submit = new ContactUsSubmit();
            var _ContactUsPage = new ContactUs
            {
                Branches = _Branches,
                Submit = _Submit
            };
            return View("Index", _ContactUsPage);
        }
    }
}
