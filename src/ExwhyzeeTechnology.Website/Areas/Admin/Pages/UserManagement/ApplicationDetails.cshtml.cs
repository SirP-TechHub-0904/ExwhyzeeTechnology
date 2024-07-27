using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Persistence.EF.SQL;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.UserManagement
{
    public class ApplicationDetailsModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public ApplicationDetailsModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public TrainingApplicationForm TrainingApplicationForm { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TrainingApplicationForm = await _context.TrainingApplicationForms
                .Include(t => t.CareerTrainingJobRole)
                .Include(t => t.Profile).FirstOrDefaultAsync(m => m.ProfileId == id);

            if (TrainingApplicationForm == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
