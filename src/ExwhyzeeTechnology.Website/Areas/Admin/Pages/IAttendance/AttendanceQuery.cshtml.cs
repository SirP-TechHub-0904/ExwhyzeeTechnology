using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using ExwhyzeeTechnology.Domain.Models;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.IAttendance
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Manager")]

    public class AttendanceQueryModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public AttendanceQueryModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IList<Attendance> Attendance { get;set; }

        public async Task OnGetAsync()
        {
            Attendance = await _context.Attendances
                .Include(a => a.User).ToListAsync();
        }
    }
}
