using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExwhyzeeTechnology.Domain.Models.Data
{
    public class DialyActivity
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }
        [Display(Name = "Finish Time")]
        public TimeSpan FinishTime { get; set; }
        public ICollection<CohortAttendance> CohortAttendance { get; set; }


        public long CohortId { get; set; }
        public Cohort Cohort { get; set; }

    }
}
