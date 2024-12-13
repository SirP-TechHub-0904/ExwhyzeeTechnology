using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExwhyzeeTechnology.Domain.Models.Data;
using ExwhyzeeTechnology.Persistence.EF.SQL;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.ICourse.IAssignment
{
    public class DetailsModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public DetailsModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public Assignment Assignment { get; set; }
        public Cohort Cohort { get; private set; }
        public DialyActivity DialyActivity { get; set; }
        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Assignment = await _context.Assignments
                .Include(x=>x.UserAssignments).ThenInclude(x=>x.Participant).ThenInclude(x=>x.User)
                .Include(a => a.DialyActivity).FirstOrDefaultAsync(m => m.Id == id);

            if (Assignment == null)
            {
                return NotFound();
            }
            DialyActivity = await _context.DialyActivities.FirstOrDefaultAsync(x => x.Id == Assignment.DialyActivityId);


            Cohort = await _context.Cohorts
                .Include(x => x.Course).FirstOrDefaultAsync(x => x.Id == DialyActivity.CohortId);


            return Page();
        }
    }
}
