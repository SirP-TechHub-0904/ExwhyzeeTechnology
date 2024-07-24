using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExwhyzeeTechnology.Website.Areas.JPTask.Pages.ProjectAct
{
    [Authorize]
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
        public XProject XProject { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            XProject.CreatedDate = DateTime.UtcNow.AddHours(1);
            _context.XProjects.Add(XProject);
            await _context.SaveChangesAsync();
            TempData["success"] = "Successfully";
            return RedirectToPage("./Details", new {id = XProject.Id});
        }
    }
}
