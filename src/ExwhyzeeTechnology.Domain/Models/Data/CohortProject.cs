using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExwhyzeeTechnology.Domain.Models.Data
{
    public class CohortProject
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Question { get; set; }
        public string? FIleUrl { get; set; }
        public string? FIleKey { get; set; }
        public DateTime Date { get; set; }
        public DateTime LastSubmissionDate { get; set; }

        public long CohortId { get; set; }
        public Cohort Cohort { get; set; }

        public string? UserId {  get; set; }
        public Profile User {  get; set; }
    }
}
