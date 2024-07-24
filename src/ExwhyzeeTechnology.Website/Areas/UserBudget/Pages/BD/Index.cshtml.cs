using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Persistence.EF.SQL;
using Microsoft.AspNetCore.Identity;
using System.Web.Mvc;

namespace ExwhyzeeTechnology.Website.Areas.UserBudget.Pages.BD
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<BudgetMainCategory> BudgetMainCategory { get;set; }
        //public IList<BudgetMainCategory> BudgetMainCategoryList { get;set; }

        public async Task OnGetAsync()
        {
            string uid = _userManager.GetUserId(HttpContext.User);
            BudgetMainCategory = await _context.BudgetMainCategories
                .Include(b => b.User)
                .Where(x=>x.UserId == uid).ToListAsync();

            var user = await _userManager.FindByIdAsync(uid);

        }
    }
}
