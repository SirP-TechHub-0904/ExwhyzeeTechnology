using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExwhyzeeTechnology.Website.Areas.Student.Pages.Dashboard
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Student")]

    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost()
        {
        }
    }
}
