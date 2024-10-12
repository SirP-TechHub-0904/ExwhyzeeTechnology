using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Domain.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.TimeTablePage
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Admin,Editor")]

    public class DetailsModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public DetailsModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [BindProperty]
        public TimeTable TimeTable { get; set; }

        [BindProperty]
        public long ModeratorId { get; set; }


        [BindProperty]
        public string? NewUserId { get; set; }

        [BindProperty]
        public string? Fullname { get; set; }

        [BindProperty]
        public string? Position { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TimeTable = await _context.TimeTables
                .Include(t => t.Cohort) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (TimeTable == null)
            {
                return NotFound();
            }
            

            return Page();
        }

 
    }
}
