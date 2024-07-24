using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Persistence.EF.SQL;

namespace ExwhyzeeTechnology.Website.Areas.Management.Pages.V3
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin")]

    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public SuperSetting SuperSetting { get; set; }
         public DataConfig DataConfig { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
             
            SuperSetting = await _context.SuperSettings.FirstOrDefaultAsync();
                          DataConfig = await _context.DataConfigs.FirstOrDefaultAsync();

            return Page();
        }
    }
}
