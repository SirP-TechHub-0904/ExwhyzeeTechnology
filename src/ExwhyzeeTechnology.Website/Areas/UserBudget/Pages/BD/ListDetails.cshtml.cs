using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Areas.UserBudget.Pages.BD
{

    [Authorize]
    public class ListDetailsModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public ListDetailsModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public BudgetList BudgetList { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BudgetList = await _context.BudgetLists
                .Include(b => b.BudgetSubCategory).FirstOrDefaultAsync(m => m.Id == id);

            if (BudgetList == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
