using ExwhyzeeTechnology.Domain.Models.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RazorWebUI.Areas.ITrainings.Pages.TestQuestions
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TrainingTest> Datas { get; private set; }

        public async Task OnGetAsync()
        {
            Datas = await _context.TrainingTests.ToListAsync();

        }
    }
}
