using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Persistence.EF.SQL;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.XProjectPage.XM
{
    public class DetailsModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public DetailsModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public XProjectMessage XProjectMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            XProjectMessage = await _context.XProjectMessages
                .Include(x => x.Receiver)
                .Include(x => x.Sender)
                .Include(x => x.XProject).FirstOrDefaultAsync(m => m.Id == id);

            if (XProjectMessage == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
