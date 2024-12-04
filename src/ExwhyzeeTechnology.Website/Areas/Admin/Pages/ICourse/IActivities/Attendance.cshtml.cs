using ExwhyzeeTechnology.Domain.Models.Data;
using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ExwhyzeeTechnology.Domain.Enum.Enum;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.ICourse.IActivities
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class AttendanceModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public AttendanceModel(ILogger<IndexModel> logger, Persistence.EF.SQL.DashboardDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IEnumerable<CohortAttendance> Datas { get; private set; }
        public Cohort Cohort { get; private set; }

        public DialyActivity DialyActivity { get; set; }

        public AttendanceSignInStatus AttendanceStatus { get; set; }

        [BindProperty]
        public long CohortId { get; set; }

        [BindProperty]
        public long DialyActivityId { get; set; }
        public async Task<IActionResult> OnGetAsync(long id, long aid)
        {
            if (id < 0)
            {
                return NotFound();
            }




             DialyActivity = await _context.DialyActivities.FindAsync(aid);

             Cohort = await _context.Cohorts.Include(x=>x.Course).FirstOrDefaultAsync(x=>x.Id == id);
            if (Cohort.CourseStatus != CourseStatus.Concluded)
            {
                // Retrieve the cohort
                var cohort = await _context.Cohorts.FindAsync(id);
                if (cohort == null)
                {
                    throw new ArgumentException("Invalid cohort ID");
                }

                // Get all participants of the cohort
                var participants = await _context.Participants
                    .Where(tp => tp.CohortId == id && tp.ParticipantStatus == ParticipantStatus.Active)
                    .Include(tp => tp.User)
                    .ToListAsync();

                

                //check null
                participants = participants.Where(x => x.User != null).ToList();
 


                // Get the daily activities associated with the cohort
                var dailyActivities = await _context.DialyActivities
                    .Where(da => da.CohortId == id)
                    .Include(da => da.CohortAttendance)
                    .ToListAsync();

                // Helper function to add attendance
                void AddAttendance(string userId, int id, int accountType, DialyActivity activity)
                {
                    var existingAttendance = activity.CohortAttendance
                        .FirstOrDefault(a => a.UserId == userId && a.DialyActivityId == activity.Id);

                    if (existingAttendance == null)
                    {
                        activity.CohortAttendance.Add(new CohortAttendance
                        {
                            UserId = userId,
                            CohortParticipantId = id,
                            DialyActivityId = activity.Id,
                            AttendanceSignInStatus = AttendanceSignInStatus.Null, // or any other status
                            AttendanceSignOutStatus = AttendanceSignOutStatus.Null,
                            AccountType = accountType
                        });
                    }
                    
                }

                // Iterate over each participant and add attendance
                foreach (var participant in participants)
                {
                    foreach (var activity in dailyActivities)
                    {
                        AddAttendance(participant.UserId, participant.Id, 1, activity);
                    }
                }

             
                await _context.SaveChangesAsync(); // Save changes to the database
            }

            Datas = await _context.CohortAttendances
                .Include(x => x.User)
                .Where(x => x.DialyActivityId == aid).ToListAsync();

            return Page();
        }



        public async Task<IActionResult> OnPostSignInAsync()
        {
            StringBuilder formInfo = new StringBuilder();
            var attendanceData = new List<(long attendanceId, AttendanceSignInStatus status)>();
            // Initialize counters for each status
            int presentCount = 0;
            int absentCount = 0;

            foreach (var key in Request.Form.Keys)
            {
                string value = Request.Form[key];
                formInfo.AppendLine($"{key}: {value}");

                // Check if the key starts with "AttendanceResult_"
                if (key.StartsWith("AttendanceSignInResult_"))
                {
                    // Extract the attendance ID from the key
                    if (long.TryParse(key.Substring("AttendanceSignInResult_".Length), out long attendanceId))
                    {
                        // Get the enum value from the form
                        if (Enum.TryParse(value, out ExwhyzeeTechnology.Domain.Enum.Enum.AttendanceSignInStatus status))
                        {
                            // Add the extracted attendance ID and status to the list
                            attendanceData.Add((attendanceId, status));
                            // Increment the corresponding counter
                            switch (status)
                            {
                                case AttendanceSignInStatus.Present:
                                    presentCount++;
                                    break;
                                case AttendanceSignInStatus.Absent:
                                    absentCount++;
                                    break;

                            }
                        }
                        else
                        {
                            // Handle invalid enum value
                            // Perhaps return an error response or log the issue
                        }
                    }
                    else
                    {
                        // Handle invalid attendance ID
                        // Perhaps return an error response or log the issue
                    }
                }
            }

            // Now you have attendanceData populated with attendance IDs and statuses
            // Pass attendanceData to the command handler
            foreach (var (attendanceId, status) in attendanceData)
            {
                // Retrieve the Attendance record by its ID
                var attendance = await _context.CohortAttendances.FindAsync(attendanceId);
                if (attendance != null)
                {
                    // Update the AttendanceStatus
                    attendance.AttendanceSignInStatus = status;
                    attendance.SignInSubmitted = true;
                    _context.Attach(attendance).State = EntityState.Modified;

                }
                else
                {
                    // Handle the case where the provided attendanceId is not found
                    throw new ArgumentException($"Attendance record with ID {attendanceId} not found.");
                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

 
            // Construct the TempData message with the counts
            string message = $"{presentCount} signin, {absentCount} not available";

            // Store the message in TempData
            TempData["response"] = message;
            return RedirectToPage("./Attendance", new { id = CohortId, aid = DialyActivityId });
            // Your existing code continues here...
        }


        public async Task<IActionResult> OnPostSignOutAsync()
        {
            StringBuilder formInfo = new StringBuilder();
            var attendanceData = new List<(long attendanceId, AttendanceSignOutStatus status)>();
            // Initialize counters for each status
            int presentCount = 0;
            int absentCount = 0;

            foreach (var key in Request.Form.Keys)
            {
                string value = Request.Form[key];
                formInfo.AppendLine($"{key}: {value}");

                // Check if the key starts with "AttendanceResult_"
                if (key.StartsWith("AttendanceSignOutResult_"))
                {
                    // Extract the attendance ID from the key
                    if (long.TryParse(key.Substring("AttendanceSignOutResult_".Length), out long attendanceId))
                    {
                        // Get the enum value from the form
                        if (Enum.TryParse(value, out ExwhyzeeTechnology.Domain.Enum.Enum.AttendanceSignOutStatus status))
                        {
                            // Add the extracted attendance ID and status to the list
                            attendanceData.Add((attendanceId, status));
                            // Increment the corresponding counter
                            switch (status)
                            {
                                case AttendanceSignOutStatus.Present:
                                    presentCount++;
                                    break;
                                case AttendanceSignOutStatus.Absent:
                                    absentCount++;
                                    break;

                            }
                        }
                        else
                        {
                            // Handle invalid enum value
                            // Perhaps return an error response or log the issue
                        }
                    }
                    else
                    {
                        // Handle invalid attendance ID
                        // Perhaps return an error response or log the issue
                    }
                }
            }

            // Now you have attendanceData populated with attendance IDs and statuses
           
            // Construct the TempData message with the counts
            string message = $"{presentCount} signout, {absentCount} not available";

            // Store the message in TempData
            TempData["response"] = message;
            return RedirectToPage("./Attendance", new { id = CohortId, aid = DialyActivityId });
            // Your existing code continues here...
        }


        public async Task<IActionResult> OnPostDeleteAttendanceAsync()
        {
            try
            {
                var attendance = await _context.CohortAttendances
                    .Where(da => da.DialyActivityId == DialyActivityId)
                    .ToListAsync();

                foreach (var item in attendance)
                {
                    _context.CohortAttendances.Remove(item);

                }
                await _context.SaveChangesAsync();
            }
            catch (Exception c) { }
            return RedirectToPage("./Attendance", new { id = CohortId, aid = DialyActivityId });

        }
    }
}
