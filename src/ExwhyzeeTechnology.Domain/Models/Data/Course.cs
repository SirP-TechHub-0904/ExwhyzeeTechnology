using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExwhyzeeTechnology.Domain.Enum.Enum;

namespace ExwhyzeeTechnology.Domain.Models.Data
{
    public class Course
    {
        public long Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Abbreviation")]
        public string Abbreviation { get; set; }
        public ICollection<Cohort> Cohort { get; set; }
    }
}
