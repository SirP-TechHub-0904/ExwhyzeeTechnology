using ExwhyzeeTechnology.Application.Dtos;
using ExwhyzeeTechnology.Application.Dtos.AwsDtos;
using ExwhyzeeTechnology.Application.Repository.NotifyRegister;
using ExwhyzeeTechnology.Application.Repository.Services;
using ExwhyzeeTechnology.Application.Services.AWS;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Persistence.EF.SQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Encodings.Web;

namespace ExwhyzeeTechnology.Website.Pages
{
    public class TrainingInitialModel : PageModel
    {
        private readonly ISettingsService _settingsService;
        private readonly RoleManager<IdentityRole> _role;
        private readonly IEmailSendService _email;
        private readonly ISmsSendService _sms;
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public TrainingInitialModel(ISettingsService settingsService, RoleManager<IdentityRole> role, IEmailSendService email, ISmsSendService sms, DashboardDbContext context, UserManager<Profile> userManager)
        {
            _settingsService = settingsService;
            _role = role;
            _email = email;
            _sms = sms;
            _context = context;
            _userManager = userManager;
        }

        public SuperSetting SuperSetting { get; set; }
        public Setting Setting { get; set; }
        [BindProperty]
        public Profile UserDatas { get; set; }

        public List<CareerTrainingJobRole> JobRoles { get; set; }
        public async Task<IActionResult> OnGet()
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
            JobRoles = await _context.CareerTrainingJobRoles.Where(x => x.Disable == false).ToListAsync();

            Random random = new Random();
            FirstNum = random.Next(1, 10);
            SecondNum = random.Next(1, 10);
            return Page();
        }

        [BindProperty]
        public CareerFile CareerFile { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public int FirstNum { get; set; }


        [BindProperty]
        public int SecondNum { get; set; }

        [BindProperty]
        public int TotalNum { get; set; }
        public long ApplyId { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Setting = await _context.Settings.FirstOrDefaultAsync();
            var httpContext = HttpContext;
            VerificationWebDto setting = await _settingsService.ValidateWeb(httpContext);
            Random random = new Random();
           
            if (FirstNum + SecondNum != TotalNum)
            {
                TempData["error"] = "Incorrect CAPTCHA. Please try again.";
               
                if (setting.SettingFound == false)
                {
                    return RedirectToPage(setting.Path, new { area = setting.Area });
                }
                if (setting.Portfolio == true)
                {
                    return RedirectToPage(setting.PortfolioPath);

                }

                SuperSetting = setting.SuperSetting;
                JobRoles = await _context.CareerTrainingJobRoles.Where(x => x.Disable == false).ToListAsync();
                 
                return Page();
            }

          if(UserDatas.FirstName.Contains("http") || UserDatas.FirstName.Contains("/")
            || UserDatas.LastName.Contains("http") || UserDatas.LastName.Contains("/"))
            {
                TempData["error"] = "Invalid Input";

                if (setting.SettingFound == false)
                {
                    return RedirectToPage(setting.Path, new { area = setting.Area });
                }
                if (setting.Portfolio == true)
                {
                    return RedirectToPage(setting.PortfolioPath);

                } 
                SuperSetting = setting.SuperSetting;
                JobRoles = await _context.CareerTrainingJobRoles.Where(x => x.Disable == false).ToListAsync();
                return Page();
            }

            //var xuser = await _userManager.Users.Where(x => x.FirstName.Contains("http") || x.FirstName.Contains("/") ||
            //x.MiddleName.Contains("http") || x.MiddleName.Contains("/")

                //|| x.LastName.Contains("http") || x.LastName.Contains("/")).ToListAsync();


            var user = new Profile
            {
                UserName = UserDatas.Email,
                Email = UserDatas.Email,
                PhoneNumber = UserDatas.PhoneNumber,
                FirstName = UserDatas.FirstName,
                LastName = UserDatas.LastName,
                Gender = UserDatas.Gender,
                DateOfBirth = UserDatas.DateOfBirth, 
                Date = DateTime.UtcNow.AddHours(1),
                Title = UserDatas.Title,
                Role = "TRAINING",
                ResetPassword = false,
                TempPass = "",
                UpdateProfile = true,
                UpdateAwards = true,
                UpdateCertificate = true,
                UpdateEducation = true,
                UpdateExperience = true,
                UpdateInterest = true,
                UpdateLanguage = true,
                UpdateReference = true,
                UpdateSkills = true,
                LockoutEnabled = false,
                UserStatus = Domain.Enum.Enum.UserStatus.Pending,
                PositionOrRank = "",
                NIN = UserDatas.NIN
            };

            user.Id = Guid.NewGuid().ToString();

            var result = await _userManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                //
                IdentityRole Managerf = new IdentityRole("TRAINING");
                var checkManagerf = await _role.FindByNameAsync("TRAINING");
                if (checkManagerf == null)
                {
                    await _role.CreateAsync(Managerf);

                }
                await _userManager.AddToRoleAsync(user, "TRAINING");
                try
                {
                    List<string> category = new List<string> { "EDUCATION", "EXPERIENCE", "DIGITAL SKILLS EXPEREINCE", "SKILLS", "DIGITAL SKILL CERTIFICATES", "DIGITAL SKILLS AREA TO TRAIN ON IGF", "PROFESSIONAL ACHIEVEMENT", "LANGUAGES", "AWARDS", "INTEREST", "REFERENCES" };

                    foreach (string item in category)
                    {
                        ProfileCategory p = new ProfileCategory();
                        p.ProfileId = user.Id;
                        p.Title = item;
                        _context.ProfileCategories.Add(p);
                        await _context.SaveChangesAsync();

                    }
                }
                catch (Exception c)
                {

                }

                try
                {
                    TrainingApplicationForm p = new TrainingApplicationForm();
                    p.ProfileId = user.Id;
                    _context.TrainingApplicationForms.Add(p);
                    await _context.SaveChangesAsync();
                    ApplyId = p.Id;
                }
                catch (Exception e) { }

                //Message newmessage = new Message();
                //newmessage.

                var linkcomplete = Url.Page(
                    "/AuthV2/Account/Login",
                    pageHandler: null,
                    values: new { area = "V2" },
                    protocol: Request.Scheme);
                string datavalue = $"<a href='{HtmlEncoder.Default.Encode(linkcomplete)}'>click here to login</a>.";

                var getusertoupdate = await _userManager.FindByIdAsync(user.Id);
                string messagedetails = $"Your Application has been submitted";



                var send = await _sms.SendSmsWithResult(user.PhoneNumber, "Your Application is under review. \nThanks");

                await _email.SendEmailPostmaster(user.FullnameX, user.Email, "", "", $"EXWHYZEE SCHOLARSHIP TRAINING", messagedetails);

                return RedirectToPage("./CTrainingpg", new { id = ApplyId, apid = Guid.NewGuid(), stage = "xmbtwxmbt", process = Guid.NewGuid() });

            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
              if (setting.SettingFound == false)
            {
                return RedirectToPage(setting.Path, new { area = setting.Area });
            }
            if (setting.Portfolio == true)
            {
                return RedirectToPage(setting.PortfolioPath);

            }

            SuperSetting = setting.SuperSetting;
            JobRoles = await _context.CareerTrainingJobRoles.Where(x => x.Disable == false).ToListAsync();
       
            return Page();
        }
    }

}
