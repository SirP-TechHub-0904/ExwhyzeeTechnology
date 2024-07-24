using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Persistence.EF.SQL;

namespace ExwhyzeeTechnology.Website.Areas.SMS.Pages.Messages
{
    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IList<SmsMessage> SmsMessage { get; set; }
        public SmsMessageCategory SmsMessageCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id == null)
            {
                return NotFound();
            }
            SmsMessage = await _context.SmsMessages
                .Include(s => s.SmsMessageCategory).Where(x => x.Id == id).ToListAsync();

            SmsMessageCategory = await _context.SmsMessageCategories.FindAsync(id);

            return Page();
        }
    }
}
