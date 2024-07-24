using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Persistence.EF.SQL;
using Microsoft.AspNetCore.Authorization;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.XProjectPage.XP
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IList<XProject> XProject { get;set; }

        public async Task OnGetAsync()
        {
            XProject = await _context.XProjects.ToListAsync();
        }
    }
}
