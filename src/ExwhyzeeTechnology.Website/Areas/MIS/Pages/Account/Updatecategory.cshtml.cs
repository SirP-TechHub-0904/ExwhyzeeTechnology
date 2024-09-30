using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Persistence.EF.SQL;

namespace ExwhyzeeTechnology.Website.Areas.Account.Pages.Account
{
    public class UpdatecategoryModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public UpdatecategoryModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProfileCategoryList ProfileCategoryList { get; set; }
        [BindProperty]
        public string LK {  get; set; }
        public IList<ProfileCategory> ProfileCategories { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id, string lk)
        {
            if (id == null)
            {
                return NotFound();
            }
            LK = lk;
            ProfileCategoryList = await _context.ProfileCategoryLists
                .Include(p => p.Profile)
                .Include(p => p.ProfileCategory).FirstOrDefaultAsync(m => m.Id == id);
            ProfileCategories = await _context.ProfileCategories
               .Include(x => x.ProfileCategoryLists).Where(x => x.Id == ProfileCategoryList.ProfileCategoryId).ToListAsync();
            if (ProfileCategoryList == null)
            {
                return NotFound();
            }
          
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
           

            _context.Attach(ProfileCategoryList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileCategoryListExists(ProfileCategoryList.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
          
                return RedirectToPage("./" + LK, new { id = ProfileCategoryList.ProfileId });
             
           
        }

        private bool ProfileCategoryListExists(long id)
        {
            return _context.ProfileCategoryLists.Any(e => e.Id == id);
        }
    }
}
