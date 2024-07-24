using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Areas.UI.Pages
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,SubAdmin")]

    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public SuperSetting SuperSetting { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            SuperSetting = await _context.SuperSettings.FirstOrDefaultAsync();

            return Page();
        }
    }
}
