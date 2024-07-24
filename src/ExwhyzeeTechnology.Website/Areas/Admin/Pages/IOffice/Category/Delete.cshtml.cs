using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Persistence.EF.SQL;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.IOffice.Category
{        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Manager,Admin")]

    public class DeleteModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public DeleteModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OfficeActivityCategory OfficeActivityCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OfficeActivityCategory = await _context.OfficeActivityCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (OfficeActivityCategory == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OfficeActivityCategory = await _context.OfficeActivityCategories.FindAsync(id);

            if (OfficeActivityCategory != null)
            {
                _context.OfficeActivityCategories.Remove(OfficeActivityCategory);
                //await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
