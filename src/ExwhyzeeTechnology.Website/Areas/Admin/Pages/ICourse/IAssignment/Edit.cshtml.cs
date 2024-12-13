using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExwhyzeeTechnology.Domain.Models.Data;
using ExwhyzeeTechnology.Persistence.EF.SQL;
using ExwhyzeeTechnology.Application.Services.AWS;
using ExwhyzeeTechnology.Application.Dtos.AwsDtos;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.ICourse.IAssignment
{
    public class EditModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public EditModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context, IConfiguration config, IStorageService storageService)
        {
            _context = context;
            _config = config;
            _storageService = storageService;
        }

        [BindProperty]
        public Assignment Assignment { get; set; }
        [BindProperty]
        public IFormFile? imagefile { get; set; }
        [BindProperty]
        public DialyActivity DialyActivity { get; set; }
        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Assignment = await _context.Assignments
                .Include(a => a.DialyActivity).FirstOrDefaultAsync(m => m.Id == id);

            if (Assignment == null)
            {
                return NotFound();
            }
           ViewData["DialyActivityId"] = new SelectList(_context.DialyActivities, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //image
            if (imagefile != null)
            {
                try
                {
                    // Process file
                    await using var memoryStream = new MemoryStream();
                    await imagefile.CopyToAsync(memoryStream);

                    var fileExt = System.IO.Path.GetExtension(imagefile.FileName);
                    var docName = $"{Guid.NewGuid()}{fileExt}";
                    // call server

                    var s3Obj = new Application.Dtos.AwsDtos.S3Object()
                    {
                        BucketName = await _storageService.BucketName(),
                        InputStream = memoryStream,
                        Name = docName
                    };

                    var cred = new AwsCredentials()
                    {
                        AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                        SecretKey = _config["AwsConfiguration:AWSSecretKey"]
                    };

                    var xresult = await _storageService.UploadFileReturnUrlAsync(s3Obj, cred, "");
                    // 
                    if (xresult.Message.Contains("200"))
                    {
                        Assignment.FIleUrl = xresult.Url;
                        Assignment.FIleKey = xresult.Key;
                    }
                    else
                    {
                        TempData["error"] = "unable to upload file";
                        //return Page();
                    }
                }
                catch (Exception c)
                {

                }
            }


            _context.Attach(Assignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignmentExists(Assignment.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AssignmentExists(long id)
        {
            return _context.Assignments.Any(e => e.Id == id);
        }
    }
}
