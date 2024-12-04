using ExwhyzeeTechnology.Domain.Models.Data;
using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.ICourse.IActivities
{
   
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class ActivityDetailsModel : PageModel
    {
        private readonly ILogger<ActivityDetailsModel> _logger;
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public ActivityDetailsModel(ILogger<ActivityDetailsModel> logger, Persistence.EF.SQL.DashboardDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Cohort Cohort { get; private set; }
        [BindProperty]
        public DialyActivity DialyActivity { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            DialyActivity = await _context.DialyActivities.FindAsync(id);

            Cohort = await _context.Cohorts
                .Include(x => x.Course).FirstOrDefaultAsync(x => x.Id == id);

            return Page();
        }
    }
}
