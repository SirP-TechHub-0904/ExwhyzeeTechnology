using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExwhyzeeTechnology.Domain.Enum.Enum;

namespace ExwhyzeeTechnology.Domain.Models.Data
{
    public class TimeTable
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public int SortOrder { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Content { get; set; } 
        public EventType EventType { get; set; }
        public ContentStatus ContentStatus { get; set; }
        public ContentEnumType ContentType { get; set; }
        public string? Note { get; set; }

        public bool IsLecture { get; set; }
        public string? Module { get; set; }

        public long? CohortId { get; set; }
        public Cohort Cohort { get; set; }

    }
}
