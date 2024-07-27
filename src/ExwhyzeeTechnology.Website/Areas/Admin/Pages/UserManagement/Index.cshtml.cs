using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static ExwhyzeeTechnology.Domain.Enum.Enum;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.UserManagement
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Manager,Admin")]
    public class RoleDTO
    {
        public string RoleName { get; set; }
        // You can add more properties related to roles if needed
    }
    public class ProfileDto
    {
        public string Id { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public UserStatus Status { get; set; }
         public GenderStatus Gender { get; set; }
        public string Roles { get; set; }
        public string State { get; set; }
        public string LGA { get; set; }
        public DateTime Date { get; set; }
    }
    public class IndexModel : PageModel
    {
        private readonly UserManager<Profile> _userManager;
        private readonly SignInManager<Profile> _signInManager;
        private readonly ILogger<IndexModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IndexModel(SignInManager<Profile> signInManager,
            ILogger<IndexModel> logger,
            UserManager<Profile> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
        }
        public IList<ProfileDto> UserDatasList { get; set; }

        public async Task OnGetAsync()
        {
            UserDatasList = new List<ProfileDto>();
            var UserDatas = await _userManager.Users.Where(x => x.UserStatus == Domain.Enum.Enum.UserStatus.Active).ToListAsync();

            var excludedRoles = new List<string> { "mSuperAdmin", "SubAdmin" };
            //var excludedRoles = new List<string> { "mSuperAdmin", "SubAdmin" };

            foreach (var user in UserDatas)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                if (userRoles.All(roleName => !excludedRoles.Contains(roleName)))
                {
                    var profile = new ProfileDto
                    {
                        Fullname = user.FirstName + " " + user.MiddleName + " " + user.LastName,
                        Phone = user.PhoneNumber,
                        Email = user.Email,
                        Status = user.UserStatus,
                        Id = user.Id,
                        Roles = string.Join(", ", userRoles) // Combine roles into a comma-separated string
                    };

                    UserDatasList.Add(profile);
                }
            }
        }
    }
}
