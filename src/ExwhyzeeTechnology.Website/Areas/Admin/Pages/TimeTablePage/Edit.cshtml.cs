using ExwhyzeeTechnology.Domain.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.TimeTablePage
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Admin,Editor")]

    public class EditModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public EditModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TimeTable TimeTable { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TimeTable = await _context.TimeTables
                .Include(t => t.Cohort).FirstOrDefaultAsync(m => m.Id == id);

            if (TimeTable == null)
            {
                return NotFound();
            }
            var courseList = await _context.Cohorts.Include(x => x.Course).ToListAsync();
            var oulist = courseList.Select(x => new DropDownListCourse
            {
                Id = x.Id,
                Title = x.Course.Name + " (" + x.Course.Abbreviation + " " + x.CohortNumber + " - " + x.Year.ToString("yyyy") + ")",
            }).ToList();

            ViewData["CourseId"] = new SelectList(oulist, "Id", "Title"); 
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            
            _context.Attach(TimeTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeTableExists(TimeTable.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            } 

            return RedirectToPage("./Index");
        }

        private bool TimeTableExists(long id)
        {
            return _context.TimeTables.Any(e => e.Id == id);
        }
    }
}
