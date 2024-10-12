using ExwhyzeeTechnology.Domain.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.TimeTablePage
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Admin,Editor")]

    public class CreateModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public CreateModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            var courseList = await _context.Cohorts.Include(x => x.Course).ToListAsync();
            var oulist = courseList.Select(x => new DropDownListCourse
            {
                Id = x.Id,
                Title = x.Course.Name + " (" + x.Course.Abbreviation + " " + x.CohortNumber + " - " + x.Year.ToString("yyyy") + ")",
            }).ToList();

            ViewData["CourseId"] = new SelectList(oulist, "Id", "Title"); return Page();
        }

        [BindProperty]
        public TimeTable TimeTable { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            
            _context.TimeTables.Add(TimeTable);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
            //return RedirectToPage("./Details", new {id = TimeTable.Id});
        }
    }
    public class DropDownListCourse
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
    }
}
