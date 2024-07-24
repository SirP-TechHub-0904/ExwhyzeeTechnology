using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Areas.Content.Pages.IBlogs.Post
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Editor")]

    public class PostListModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public PostListModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IList<Blog> Blog { get; set; }

        public async Task OnGetAsync(long id)
        {
            Blog = await _context.Blogs
                .Include(b => b.BlogCategory).Where(x=>x.BlogCategoryId == id).ToListAsync();
        }
    }
}
