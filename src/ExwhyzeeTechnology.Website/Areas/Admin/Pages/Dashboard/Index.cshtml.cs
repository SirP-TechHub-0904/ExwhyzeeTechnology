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
        public IList<CareerApplicationCountViewModel> AddmittedCount { get; set; }
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
                ApplicationCount = g.Count()    // Count the number of applications for each career
            })
            .ToListAsync();


            ///
            // Query the database to include Courses, Cohorts, and Participants (with User details)
            var list = _context.Participants
                .Include(x=>x.User)
                .AsQueryable();

           
            // Calculate the total admitted participants
            AllAdmitted = await list.CountAsync();

            // Calculate the number of male admitted participants
            MaleAdmitted = await list.CountAsync(p => p.User.Gender == GenderStatus.Male);

            // Calculate the number of female admitted participants
            FemaleAdmitted = await list.CountAsync(p => p.User.Gender == GenderStatus.Male);

            // Group admitted participants by Cohort and Course and count the admissions
            AddmittedCount = await _context.Courses
                .GroupBy(p => new { p.Id, p.Name })
                .Select(g => new CareerApplicationCountViewModel
                {
                    CareerName = g.Key.Name,            // Grouped by Course Name 
                    ApplicationCount = g.Count()        // Count the number of admitted participants per cohort
                })
                .ToListAsync();
        }
    }
    public class CareerApplicationCountViewModel
    {
        public string CareerName { get; set; }
        public int ApplicationCount { get; set; }
    }
}
