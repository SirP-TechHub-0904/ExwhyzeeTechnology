using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExwhyzeeTechnology.Domain.Enum.Enum;

namespace ExwhyzeeTechnology.Domain.Models.Data
{
    public class CohortAttendance
    {
        public long Id { get; set; }
        public string? UserId { get; set; }
        public Profile User { get; set; }

        public int? CohortParticipantId { get; set; }
        public Participant CohortParticipant { get; set; }
        [Display(Name = "SignIn Status")]
        public AttendanceSignInStatus AttendanceSignInStatus { get; set; }
        public bool SignInSubmitted { get; set; }

        [Display(Name = "SignOut Status")]
        public AttendanceSignOutStatus AttendanceSignOutStatus { get; set; }
        public bool SignOutSubmitted { get; set; }

        [Display(Name = "Dialy Activity")]
        public long DialyActivityId { get; set; }
        [Display(Name = "Dialy Activity")]
        public DialyActivity DialyActivity { get; set; }


        public int AccountType { get; set; }
    }
}
