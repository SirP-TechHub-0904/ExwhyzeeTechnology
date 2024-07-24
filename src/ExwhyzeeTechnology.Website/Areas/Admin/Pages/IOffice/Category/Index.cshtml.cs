using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Persistence.EF.SQL;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.IOffice.Category
{        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Manager,Admin")]

    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IList<OfficeActivityCategory> OfficeActivityCategory { get;set; }

        public async Task OnGetAsync()
        {
            OfficeActivityCategory = await _context.OfficeActivityCategories.ToListAsync();
        }
    }
}
