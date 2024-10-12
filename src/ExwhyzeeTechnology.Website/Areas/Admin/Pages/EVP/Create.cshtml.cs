using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using ExwhyzeeTechnology.Domain.Models;
using Amazon.Runtime;
using ExwhyzeeTechnology.Domain.Models.Data;

namespace ExwhyzeeTechnology.Website.Areas.EVP.Pages.EVP
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Admin")]

    public class CreateModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public CreateModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public EvaluationQuestion EvaluationQuestion { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
           

            _context.EvaluationQuestions.Add(EvaluationQuestion);
            await _context.SaveChangesAsync(); TempData["success"] = "Successful";

            return RedirectToPage("./Index");
        }
    }
}
