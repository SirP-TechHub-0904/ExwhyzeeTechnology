using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using ExwhyzeeTechnology.Domain.Models;

namespace ExwhyzeeTechnology.Web.Areas.Staff.Pages.Dashboard
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class MeetingInfoModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public MeetingInfoModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public Meeting Meeting { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Meeting = await _context.Meetings.FirstOrDefaultAsync(m => m.Id == id);

            if (Meeting == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
