using ExwhyzeeTechnology.Application.Dtos;
using ExwhyzeeTechnology.Application.Repository.Services;
using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Pages
{
    public class CatBpgModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly ISettingsService _settingsService;
        public CatBpgModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context, ISettingsService settingsService)
        {
            _context = context;
            _settingsService = settingsService;
        }
        public string Templatechoose { get; set; }
        public Setting Setting { get; set; }
        public SuperSetting SuperSetting { get; set; }
        public IList<Blog> Blogs { get; set; }
        public BlogCategory BlogCategory { get; set; }
        public IList<BlogCategory> BlogCategoryList { get; set; }
        public async Task<IActionResult> OnGetAsync(long id, string? title)
        {
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

            Templatechoose = setting.SuperSetting.TemplateLayoutKey;
            SuperSetting = setting.SuperSetting;
            Blogs = await _context.Blogs.Where(x => x.Publish == true && x.BlogCategoryId == id).OrderByDescending(x => x.Date).ToListAsync();
            BlogCategory = await _context.BlogCategories.FirstOrDefaultAsync(x=>x.Id == id);
            BlogCategoryList = await _context.BlogCategories.Where(x=>x.Publish == true).ToListAsync();

            Setting = await _context.Settings.FirstOrDefaultAsync();
            return Page();
        }


    }
}
