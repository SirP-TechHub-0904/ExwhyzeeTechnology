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

    public class StaffTrainingSheduleModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public StaffTrainingSheduleModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IList<TrainingSchedule> TrainingSchedule { get;set; }

        public async Task OnGetAsync()
        {
            TrainingSchedule = await _context.TrainingSchedules.ToListAsync();
        }
    }
}
