using ExwhyzeeTechnology.Domain.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.UserManagement
{
    public class ParticipantsModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public ParticipantsModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IList<Participant> Participant { get; set; }
        public Cohort Cohort { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
           
            Participant = await _context.Participants
                .Include(p => p.Cohort).ThenInclude(x=>x.Course)
                .Include(p => p.User).ToListAsync();
          
            return Page();
        }
    }
}
