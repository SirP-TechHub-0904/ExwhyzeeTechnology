using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Persistence.EF.SQL;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.IOffice.Activity
{        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Manager,Admin")]

    public class DetailsModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public DetailsModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public OfficeActivityDialy OfficeActivityDialy { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OfficeActivityDialy = await _context.OfficeActivityDialies
                .Include(o => o.LastUpdateBy)
                .Include(o => o.OfficeActivityCategory).FirstOrDefaultAsync(m => m.Id == id);

            if (OfficeActivityDialy == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
