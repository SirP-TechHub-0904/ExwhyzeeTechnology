using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExwhyzeeTechnology.Domain.Models.Data
{
    public class UserAssignment
    {
        public long Id { get; set; }
        public int ParticipantId { get; set; }
        public Participant Participant { get; set; }

        public long AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        public string? Answer {  get; set; }
        public string? FileUrl { get; set; }
        public string? FileKey { get; set; }

        public DateTime DateSubmitted { get;set;}
        public DateTime LastDateUpdated { get;set;}

        public long? GroupId { get; set; }
        public GroupAssignment? Group { get; set; }
    }

    public class GroupAssignment
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserAssignment> UserAssignments { get; set; }
    }
}
