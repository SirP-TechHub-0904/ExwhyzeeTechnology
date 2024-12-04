using DocumentFormat.OpenXml.Office2010.Excel;
using ExwhyzeeTechnology.Application.Dtos;
using ExwhyzeeTechnology.Application.Dtos.Dashboarrd;
using ExwhyzeeTechnology.Application.Services;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Domain.Models.Data;
using ExwhyzeeTechnology.Website.Areas.Admin.Pages.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static ExwhyzeeTechnology.Domain.Enum.Enum;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.Dashboard
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Admin,Manager,Editor,Administrator")]

    public class IndexModel : PageModel
    {
        private readonly UserManager<Profile> _userManager;

        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<TrainingApplicationForm> TrainingApplicationForm { get; set; }
        // Property to hold the grouped result
        public IList<CareerApplicationCountViewModel> CareerApplicationCounts { get; set; }
        // List of background color classes
        public List<string> Colors { get; set; } = new List<string>
    {
        "bg-danger", "bg-primary", "bg-warning", "bg-info",
        "bg-secondary", "bg-dark", "bg-light", "bg-muted", "bg-success"
    };
        public int AllApplication {  get; set; }
        public int AllMale {  get; set; }
        public int AllFemale {  get; set; }

        public int AllAdmitted { get; set; }
        public int MaleAdmitted { get; set; }
        public int FemaleAdmitted { get; set; }
        public IList<CareerApplicationCountViewModel> AdmittedCount { get; set; }
        public async Task OnGetAsync()
        {
           var TrainingApplicationForm = _context.TrainingApplicationForms.Include(x=>x.CareerTrainingJobRole)
              .AsQueryable();

            AllApplication = TrainingApplicationForm.Count();
            AllMale = TrainingApplicationForm.Where(x=>x.Profile.Gender == Domain.Enum.Enum.GenderStatus.Male).Count();
            AllFemale = TrainingApplicationForm.Where(x=>x.Profile.Gender == Domain.Enum.Enum.GenderStatus.Female).Count();

            CareerApplicationCounts = await _context.TrainingApplicationForms
            .Include(x => x.CareerTrainingJobRole) // Include the CareerTrainingJobRole for career names
            .GroupBy(x => new { x.CareerTrainingJobRoleId, x.CareerTrainingJobRole.Title })
            .Select(g => new CareerApplicationCountViewModel
            {
                CareerName = g.Key.Title ?? "NO CHOICE",  // Grouped by Career Name
                ApplicationCount = g.Count(),    // Count the number of applications for each career
                MaleCount = g.Count(x => x.Profile.Gender == Domain.Enum.Enum.GenderStatus.Male),
                FemaleCount = g.Count(x => x.Profile.Gender == Domain.Enum.Enum.GenderStatus.Female),
                NoGenderCount = g.Count(x => x.Profile.Gender == Domain.Enum.Enum.GenderStatus.None)

            })
            .ToListAsync();


            ///
            // Query the database to include Courses, Cohorts, and Participants (with User details)
            var list = _context.Participants
                .Include(x => x.User)
                .Include(x => x.Cohort)
                .ThenInclude(c => c.Course)  // Ensure we load Course information through Cohort
                .AsQueryable();

            // Calculate the total admitted participants
            AllAdmitted = await list.CountAsync();

            // Calculate the number of male admitted participants
            MaleAdmitted = await list.CountAsync(p => p.User.Gender == GenderStatus.Male);

            // Calculate the number of female admitted participants
            FemaleAdmitted = await list.CountAsync(p => p.User.Gender == GenderStatus.Female);

            // Group admitted participants by Course and Cohort and count the admissions
            AdmittedCount = await _context.Cohorts
                .Include(c => c.Course)
                .SelectMany(c => c.Participants)  // Access participants for each cohort
                .GroupBy(p => new { p.Cohort.Course.Name, p.Cohort.CohortNumber })  // Group by Course Name and Cohort Number
                .Select(g => new CareerApplicationCountViewModel
                {
                    CareerName = g.Key.Name + " - Cohort " + g.Key.CohortNumber,  // Display Course Name with Cohort Number
                    ApplicationCount = g.Count(),  // Count the number of admitted participants per cohort
                    MaleCount = g.Count(p => p.User.Gender == GenderStatus.Male),
                    FemaleCount = g.Count(p => p.User.Gender == GenderStatus.Female),
                    NoGenderCount = g.Count(p => p.User.Gender == GenderStatus.None),
                })
                .ToListAsync();

        }
    }
    public class CareerApplicationCountViewModel
    {
        public string CareerName { get; set; }
        public int ApplicationCount { get; set; }
        public int MaleCount { get; set; }  // Male count
        public int FemaleCount { get; set; }  // Female count
        public int NoGenderCount { get; set; }  // Female count
    }
}
