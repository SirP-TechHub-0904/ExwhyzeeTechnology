using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExwhyzeeTechnology.Domain.Models.Data
{
    public class UserTest
    {
        public long Id { get; set; }
        public long TrainingTestId { get; set; }
        public TrainingTest TrainingTest { get; set; }
        public string? UserId { get; set; }
        public Profile User { get; set; }
        public long CohortId { get; set; }
        public Cohort Cohort { get; set; }

        public string Answer { get; set; }
        public bool Submitted { get; set; }
        public DateTime Date { get; set; }
    }
}
