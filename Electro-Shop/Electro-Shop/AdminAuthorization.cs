using Electro_Shop.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electro_Shop
{
    public class AdminAuthorizationRequirementAttribute : TypeFilterAttribute
    {
        public AdminAuthorizationRequirementAttribute() : base(typeof(AdminAuthorizationFilter))
        {

        }
    }

    public class AdminAuthorizationFilter : IAuthorizationFilter
    {
        UserManager<ApplicationUser> _userManager;

        public AdminAuthorizationFilter(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var role = _userManager.GetRolesAsync(new ApplicationUser { Id = _userManager.GetUserId(context.HttpContext.User) }).Result;
             if (!role. Contains("Admin"))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
