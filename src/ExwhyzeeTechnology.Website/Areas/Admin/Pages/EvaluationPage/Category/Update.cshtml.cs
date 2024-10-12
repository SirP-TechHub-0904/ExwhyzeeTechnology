using ExwhyzeeTechnology.Domain.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RazorWebUI.Areas.ITrainings.Pages.EvaluationPage.Category
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class UpdateModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public UpdateModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }


        [BindProperty]
        public EvaluationQuestionCategory EvaluationQuestionCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            EvaluationQuestionCategory = await _context.EvaluationQuestionCategories
                  .FirstOrDefaultAsync(m => m.Id == id);

            if (EvaluationQuestionCategory == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                _context.Attach(EvaluationQuestionCategory).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                TempData["success"] = "Success";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
