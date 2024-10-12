using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExwhyzeeTechnology.Domain.Models.Data;
using ExwhyzeeTechnology.Persistence.EF.SQL;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.ICourse.IParticipant
{
    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IList<Participant> Participant { get;set; }
        public Cohort Cohort { get; set; }


        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Participant = await _context.Participants
                .Include(p => p.Cohort)
                .Include(p => p.User).Where(x=>x.CohortId == id).ToListAsync();
            Cohort = await _context.Cohorts
                .Include(c => c.Course).FirstOrDefaultAsync(m => m.Id == id);

            if (Cohort == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
