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
    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IList<Assignment> Assignment { get;set; }
        public Cohort Cohort { get; private set; }
        public DialyActivity DialyActivity { get; set; }
        public async Task OnGetAsync(long id)
        {
            Assignment = await _context.Assignments
                .Include(x => x.UserAssignments)
                .Include(a => a.DialyActivity).Where(x=>x.DialyActivityId == id).ToListAsync();



            Cohort = await _context.Cohorts
                .Include(x => x.Course).FirstOrDefaultAsync(x => x.Id == id);

            DialyActivity = await _context.DialyActivities.FirstOrDefaultAsync(x => x.Id == id);

        }
    }
}
