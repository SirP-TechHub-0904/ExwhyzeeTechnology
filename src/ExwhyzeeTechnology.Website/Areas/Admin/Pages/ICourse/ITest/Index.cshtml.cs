using ExwhyzeeTechnology.Application.Dtos;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Domain.Models.Data;
using ExwhyzeeTechnology.Persistence.EF.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static ExwhyzeeTechnology.Domain.Enum.Enum;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.ICourse.ITest
{
     [Microsoft.AspNetCore.Authorization.Authorize]

    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(DashboardDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<UserTestListDto> UserTest { get; set; }
        public Cohort Cohort { get; private set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            // File: UserTestProcessing.cs

            var userTests = await _context.UserTests
                .Include(x => x.User)
                .Include(x => x.TrainingTest)
                .ThenInclude(tt => tt.TrainingTestOptions) // Ensure options are eagerly loaded
                .Where(x => x.CohortId == id && x.Submitted)
                .ToListAsync();

            var groupedUserTests = userTests.GroupBy(x => x.UserId);

            var userTestListDtos = new List<UserTestListDto>();

            foreach (var group in groupedUserTests)
            {
                var userId = group.Key;
                var user = group.First().User;

                // Calculate pre-test score
                var (preTestScore, preTestTaken) = CalculateTestScore(
                    group,
                    TrainingTestType.PreTest
                );

                // Calculate post-test score
                var (postTestScore, postTestTaken) = CalculateTestScore(
                    group,
                    TrainingTestType.PostTest
                );

                userTestListDtos.Add(new UserTestListDto
                {
                    UserId = userId,
                    User = user,
                    PreTestScore = preTestScore,
                    PostTestScore = postTestScore,
                    PreTestTaken = preTestTaken,
                    PostTestTaken = postTestTaken
                });
            }

            // Utility method to calculate scores
            (string, bool) CalculateTestScore(
                IEnumerable<UserTest> userTests,
                TrainingTestType testType)
            {
                var tests = userTests.Where(x => x.TrainingTest.TrainingTestType == testType).ToList();
                if (tests.Count == 0)
                {
                    return ("", false);
                }

                int correctAnswers = tests.Count(x =>
                {
                    var correctOption = x.TrainingTest.TrainingTestOptions.FirstOrDefault(opt => opt.CorrectAnswer);
                    return correctOption != null && x.Answer == correctOption.Option;
                });

                double score = (double)correctAnswers / tests.Count * 100;
                return ($"{score:F0}%", true);
            }


            UserTest = userTestListDtos;

            Cohort = await _context.Cohorts.Include(x=>x.Course).FirstOrDefaultAsync(x=>x.Id == id);
            return Page();
        }
    }

}
