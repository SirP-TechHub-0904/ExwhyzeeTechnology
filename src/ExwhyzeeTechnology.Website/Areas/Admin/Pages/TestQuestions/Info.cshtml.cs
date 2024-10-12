using ExwhyzeeTechnology.Domain.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.TestQuestions
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class InfoModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public InfoModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public TrainingTest TrainingTest { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            //GetByIdAndCountTrainingQuery Command = new GetByIdAndCountTrainingQuery(id);
            //Training = await _mediator.Send(Command);
            return Page();
        }
    }
}
