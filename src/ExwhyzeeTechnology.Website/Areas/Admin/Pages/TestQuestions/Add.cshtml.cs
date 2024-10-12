using ExwhyzeeTechnology.Domain.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.TestQuestions
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class AddModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public AddModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TrainingTest TrainingTest { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                _context.TrainingTests.Add(TrainingTest);
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
