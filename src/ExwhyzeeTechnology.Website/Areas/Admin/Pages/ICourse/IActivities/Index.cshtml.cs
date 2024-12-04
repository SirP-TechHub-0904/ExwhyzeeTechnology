using ExwhyzeeTechnology.Domain.Models.Data;
using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.ICourse.IActivities
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, Persistence.EF.SQL.DashboardDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IEnumerable<DialyActivity> Datas { get; private set; }
        public Cohort Cohort { get; private set; }
        [BindProperty]
        public DialyActivity DialyActivity { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            Datas = await _context.DialyActivities
              .Include(x => x.CohortAttendance)
              .Include(x => x.Cohort)
              .Where(x => x.CohortId == id).ToListAsync();

            Cohort = await _context.Cohorts
                .Include(x => x.Course).FirstOrDefaultAsync(x => x.Id == id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _context.DialyActivities.AddAsync(DialyActivity);
                await _context.SaveChangesAsync();

                TempData["success"] = "Success";
                return RedirectToPage("./Index", new { id = DialyActivity.CohortId });
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error";
                return RedirectToPage("./Index", new { id = DialyActivity.CohortId });

            }
        }

    }

}
