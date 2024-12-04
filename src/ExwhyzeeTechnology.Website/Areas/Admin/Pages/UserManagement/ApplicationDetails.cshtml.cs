using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Persistence.EF.SQL;
using ExwhyzeeTechnology.Application.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExwhyzeeTechnology.Domain.Models.Data;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.UserManagement
{
    public class ApplicationDetailsModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public ApplicationDetailsModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public TrainingApplicationForm TrainingApplicationForm { get; set; }
        public SuperSetting SuperSetting { get; set; }
        public Setting Setting { get; set; }
        [BindProperty]
        public long JobRoleId { get; set; }
        [BindProperty]
        public string Stage { get; set; }


        public bool F1 { get; set; }
        public bool F2 { get; set; }
        public bool F3 { get; set; }
        public bool F4 { get; set; }
        public bool F5 { get; set; }
        public bool F6 { get; set; }
        public bool F9 { get; set; }

        public WebPage TermPage { get; set; }

        [BindProperty]
        public Profile Profile { get; set; }
        public Profile UserDatas { get; set; }
        public IList<AdditionalProfile> AdditionalProfile { get; set; }
        public IList<ProfileCategory> ProfileCategories { get; set; }

        [BindProperty]
        public long SelectedCourseId { get; set; }

        [BindProperty]
        public long SelectedCohortId { get; set; }
        //[BindProperty]
        //public SelectList Courses { get; set; }
        //[BindProperty]
        //public SelectList Cohorts { get; set; }
        [BindProperty]
        public string OTP { get; set; }
        [BindProperty]
        public string UserId { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {

            Profile = await _userManager.FindByIdAsync(id);

            if (Profile == null)
            {
                return NotFound();
            }
            TrainingApplicationForm = await _context.TrainingApplicationForms
                .Include(x => x.Profile)
                .Include(x => x.CareerTrainingJobRole)
                .FirstOrDefaultAsync(x => x.ProfileId == Profile.Id);
            if (TrainingApplicationForm == null)
            {
                TempData["error"] = "Unable to validate data";
                return RedirectToPage("./TrainingInitial");
            }
            if (TrainingApplicationForm.CareerTrainingJobRoleId != null)
            {
                F1 = true;
            }
            if (TrainingApplicationForm.HighestLevelOfEducation != null)
            {
                F2 = true;
            }
            if (TrainingApplicationForm.ComputerLiteracy != null)
            {
                F3 = true;
            }
            if (TrainingApplicationForm.WhyAreYouInterestedInThisDigitalSkill != null)
            {
                F4 = true;
            }
            if (TrainingApplicationForm.DoYouHaveAnySpecialNeedsOrAccommodationsWeShouldBeAwareOf != null)
            {
                F5 = true;
            }
            if (TrainingApplicationForm.AcceptTerms == true)
            {
                F6 = true;
            }
            if (TrainingApplicationForm.Profile.State != null)
            {
                F9 = true;
            }
            UserDatas = Profile;
            AdditionalProfile = await _context.AdditionalProfiles.Where(x => x.ProfileId == UserDatas.Id).ToListAsync();
            ProfileCategories = await _context.ProfileCategories
                .Include(x => x.ProfileCategoryLists).Where(x => x.ProfileId == UserDatas.Id).ToListAsync();
            //var courseList = await _context.Courses.ToListAsync();
            //Courses = new SelectList(courseList, "Id", "Name");

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");

            return Page();
        }

        public async Task<JsonResult> OnGetCohortsByCourseIdAsync(long courseId)
        {
            var cohorts = await _context.Cohorts
                .Where(c => c.CourseId == courseId)
                .Select(c => new { c.Id, c.CohortNumber })
                .ToListAsync();
            return new JsonResult(cohorts);
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            TrainingApplicationForm = await _context.TrainingApplicationForms
                    .Include(x => x.Profile)
                    .Include(x => x.CareerTrainingJobRole)
                    .FirstOrDefaultAsync(x => x.ProfileId == Profile.Id);

             
            // Strip spaces and hypens
            //var verificationCode = OTP.Replace(" ", string.Empty).Replace("-", string.Empty);

            //var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
            //    user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

            //if (!is2faTokenValid || TrainingApplicationForm == null)
            //{
            //    ModelState.AddModelError("Input.Code", "Verification code is invalid.");
            //    Profile = await _userManager.FindByIdAsync(UserId);

            //    if (Profile == null)
            //    {
            //        return NotFound();
            //    }
                
            //    if (TrainingApplicationForm == null)
            //    {
            //        TempData["error"] = "Unable to validate data";
            //        return RedirectToPage("./Training");
            //    }
            //    if (TrainingApplicationForm.CareerTrainingJobRoleId != null)
            //    {
            //        F1 = true;
            //    }
            //    if (TrainingApplicationForm.HighestLevelOfEducation != null)
            //    {
            //        F2 = true;
            //    }
            //    if (TrainingApplicationForm.ComputerLiteracy != null)
            //    {
            //        F3 = true;
            //    }
            //    if (TrainingApplicationForm.WhyAreYouInterestedInThisDigitalSkill != null)
            //    {
            //        F4 = true;
            //    }
            //    if (TrainingApplicationForm.DoYouHaveAnySpecialNeedsOrAccommodationsWeShouldBeAwareOf != null)
            //    {
            //        F5 = true;
            //    }
            //    if (TrainingApplicationForm.AcceptTerms == true)
            //    {
            //        F6 = true;
            //    }
            //    if (TrainingApplicationForm.Profile.State != null)
            //    {
            //        F9 = true;
            //    }
            //    UserDatas = Profile;
            //    AdditionalProfile = await _context.AdditionalProfiles.Where(x => x.ProfileId == UserDatas.Id).ToListAsync();
            //    ProfileCategories = await _context.ProfileCategories
            //        .Include(x => x.ProfileCategoryLists).Where(x => x.ProfileId == UserDatas.Id).ToListAsync();
            //    ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");

            //    //Courses = new SelectList(await _context.Courses.ToListAsync(), "Id", "Name");
            //    return Page();
            //}

            // Check if the participant already exists
            var checkadmission = await _context.Participants.FirstOrDefaultAsync(x => x.UserId == UserId);

            if (checkadmission == null)
            {
                // Fetch the cohort and related course info
                var cohort = await _context.Cohorts
                                           .Include(c => c.Course) // Assuming Cohort has a Course navigation property
                                           .FirstOrDefaultAsync(c => c.Id == SelectedCohortId);

                if (cohort == null)
                {
                    // Handle case where cohort is not found
                    return NotFound("Selected cohort not found.");
                }

                // Get the current count of participants in this cohort to generate the registration number
                int cohortSerialNumber = await _context.Participants
                                                       .Where(p => p.CohortId == SelectedCohortId)
                                                       .CountAsync() + 1; // Increment to get the next serial number

                // Create new participant
                Participant newparticipant = new Participant
                {
                    UserId = UserId,
                    CohortId = SelectedCohortId,
                    ParticipantStatus = Domain.Enum.Enum.ParticipantStatus.Active
                };

                // Generate registration number in format: course/cohort/01
                newparticipant.IdNumber = $"{cohort.Course.Abbreviation}/{cohort.CohortCode}/{cohortSerialNumber:D2}";

                // Add new participant to the database
                _context.Participants.Add(newparticipant);

                TrainingApplicationForm.AdmissionStatus = Domain.Enum.Enum.AdmissionStatus.Admitted;
                _context.Attach(TrainingApplicationForm).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Redirect to a page, passing the new participant ID
                return RedirectToPage("./AdmissionStatus", new { id = newparticipant.Id });
            }

            // If participant already exists, redirect without adding a new one
            return RedirectToPage("./AdmissionStatus", new { id = checkadmission.Id });
        }

    }
}
