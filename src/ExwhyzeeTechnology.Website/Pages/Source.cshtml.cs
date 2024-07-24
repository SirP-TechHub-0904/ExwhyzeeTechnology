using ExwhyzeeTechnology.Application.Dtos;
using ExwhyzeeTechnology.Application.Repository.Services;
using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Pages
{
    public class SourceModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly ISettingsService _settingsService;
        public SourceModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context, ISettingsService settingsService)
        {
            _context = context;
            _settingsService = settingsService;
        }

        public WebPage WebPage { get; set; }
        public string Templatechoose { get; set; }
        public SuperSetting SuperSetting { get; set; }
        public async Task<IActionResult> OnGetAsync(long? id)
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

            if (id == null)
            {
                return NotFound();
            }
            if(id == 000)
            {
                return RedirectToPage("/Contact");
            }
            WebPage = await _context.WebPages.FirstOrDefaultAsync(m => m.Id == id);

            if (WebPage == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
