using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electro_Shop.Areas.AdminCenter.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View("Areas/AdminCenter/Views/Admin/Index.cshtml");
        }
    }
}
