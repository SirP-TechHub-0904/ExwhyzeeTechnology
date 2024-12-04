using ExwhyzeeTechnology.Domain.Models.Data;
using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ExwhyzeeTechnology.Persistence.EF.SQL;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.ICourse.IEvaluation
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class EvaluationQuestionModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public EvaluationQuestionModel(DashboardDbContext context)
        {
            _context = context;
        }

        public List<DialyEvaluationQuestion> ListDialyEvaluationQuestion { get; set; }
        public Cohort Cohort { get; private set; }
        public DialyActivity DialyActivity { get; set; }

        [BindProperty]
        public DialyEvaluationQuestion DialyEvaluationQuestion { get; set; }
        public List<SelectListItem> EvaluationQuestionDropdownDto { get; set; }
        public List<SelectListItem> ModuleTopicDropdownDto { get; set; }


        [BindProperty]
        public string? GeneralQuestion { get; set; }
        [BindProperty]
        public string? ModuleTopicQuestion { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }


            DialyActivity = await _context.DialyActivities.FindAsync(id);

            Cohort = await _context.Cohorts
               .Include(x => x.Course)
               .FirstOrDefaultAsync(x => x.Id == id);



            ListDialyEvaluationQuestion = await _context.DialyEvaluationQuestions.Where(x => x.DialyActivityId == DialyActivity.Id).ToListAsync();

            //
            var Listquestiondropdown = await _context.EvaluationQuestions
               .Include(x => x.EvaluationQuestionCategory).ToListAsync();

            EvaluationQuestionDropdownDto = Listquestiondropdown
            .Select(eq => new SelectListItem
            {
                Value = eq.Question.ToString(),
                Text = eq.Question
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {


            DialyEvaluationQuestion.Question = GeneralQuestion;

            try
            {
                await _context.DialyEvaluationQuestions.AddAsync(DialyEvaluationQuestion);
                await _context.SaveChangesAsync();
                TempData["success"] = "Success";
                return RedirectToPage("./EvaluationQuestion", new { id = DialyEvaluationQuestion.DialyActivityId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {


            try
            {
                try
                {
                    _context.DialyEvaluationQuestions.Remove(DialyEvaluationQuestion);
                await _context.SaveChangesAsync();

                    TempData["success"] = "Success";

                }
                catch (Exception ex)
                {
                    TempData["error"] = "Cannot be delete. Evalution already answered by participants";
                }
                return RedirectToPage("./EvaluationQuestion", new { id = DialyEvaluationQuestion.DialyActivityId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }

}
