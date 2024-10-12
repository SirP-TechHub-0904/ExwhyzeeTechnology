using ExwhyzeeTechnology.Domain.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RazorWebUI.Areas.ITrainings.Pages.TestQuestions
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
        public TrainingTest TrainingTest { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            TrainingTest = await _context.TrainingTests.FirstOrDefaultAsync(m => m.Id == id);

            if (TrainingTest == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                _context.Attach(TrainingTest).State = EntityState.Modified;
                 
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
