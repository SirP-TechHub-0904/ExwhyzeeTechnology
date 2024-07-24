using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Persistence.EF.SQL;

namespace ExwhyzeeTechnology.Website.Areas.UserBudget.Pages.BList
{
    public class CreateModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public CreateModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BudgetSubCategoryId"] = new SelectList(_context.BudgetSubCategories, "Id", "Description");
            return Page();
        }

        [BindProperty]
        public BudgetList BudgetList { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BudgetLists.Add(BudgetList);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
