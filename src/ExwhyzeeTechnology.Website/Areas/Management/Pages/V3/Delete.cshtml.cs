﻿using System;
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

    public class DeleteModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public DeleteModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SuperSetting SuperSetting { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SuperSetting = await _context.SuperSettings.FirstOrDefaultAsync(m => m.Id == id);

            if (SuperSetting == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SuperSetting = await _context.SuperSettings.FindAsync(id);

            if (SuperSetting != null)
            {
                _context.SuperSettings.Remove(SuperSetting);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
