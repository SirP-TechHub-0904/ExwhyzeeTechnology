using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using ExwhyzeeTechnology.Application.Repository.NotifyRegister;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Domain.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.UserManagement
{

    public class AdmissionStatusModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly IEmailSendService _email;
        private readonly ISmsSendService _sms;
        private readonly UserManager<Profile> _userManager;

        public AdmissionStatusModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context, IEmailSendService email, ISmsSendService sms, UserManager<Profile> userManager)
        {
            _context = context;
            _email = email;
            _sms = sms;
            _userManager = userManager;
        }

        public Participant Participant { get; set; }
        [BindProperty]
        public string UserId { get; set; }
        [BindProperty]
        public string ParticipantId { get; set; }

        [BindProperty]
        public bool SendEmail { get; set; }


        [BindProperty]
        public bool SendSMS { get; set; }


        [BindProperty]
        public string Letter { get; set; }
        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Participant = await _context.Participants.Include(x => x.Cohort).ThenInclude(x => x.Course).Include(x => x.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Participant == null)
            {
                return NotFound();
            }

            try
            {
                var normalsettings = await _context.Settings.FirstOrDefaultAsync();
                string emailTemplate = normalsettings.ApplicationLetter ?? "";
                emailTemplate = emailTemplate.Replace("**name**", Participant.User.FullnameX);
                emailTemplate = emailTemplate.Replace("**course**", Participant.Cohort.Course.Name);
                emailTemplate = emailTemplate.Replace("**cohort**", Participant.Cohort.CohortNumber);
                emailTemplate = emailTemplate.Replace("**startdate**", Participant.Cohort.StartDate.ToString("ddd dd MMM, yyyy"));
                emailTemplate = emailTemplate.Replace("**enddate**", Participant.Cohort.EndDate.ToString("ddd dd MMM, yyyy"));

                Letter = emailTemplate;
            }
            catch(Exception d) { }
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var user = await _userManager.FindByIdAsync(UserId);
                 
                

                if (SendSMS == true)
                {
                    var send = await _sms.SendSmsWithResult(user.PhoneNumber, "Kindly check your email and follow the instructions.");
                    if (send == true)
                    {
                        user.SmsSent = true;
                        await _userManager.UpdateAsync(user);
                    }
                }
                if (SendEmail == true)
                {

                    var sendmailparticipants = await _email.SendEmailWithResult(user.FullnameX, user.Email, "", "", $"EXWHYZEE TRAINING ADMISSION", Letter);


                    if (sendmailparticipants == true)
                    {
                        user.EmailSent = true;
                    }
                    await _userManager.UpdateAsync(user);


                }
                return RedirectToPage("./AdmissionStatus", new { id = ParticipantId });

            }
            catch (Exception ex)
            {

                return Page();
            }
        }

    }

}
