using ExwhyzeeTechnology.Application.Dtos;
using ExwhyzeeTechnology.Application.Repository.Services;
using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExwhyzeeTechnology.Website.Pages
{
    public class ProfileModel : PageModel
    {
          private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly ISettingsService _settingsService;
        private readonly UserManager<Profile> _userManager;

        public ProfileModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context, ISettingsService settingsService, UserManager<Profile> userManager)
        {
            _context = context;
            _settingsService = settingsService;
            _userManager = userManager;
        }
        public ProfilePortfolioSetting ProfilePortfolioSetting { get; set; }
        public async Task<IActionResult> OnGetAsync(string data, string key, string query)
        {
            var httpContext = HttpContext;
            VerificationWebDto setting = await _settingsService.ProfileData(httpContext);
             if(setting.PortfolioPath == "///")
            {
                return NotFound();
            }
            ProfilePortfolioSetting = setting.ProfilePortfolioSetting;

            return Page();
        }
   
    }
}
