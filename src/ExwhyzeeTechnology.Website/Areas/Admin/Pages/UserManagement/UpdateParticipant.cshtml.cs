using ExwhyzeeTechnology.Application.Services.AWS;
using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static ExwhyzeeTechnology.Domain.Enum.Enum;
using System.ComponentModel.DataAnnotations;
using ExwhyzeeTechnology.Application.Dtos.AwsDtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using ExwhyzeeTechnology.Domain.Models.Data;
using ExwhyzeeTechnology.Persistence.EF.SQL.Migrations;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.UserManagement
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class UpdateParticipantModel : PageModel
    {

        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly UserManager<Profile> _userManager;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public UpdateParticipantModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context, UserManager<Profile> userManager, IConfiguration config, IStorageService storageService)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
            _storageService = storageService;
        }
        [BindProperty]
        public Profile UserDatas { get; set; }

        [BindProperty]
        public IFormFile? imagefile { get; set; }

        [BindProperty]
        public AdditionalProfile AdditionalProfileData { get; set; }

        [BindProperty]
        public ProfileCategoryList ProfileCategoryList { get; set; }


        public IList<AdditionalProfile> AdditionalProfile { get; set; }
        public IList<ProfileCategory> ProfileCategories { get; set; }


        [BindProperty]
        public Profile Input { get; set; }

        public class InputModel
        {
            [Display(Name = "First Name")]
            [Required]
            public string? FirstName { get; set; }

            [Display(Name = "Title")]
            [Required]
            public string? Title { get; set; }

            [Display(Name = "Middle Name")]
            [Required]
            public string? MiddleName { get; set; }

            [Display(Name = "Last Name")]
            [Required]
            public string? LastName { get; set; }

            [Required]
            public string? PhoneNumber { get; set; }

            [Required]
            public string Email { get; set; }
            [Required]
            public string NIN { get; set; }
            [Required]
            public GenderStatus GenderStatus { get; set; }
            [Required]
            public string? EmploymentStatus { get; set; }


        }
        [BindProperty]
        public long? NewCourseId { get; set; }
        [BindProperty]
        public string redirectttt { get; set; }
        public List<CourseDropdownDto> CoursesDropdown { get; set; }
        [BindProperty]
        public long ParticipantId { get; set; }
        [BindProperty]
        public int Gender { get;set;}
        public Participant ParticipantInfo { get;set;}
        public async Task<IActionResult> OnGetAsync(long id)
        {
            var participant = await _context.Participants
                .Include(x=>x.Cohort).ThenInclude(x=>x.Course)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (participant == null)
            {
                return NotFound();
            }
            ParticipantInfo = participant;
            if (participant.User == null)
            {
                return NotFound();
            }
            ParticipantId = id;
            Input = participant.User;

            // Fetch courses with cohorts
            CoursesDropdown = await _context.Cohorts
                .Include(c => c.Course)
                .Select(c => new CourseDropdownDto
                {
                    Id = c.Id,
                    Text = $"{c.Course.Name} - Cohort {c.CohortNumber}"
                })
                .ToListAsync();

            ViewData["CourseId"] = new SelectList(CoursesDropdown, "Id", "Text");


            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var userinfo = await _userManager.FindByIdAsync(Input.Id);

                var checkemail = await _userManager.FindByEmailAsync(Input.Email);

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

                        var xresult = await _storageService.UploadFileReturnUrlAsync(s3Obj, cred, userinfo.PassportFilePathKey);
                        // 
                        if (xresult.Message.Contains("200"))
                        {
                            userinfo.PassportFilePathUrl = xresult.Url;
                            userinfo.PassportFilePathKey = xresult.Key;
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



                userinfo.FirstName = Input.FirstName;
                userinfo.LastName = Input.LastName;
                userinfo.MiddleName = Input.MiddleName;
                userinfo.PhoneNumber = Input.PhoneNumber;
                if (checkemail == null)
                {
                    var emx = await _userManager.GenerateChangeEmailTokenAsync(userinfo, Input.Email);
                    var upd = await _userManager.ChangeEmailAsync(userinfo, Input.Email, emx);
                    if (!upd.Succeeded)
                    {
                        TempData["error"] = "Unable to change email";
                    }
                    else
                    {
                        await _userManager.UpdateNormalizedEmailAsync(userinfo);

                        var ccemx = await _userManager.SetUserNameAsync(userinfo, Input.Email);
                        if (ccemx.Succeeded)
                        {
                            await _userManager.UpdateNormalizedUserNameAsync(userinfo);
                        }

                        TempData["success"] = "Email Changed Successfully";
                    }
                }

                userinfo.PersonalEmail = Input.NIN;
                userinfo.Gender = Input.Gender;
                userinfo.EmploymentStatus = Input.EmploymentStatus;
                await _userManager.UpdateAsync(userinfo);

                if (NewCourseId > 0)
                {
                    ///
                    // Create new participant
                    var getparticipant = await _context.Participants.FirstOrDefaultAsync(x => x.Id == ParticipantId);
                    if (getparticipant != null)
                    {

                        string input = getparticipant.IdNumber;

                        // Split the string by '/'
                        string[] parts = input.Split('/');


                        var getcohort = await _context.Cohorts.Include(z=>z.Course).FirstOrDefaultAsync(x=>x.Id == NewCourseId);
                        getparticipant.CohortId = NewCourseId;
                        //newparticipant.IdNumber = $"EXWHYZEE/{cohort.Course.Abbreviation}/{cohort.CohortCode}/{cohortSerialNumber:D2}";

                        string modified = input
            .Replace(parts[1], getcohort.Course.Abbreviation) // Replace PBED with NEWVALUE
            .Replace(parts[2], getcohort.CohortCode);

                        getparticipant.IdNumber = modified;

                        // Add new participant to the database
                        _context.Attach(getparticipant).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }
                //
                TempData["success"] = "Successfull";
                return RedirectToPage("./UpdateParticipant", new { id = ParticipantId });
            }
            catch (Exception c)
            {
                var participant = await _context.Participants
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == ParticipantId);
                if (participant == null)
                {
                    return NotFound();
                }
                if (participant.User == null)
                {
                    return NotFound();
                }
                ParticipantId = ParticipantId;
                Input = participant.User;

                // Fetch courses with cohorts
                CoursesDropdown = await _context.Cohorts
                    .Include(c => c.Course)
                    .Select(c => new CourseDropdownDto
                    {
                        Id = c.Id,
                        Text = $"{c.Course.Name} - Cohort {c.CohortNumber}"
                    })
                    .ToListAsync();

                ViewData["CourseId"] = new SelectList(CoursesDropdown, "Id", "Text");

                return Page();
            }
        }

    }
    public class CourseDropdownDto
    {
        public long Id { get; set; } // Course ID
        public string Text { get; set; } // Course Name + Cohort Number
    }
}
