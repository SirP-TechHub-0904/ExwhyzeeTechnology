﻿using System;
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

    public class DetailsModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public DetailsModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public CompanyFund CompanyFund { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CompanyFund = await _context.CompanyFunds
                .Include(c => c.FundSource).FirstOrDefaultAsync(m => m.Id == id);

            if (CompanyFund == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
