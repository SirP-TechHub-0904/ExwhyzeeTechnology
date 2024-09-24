using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExwhyzeeTechnology.Website.Areas.Student.Pages.TechClasses
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Student")]

    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
