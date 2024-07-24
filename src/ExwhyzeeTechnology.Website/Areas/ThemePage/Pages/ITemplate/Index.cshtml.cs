using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Areas.ThemePage.Pages.ITemplate
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin")]

    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IList<TemplateType> TemplateType { get;set; }

        public async Task OnGetAsync()
        {
            var setting = await _context.SuperSettings.FirstOrDefaultAsync();

            TemplateType = await _context.TemplateTypes.ToListAsync();
        }
    }
}
