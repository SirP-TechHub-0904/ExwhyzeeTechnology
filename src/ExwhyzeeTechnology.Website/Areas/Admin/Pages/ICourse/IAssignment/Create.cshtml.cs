using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExwhyzeeTechnology.Domain.Models.Data;
using ExwhyzeeTechnology.Persistence.EF.SQL;
using Microsoft.EntityFrameworkCore;
using ExwhyzeeTechnology.Application.Services.AWS;
using DocumentFormat.OpenXml.Vml;
using ExwhyzeeTechnology.Application.Dtos.AwsDtos;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.ICourse.IAssignment
{
    public class CreateModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public CreateModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context, IConfiguration config, IStorageService storageService)
        {
            _context = context;
            _config = config;
            _storageService = storageService;
        }

        [BindProperty]
        public DialyActivity DialyActivity { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            DialyActivity = await _context.DialyActivities.FirstOrDefaultAsync(x => x.Id == id);
            if(DialyActivity == null)
            {
                return NotFound();
            }
            return Page();
        }

        [BindProperty]
        public Assignment Assignment { get; set; }
        [BindProperty]
        public IFormFile? imagefile { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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


            _context.Assignments.Add(Assignment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new {id = Assignment.DialyActivityId});
        }
    }
}
