using ExwhyzeeTechnology.Domain.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Http;
using static ExwhyzeeTechnology.Domain.Enum.Enum;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.UserManagement
{
    public class YearbookModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly HttpClient _httpClient;
        public YearbookModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }


        public List<KeyValuePair<long, string>> GroupedCourses { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var participants = await _context.Participants
           .Include(p => p.Cohort).ThenInclude(c => c.Course)
           .Include(p => p.User)
           .ToListAsync();

            // Group by Course ID and Course Name, then select the key-value pairs
            GroupedCourses = participants
                .GroupBy(p => new { p.Cohort.Course.Id, p.Cohort.Course.Name })
                .Select(g => new KeyValuePair<long, string>(g.Key.Id, g.Key.Name))
                .ToList();
            return Page();
        }
         
    }
     
}