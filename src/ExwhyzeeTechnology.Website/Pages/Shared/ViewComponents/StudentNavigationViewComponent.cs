using Microsoft.AspNetCore.Mvc;

namespace ExwhyzeeTechnology.Website.Pages.Shared.ViewComponents
{
    public class StudentNavigationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View();
        }
    }
}
