using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using ExwhyzeeTechnology.Domain.Models;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.IMeetingAttendance
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Secretary")]

    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IList<MeetingAttendance> MeetingAttendance { get;set; }

        public async Task OnGetAsync()
        {
            MeetingAttendance = await _context.MeetingAttendances
                .Include(m => m.Meeting)
                .Include(m => m.User).ToListAsync();
        }
    }
}
