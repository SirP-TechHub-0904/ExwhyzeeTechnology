using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Areas.Content.Pages.ISettings
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Manager,Admin,Editor")]

    public class DetailsModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public DetailsModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public Setting Setting { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Setting = await _context.Settings.FirstOrDefaultAsync(m => m.Id == id);

            if (Setting == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
