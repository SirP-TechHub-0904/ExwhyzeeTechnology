using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExwhyzeeTechnology.Website.Areas.Management.Pages.Dashboard
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin")]

    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
