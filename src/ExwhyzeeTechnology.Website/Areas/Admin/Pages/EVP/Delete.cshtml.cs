using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Domain.Models.Data;

namespace ExwhyzeeTechnology.Website.Areas.EVP.Pages.EVP
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Admin")]

    public class DeleteModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public DeleteModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EvaluationQuestion EvaluationQuestion { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EvaluationQuestion = await _context.EvaluationQuestions.FirstOrDefaultAsync(m => m.Id == id);

            if (EvaluationQuestion == null)
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

            EvaluationQuestion = await _context.EvaluationQuestions.FindAsync(id);

            if (EvaluationQuestion != null)
            {
                _context.EvaluationQuestions.Remove(EvaluationQuestion);
                await _context.SaveChangesAsync(); TempData["success"] = "Successful";
            }

            return RedirectToPage("./Index");
        }
    }
}
