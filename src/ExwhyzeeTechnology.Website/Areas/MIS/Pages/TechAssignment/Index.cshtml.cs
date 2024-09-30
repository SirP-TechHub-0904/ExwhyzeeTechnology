using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExwhyzeeTechnology.Website.Areas.MIS.Pages.TechAssignment
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class IndexModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            return RedirectToPage("/UserTechProfile/NoAdmission");
        }
    }
}
