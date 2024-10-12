using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExwhyzeeTechnology.Domain.Enum.Enum;

namespace ExwhyzeeTechnology.Domain.Models.Data
{
    public class Cohort
    {
        public long Id { get; set; }


        public long? CourseId { get; set; }
        [Display(Name = "Course")]
        public Course Course { get; set; }
        [Display(Name = "Cohort Number")]
        public string CohortNumber { get; set; }
        [Display(Name = "Cohort Code")]
        public string CohortCode { get; set; }

        public string? Description { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public DateTime Year { get; set; }
        [Display(Name = "Course Status")]
        public CourseStatus CourseStatus { get; set; }
        public string Theme { get; set; } 

        public ICollection<Participant> Participants { get; set; }
         
    }
}
