using ExwhyzeeTechnology.Domain.Models.Data;
using ExwhyzeeTechnology.Persistence.EF.SQL.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Http;
using static ExwhyzeeTechnology.Domain.Enum.Enum;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.UserManagement
{
    public class YearbookDetailsModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly HttpClient _httpClient;
        public YearbookDetailsModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }




        public List<ParticipantDto> Participants { get; set; }
        public Course Course { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            // Filter participants by the provided cohortId
            var participants = await _context.Participants
               
                .Include(p => p.Cohort).ThenInclude(c => c.Course)
                .Include(p => p.User).Where(p => p.Cohort.CourseId == id)
                .ToListAsync();

            // Map participants to ParticipantDto
            Participants = (await Task.WhenAll(participants.Select(async p => new ParticipantDto
            {
                FullnameX = p.User.FullnameX,
                Email = p.User.Email,
                PhoneNumber = p.User.PhoneNumber,
                NIN = p.User.NIN,
                Gender = p.User.Gender,
                IdNumber = p.IdNumber,
                EmploymentStatus = p.User.EmploymentStatus,
                PassportFile = await GetFileFromUrlAsync(p.User.PassportFilePathUrl)
            }))).ToList(); // Convert the array to a List

            //
            Course = await _context.Courses.FirstOrDefaultAsync(x=>x.Id == id);


            return Page();
        }

        public async Task<byte[]> GetFileFromUrlAsync(string url)
        {
            try
            {
                // Validate URL
                if (string.IsNullOrWhiteSpace(url) || !Uri.TryCreate(url, UriKind.Absolute, out var uriResult) ||
                    !(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                {
                    return null;
                }

                // Fetch file from URL
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch
            {
                // Return default binary in case of any exception
                return null;
            }
        }
         
    }
    public class ParticipantDto
    {
        public string FullnameX { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NIN { get; set; }
        public GenderStatus Gender { get; set; }
        public string IdNumber { get; set; }
        public string EmploymentStatus { get; set; }
        public byte[] PassportFile { get; set; }
    }

}