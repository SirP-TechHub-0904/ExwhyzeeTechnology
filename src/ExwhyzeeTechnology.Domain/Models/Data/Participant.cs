using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExwhyzeeTechnology.Domain.Enum.Enum;

namespace ExwhyzeeTechnology.Domain.Models.Data
{
    public class Participant
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public Profile User { get; set; }
        public long? CohortId { get; set; }
        public Cohort Cohort { get; set; }
        public string IdNumber { get; set; }
        public int SerialNumber { get; set; } 
        public ParticipantStatus ParticipantStatus { get; set; }

    }
}
