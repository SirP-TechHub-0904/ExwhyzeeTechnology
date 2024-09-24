using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.UserManagement
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin")]

    public class DeleteModel : PageModel
    {
        private readonly UserManager<Profile> _userManager;
        private readonly SignInManager<Profile> _signInManager;
        private readonly ILogger<IndexModel> _logger;
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public DeleteModel(SignInManager<Profile> signInManager,
            ILogger<IndexModel> logger,
            UserManager<Profile> userManager,
            Persistence.EF.SQL.DashboardDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }
        public Profile UserDatas { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserDatas = await _userManager.FindByIdAsync(id);

            if (UserDatas == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);


            if (user != null)
            {
                var tr = await _context.TrainingApplicationForms.Where(x => x.ProfileId == user.Id).ToListAsync();
                foreach (var x in tr)
                {
                    _context.TrainingApplicationForms.Remove(x);
                }
                var prof = await _context.ProfileCategories.Where(x => x.ProfileId == user.Id).ToListAsync();
                foreach (var x in prof)
                {
                    _context.ProfileCategories.Remove(x);
                }
                var ccr = await _context.ProfileCategoryLists.Where(x => x.ProfileId == user.Id).ToListAsync();
                foreach (var x in ccr)
                {
                    _context.ProfileCategoryLists.Remove(x);
                }

                await _context.SaveChangesAsync();
                try
                {
                    await _userManager.RemoveFromRoleAsync(user, "TRAINING");
                }
                catch (Exception c) { }

                try
                {
                    await _userManager.DeleteAsync(user);
                }
                catch (Exception c) { }


            }

            return RedirectToPage("./Training");
        }
        public async Task<IActionResult> OnPostAllAsync()
        {

            //var xuser = await _userManager.Users.Where(x => x.FirstName.Contains("http") || x.FirstName.Contains("/") ||
            //x.MiddleName.Contains("http") || x.MiddleName.Contains("/")

            //|| x.LastName.Contains("http") || x.LastName.Contains("/")).ToListAsync();


            //foreach (var user in xuser)
            //{
            //    var tr = await _context.TrainingApplicationForms.Where(x => x.ProfileId == user.Id).ToListAsync();
            //    foreach (var x in tr)
            //    {
            //        _context.TrainingApplicationForms.Remove(x);
            //    }
            //    var prof = await _context.ProfileCategories.Where(x => x.ProfileId == user.Id).ToListAsync();
            //    foreach (var x in prof)
            //    {
            //        _context.ProfileCategories.Remove(x);
            //    }
            //    var ccr = await _context.ProfileCategoryLists.Where(x => x.ProfileId == user.Id).ToListAsync();
            //    foreach (var x in ccr)
            //    {
            //        _context.ProfileCategoryLists.Remove(x);
            //    }

            //    await _context.SaveChangesAsync();
            //    try
            //    {
            //        await _userManager.RemoveFromRoleAsync(user, "TRAINING");
            //    }
            //    catch (Exception c) { }

            //    try
            //    {
            //        await _userManager.DeleteAsync(user);
            //    }
            //    catch (Exception c) { }


            //}

            return RedirectToPage("./Training");
        }

    }
}
