using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Areas.Student.Pages.UserTechProfile
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Student")]

    public class IndexModel : PageModel
    {
        
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public Profile UserDatas { get; set; }
        public IList<AdditionalProfile> AdditionalProfile { get; set; }
        public IList<ProfileCategory> ProfileCategories { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            UserDatas = await _userManager.FindByIdAsync(user.Id);

            if (UserDatas == null)
            {
                return NotFound();
            }
            AdditionalProfile = await _context.AdditionalProfiles.Where(x => x.ProfileId == UserDatas.Id).ToListAsync();
            ProfileCategories = await _context.ProfileCategories
                .Include(x => x.ProfileCategoryLists).Where(x => x.ProfileId == UserDatas.Id).ToListAsync();
            return Page();
        }
    }
}
