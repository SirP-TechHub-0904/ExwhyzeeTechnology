using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Persistence.EF.SQL;

namespace ExwhyzeeTechnology.Website.Areas.Management.Pages.IConfigSettings
{
    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }
        public DataConfig DataConfig { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            
            DataConfig = await _context.DataConfigs.FirstOrDefaultAsync();

            if (DataConfig == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
