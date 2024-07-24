using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using ExwhyzeeTechnology.Domain.Models;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.IFunds.CompanyFunds
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Fund")]

    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IList<CompanyFund> CompanyFund { get;set; }

        public async Task OnGetAsync()
        {
            CompanyFund = await _context.CompanyFunds
                .Include(c => c.FundSource).ToListAsync();
        }
    }
}
