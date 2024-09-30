 
using ExwhyzeeTechnology.Application.Dtos.AwsDtos;
using ExwhyzeeTechnology.Application.Services.AWS;
using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Areas.Account.Pages.Account
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class EducationModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly UserManager<Profile> _userManager;
        public EducationModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [BindProperty]
        public Profile UserDatas { get; set; }

        [BindProperty]
        public IFormFile? imagefile { get; set; }
        [BindProperty]
        public IFormFile? imageidcard { get; set; }
        [BindProperty]
        public IFormFile? imageemmergency { get; set; }
        [BindProperty]
        public AdditionalProfile AdditionalProfileData { get; set; }

        [BindProperty]
        public ProfileCategoryList ProfileCategoryList { get; set; }


        public IList<AdditionalProfile> AdditionalProfile { get; set; }
        public IList<ProfileCategory> ProfileCategories { get; set; }


        [BindProperty]
        public Profile Input { get; set; }

        [BindProperty]
        public string redirectttt { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();

            }

            UserDatas = await _userManager.FindByIdAsync(id);

            Input = UserDatas;
            if (UserDatas == null)
            {
                return NotFound();
            }
            AdditionalProfile = await _context.AdditionalProfiles.Where(x => x.ProfileId == UserDatas.Id).ToListAsync();
            ProfileCategories = await _context.ProfileCategories.Where(x => x.Title.ToUpper().Contains("EDUCATION"))
                .Include(x => x.ProfileCategoryLists).Where(x => x.ProfileId == UserDatas.Id).ToListAsync();
            

            return Page();
        }
        [BindProperty]
        public bool AcoomodationStatusPopup { get; set; }

        public async Task<IActionResult> OnPostAdditionalAsync()
        {
            _context.AdditionalProfiles.Add(AdditionalProfileData);
            await _context.SaveChangesAsync();
            TempData["success"] = "Successfull";

            return RedirectToPage("./Education", new { id = AdditionalProfileData.ProfileId });
        }

        public async Task<IActionResult> OnPostCategoriesAsync()
        {
            _context.ProfileCategoryLists.Add(ProfileCategoryList);
            await _context.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(ProfileCategoryList.ProfileId);
            user.UpdateEducation = false;
            await _userManager.UpdateAsync(user);
            TempData["success"] = "Successfull";
            
                return RedirectToPage("./Education", new { id = user.Id });
             
         }


    }
}
