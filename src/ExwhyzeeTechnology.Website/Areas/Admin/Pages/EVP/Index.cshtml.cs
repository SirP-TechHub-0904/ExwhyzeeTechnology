using ExwhyzeeTechnology.Domain.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Areas.EVP.Pages.EVP
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Admin")]

    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IList<EvaluationQuestion> EvaluationQuestion { get; set; }

        public async Task OnGetAsync()
        {
            EvaluationQuestion = await _context.EvaluationQuestions.ToListAsync();
        }
    }
}
