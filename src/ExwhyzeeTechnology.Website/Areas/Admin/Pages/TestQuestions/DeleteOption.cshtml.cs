using ExwhyzeeTechnology.Domain.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RazorWebUI.Areas.ITrainings.Pages.TestQuestions
{
    public class DeleteOptionModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public DeleteOptionModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }


        [BindProperty]
        public TrainingTestOption TrainingTestOption { get; set; }
        public TrainingTest TrainingTest { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            TrainingTestOption = await _context.TrainingTestOptions.FirstOrDefaultAsync(m => m.Id == id);

            if (TrainingTestOption == null)
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
                TrainingTestOption = await _context.TrainingTestOptions.FindAsync(TrainingTestOption.Id);

            if (TrainingTestOption != null)
            {
                _context.TrainingTestOptions.Remove(TrainingTestOption);
                await _context.SaveChangesAsync();
            }
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
