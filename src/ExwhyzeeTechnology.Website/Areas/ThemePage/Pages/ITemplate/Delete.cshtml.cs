using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; 

namespace ExwhyzeeTechnology.Website.Areas.ThemePage.Pages.ITemplate
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin")]

    public class DeleteModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public DeleteModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TemplateType TemplateType { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TemplateType = await _context.TemplateTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (TemplateType == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            
            return RedirectToPage("./Index");
        }
    }
}
