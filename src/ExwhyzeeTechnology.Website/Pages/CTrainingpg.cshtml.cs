using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using ExwhyzeeTechnology.Application.Dtos;
using ExwhyzeeTechnology.Application.Dtos.AwsDtos;
using ExwhyzeeTechnology.Application.Repository.Services;
using ExwhyzeeTechnology.Application.Services.AWS;
using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ExwhyzeeTechnology.Website.Pages
{
    public class CTrainingpgModel : PageModel
    {
        private readonly ISettingsService _settingsService;

        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        private readonly UserManager<Profile> _userManager;

        public CTrainingpgModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context, ISettingsService settingsService, IConfiguration config, IStorageService storageService, UserManager<Profile> userManager)
        {
            _context = context;
            _config = config;
            _storageService = storageService;
            _settingsService = settingsService;
            _userManager = userManager;
        }

        //stage = "xmbt1xmbt"
        [BindProperty]
        public IFormFile? imagefile { get; set; }

        [BindProperty]
        public IFormFile? imagefile2 { get; set; }
        [BindProperty]
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

        public async Task<IActionResult> OnGet(long id, string apid, string process, string stage)
        {
            Setting = await _context.Settings.FirstOrDefaultAsync();
            var httpContext = HttpContext;
            VerificationWebDto setting = await _settingsService.ValidateWeb(httpContext);
            if (setting.SettingFound == false)
            {
                return RedirectToPage(setting.Path, new { area = setting.Area });
            }
            if (setting.Portfolio == true)
            {
                return RedirectToPage(setting.PortfolioPath);

            }

            SuperSetting = setting.SuperSetting;
            TrainingApplicationForm = await _context.TrainingApplicationForms
                .Include(x=>x.Profile)
                .FirstOrDefaultAsync(x => x.Id == id);
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
            Stage = stage;

            ViewData["JobRoleId"] = new SelectList(_context.CareerTrainingJobRoles.Where(x => x.Disable == false), "Id", "Title");
            TermPage = await _context.WebPages.FirstOrDefaultAsync(x => x.Publish == true && x.ShowInFooter == true && x.SecurityPage == true && x.EnableDirectUrl == false && x.Title.ToLower().Contains("term"));

            return Page();
        }

        [BindProperty]
        public CareerFile CareerFile { get; set; }

        [BindProperty]
        public string? State { get; set; }


        [BindProperty]
        public string? LGA { get; set; }


        [BindProperty]
        public string? Address { get; set; }

        [BindProperty]
        public List<long> SelectedJobRoleIds { get; set; } = new List<long>();

        public async Task<IActionResult> OnPostBioAsync()
        {
            var getform = await _context.TrainingApplicationForms.FirstOrDefaultAsync(x => x.Id == TrainingApplicationForm.Id);
            if (getform == null)
            {
                TempData["error"] = "Unable to validate data";
                return RedirectToPage("./TrainingInitial");
            }
            var user = await _userManager.FindByIdAsync(getform.ProfileId);
            if(user == null)
            {
                TempData["error"] = "Unable to validate data";
                return RedirectToPage("./TrainingInitial");
            }

            user.State = State;
            user.PermanentLga = LGA;
            user.Address = Address;
             await _userManager.UpdateAsync(user);
            TempData["success"] = "Successful Scroll Down to the Next Form";

            return RedirectToPage("./CTrainingpg", new { id = getform.Id, apid = Guid.NewGuid(), stage = "xmbt1xmbt", process = Guid.NewGuid() });
        }

        public async Task<IActionResult> OnPostCourseAsync()
        {
            var getform = await _context.TrainingApplicationForms.FirstOrDefaultAsync(x => x.Id == TrainingApplicationForm.Id);
            if (getform == null)
            {
                TempData["error"] = "Unable to validate data";
                return RedirectToPage("./TrainingInitial");
            }
            getform.CareerTrainingJobRoleId = JobRoleId;


            _context.Attach(getform).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            TempData["success"] = "Successful Scroll Down to the Next Form";

            return RedirectToPage("./CTrainingpg", new { id = getform.Id, apid = Guid.NewGuid(), stage = "xmbt2xmbt", process = Guid.NewGuid() });
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostEducationalAsync()
        {
            var getform = await _context.TrainingApplicationForms.FirstOrDefaultAsync(x => x.Id == TrainingApplicationForm.Id);
            if (getform == null)
            {
                TempData["error"] = "Unable to validate data";
                return RedirectToPage("./TrainingInitial");
            }
            getform.HighestLevelOfEducation = TrainingApplicationForm.HighestLevelOfEducation;
            getform.FieldOfStudy = TrainingApplicationForm.FieldOfStudy;
            getform.CurrentEducationalStatus = TrainingApplicationForm.CurrentEducationalStatus;

            _context.Attach(getform).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            TempData["success"] = "Successful Scroll Down to the Next Form";

            return RedirectToPage("./CTrainingpg", new { id = getform.Id, apid = Guid.NewGuid(), stage = "xmbt3xmbt", process = Guid.NewGuid() });
        }

        public async Task<IActionResult> OnPostDigitalAsync()
        {
            var getform = await _context.TrainingApplicationForms.FirstOrDefaultAsync(x => x.Id == TrainingApplicationForm.Id);
            if (getform == null)
            {
                TempData["error"] = "Unable to validate data";
                return RedirectToPage("./TrainingInitial");
            }
            getform.ComputerLiteracy = TrainingApplicationForm.ComputerLiteracy;
            getform.WebDevelopment = TrainingApplicationForm.WebDevelopment;
            getform.GraphicDesign = TrainingApplicationForm.GraphicDesign;
            getform.DigitalMarketing = TrainingApplicationForm.DigitalMarketing;
            getform.Programming = TrainingApplicationForm.Programming;
            getform.DataAnalysis = TrainingApplicationForm.DataAnalysis;
            getform.OtherRelevant = TrainingApplicationForm.OtherRelevant;
            getform.ExperienceWithDigitalTools = TrainingApplicationForm.ExperienceWithDigitalTools;

            _context.Attach(getform).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            TempData["success"] = "Successful Scroll Down to the Next Form";

            return RedirectToPage("./CTrainingpg", new { id = getform.Id, apid = Guid.NewGuid(), stage = "xmbt4xmbt", process = Guid.NewGuid() });
        }
        public async Task<IActionResult> OnPostMotivationAsync()
        {
            var getform = await _context.TrainingApplicationForms.FirstOrDefaultAsync(x => x.Id == TrainingApplicationForm.Id);
            if (getform == null)
            {
                TempData["error"] = "Unable to validate data";
                return RedirectToPage("./TrainingInitial");
            }
            getform.WhyAreYouInterestedInThisDigitalSkill = TrainingApplicationForm.WhyAreYouInterestedInThisDigitalSkill;
            getform.HowDoYouBelieveThisProgramCanHelpYouAchieveYourPersonalGoal = TrainingApplicationForm.HowDoYouBelieveThisProgramCanHelpYouAchieveYourPersonalGoal;
            getform.WhatDoYouHopeToGainFromParticipatingInThisProgram = TrainingApplicationForm.WhatDoYouHopeToGainFromParticipatingInThisProgram;

            _context.Attach(getform).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            TempData["success"] = "Successful Scroll Down to the Next Form";

            return RedirectToPage("./CTrainingpg", new { id = getform.Id, apid = Guid.NewGuid(), stage = "xmbt5xmbt", process = Guid.NewGuid() });
        }
        public async Task<IActionResult> OnPostAdditionalAsync()
        {
            var getform = await _context.TrainingApplicationForms.FirstOrDefaultAsync(x => x.Id == TrainingApplicationForm.Id);
            if (getform == null)
            {
                TempData["error"] = "Unable to validate data";
                return RedirectToPage("./TrainingInitial");
            }
            getform.DoYouHaveAnySpecialNeedsOrAccommodationsWeShouldBeAwareOf = TrainingApplicationForm.DoYouHaveAnySpecialNeedsOrAccommodationsWeShouldBeAwareOf;
            getform.HowDidYouHearAboutThisProgram = TrainingApplicationForm.HowDidYouHearAboutThisProgram;
            getform.AreYouCurrentlyEmployedOrSelfEmployed = TrainingApplicationForm.AreYouCurrentlyEmployedOrSelfEmployed;
            getform.EmploymentDetails = TrainingApplicationForm.EmploymentDetails;
            getform.DoYouHaveAnyOtherRelevantInformationYouWouldLikeToShare = TrainingApplicationForm.DoYouHaveAnyOtherRelevantInformationYouWouldLikeToShare;

            _context.Attach(getform).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            TempData["success"] = "Successful Scroll Down to the Next Form";

            return RedirectToPage("./CTrainingpg", new { id = getform.Id, apid = Guid.NewGuid(), stage = "xmbt6xmbt", process = Guid.NewGuid() });
        }
        public async Task<IActionResult> OnPostCloseAsync()
        {
            var getform = await _context.TrainingApplicationForms.FirstOrDefaultAsync(x => x.Id == TrainingApplicationForm.Id);
            if (getform == null)
            {
                TempData["error"] = "Unable to validate data";
                return RedirectToPage("./TrainingInitial");
            }
            var user = await _userManager.FindByIdAsync(getform.ProfileId);
            if (user == null)
            {
                TempData["error"] = "Unable to validate data";
                return RedirectToPage("./TrainingInitial");
            }

            //image
            if (imagefile != null)
            {
                try
                {
                    // Process file
                    await using var memoryStream = new MemoryStream();
                    await imagefile.CopyToAsync(memoryStream);

                    var fileExt = Path.GetExtension(imagefile.FileName);
                    var docName = $"{Guid.NewGuid()}{fileExt}";
                    // call server

                    var s3Obj = new Application.Dtos.AwsDtos.S3Object()
                    {
                        BucketName = await _storageService.BucketName(),
                        InputStream = memoryStream,
                        Name = docName
                    };

                    var cred = new AwsCredentials()
                    {
                        AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                        SecretKey = _config["AwsConfiguration:AWSSecretKey"]
                    };

                    var xresult = await _storageService.UploadFileReturnUrlAsync(s3Obj, cred, user.PassportFilePathKey);
                    // 
                    if (xresult.Message.Contains("200"))
                    {
                        user.PassportFilePathUrl = xresult.Url;
                        user.PassportFilePathKey = xresult.Key;
                    }
                    else
                    {
                        TempData["error"] = "unable to upload image";
                        //return Page();
                    }
                }
                catch (Exception c)
                {

                }
            }

            //image
            if (imagefile2 != null)
            {
                try
                {
                    // Process file
                    await using var memoryStream = new MemoryStream();
                    await imagefile2.CopyToAsync(memoryStream);

                    var fileExt = Path.GetExtension(imagefile2.FileName);
                    var docName = $"{Guid.NewGuid()}{fileExt}";
                    // call server

                    var s3Obj = new Application.Dtos.AwsDtos.S3Object()
                    {
                        BucketName = await _storageService.BucketName(),
                        InputStream = memoryStream,
                        Name = docName
                    };

                    var cred = new AwsCredentials()
                    {
                        AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                        SecretKey = _config["AwsConfiguration:AWSSecretKey"]
                    };

                    var xresult = await _storageService.UploadFileReturnUrlAsync(s3Obj, cred, user.NinKey);
                    // 
                    if (xresult.Message.Contains("200"))
                    {
                        user.NinUrl = xresult.Url;
                        user.NinKey = xresult.Key;
                    }
                    else
                    {
                        TempData["error"] = "unable to upload image";
                        //return Page();
                    }
                }
                catch (Exception c)
                {

                }
            }


            await _userManager.UpdateAsync(user);
            getform.AcceptTerms = true;
            
            _context.Attach(getform).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            TempData["success"] = "Successful";

            return RedirectToPage("./CTrainingpg", new { id = getform.Id, apid = Guid.NewGuid(), stage = "xmbtxmbt", process = Guid.NewGuid() });
        }
    }
}