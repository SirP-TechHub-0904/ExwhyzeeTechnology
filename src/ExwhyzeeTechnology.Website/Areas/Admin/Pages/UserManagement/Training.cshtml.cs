using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static ExwhyzeeTechnology.Domain.Enum.Enum;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.UserManagement
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Manager,Admin")]
    
    public class TrainingModel : PageModel
    {
        private readonly UserManager<Profile> _userManager;
        private readonly SignInManager<Profile> _signInManager;
        private readonly ILogger<IndexModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public TrainingModel(SignInManager<Profile> signInManager,
            ILogger<IndexModel> logger,
            UserManager<Profile> userManager,
            RoleManager<IdentityRole> roleManager,
            Persistence.EF.SQL.DashboardDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            _context = context;
        }
        public IList<ProfileDto> UserDatasList { get; set; }
        public IList<TrainingApplicationForm> Training { get; set; }

        public async Task OnGetAsync()
        {
            //UserDatasList = new List<ProfileDto>();
            //var UserDatas = await _userManager.Users.ToListAsync();

            //var excludedRoles = new List<string> { "TRAINING" }; 

            //foreach (var user in UserDatas)
            //{
            //    var userRoles = await _userManager.GetRolesAsync(user);

            //    if (userRoles.All(roleName => excludedRoles.Contains(roleName)))
            //    {
            //        var profile = new ProfileDto
            //        {
            //            Fullname = user.FirstName + " " + user.MiddleName + " " + user.LastName,
            //            Phone = user.PhoneNumber,
            //            Email = user.Email,
            //            Status = user.UserStatus,
            //            Id = user.Id,
            //            State = user.State,
            //            LGA = user.PermanentLga,
            //            Gender = user.Gender,
            //            Date = user.Date,
            //            Roles = string.Join(", ", userRoles) // Combine roles into a comma-separated string
            //        };

            //        UserDatasList.Add(profile);
            //    }
            //}

            Training = await _context.TrainingApplicationForms
                .Include(x=>x.Profile)
                .Include(x=>x.CareerTrainingJobRole)
                .ToListAsync();
        }
    }
     
}
