using ExwhyzeeTechnology.Domain.Models.Data;
using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ExwhyzeeTechnology.Persistence.EF.SQL;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.ICourse.IEvaluation
{
     [Microsoft.AspNetCore.Authorization.Authorize]

    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(DashboardDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<DialyParticipantEvaluation> DialyParticipantEvaluation { get; set; }
        public Cohort Cohort { get; private set; }
        public DialyActivity DialyActivity { get; set; }

        public async Task<IActionResult> OnGetAsync(long id, long aid)
        {
            if (id < 0)
            {
                return NotFound();
            }


             DialyActivity = await _context.DialyActivities.FirstOrDefaultAsync(x=>x.Id == aid);

            DialyParticipantEvaluation = await _context.DialyParticipantEvaluations
                .Include(x => x.Participant)
               .Include(x => x.DialyEvaluationQuestion)
               .Include(x => x.DialyActivity)
               .Where(x => x.DialyActivityId == aid)
               .GroupBy(x => x.ParticipantId)
       .Select(g => g.First())
               .ToListAsync();


            DialyParticipantEvaluation = DialyParticipantEvaluation.Where(x => x.DialyActivity.Date.Date == DialyActivity.Date.Date).ToList();



            Cohort = await _context.Cohorts
                .Include(x => x.Course).FirstOrDefaultAsync(x => x.Id == id);
            return Page();
        }
    }

}
