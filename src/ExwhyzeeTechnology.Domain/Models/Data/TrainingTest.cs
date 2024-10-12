using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExwhyzeeTechnology.Domain.Enum.Enum;

namespace ExwhyzeeTechnology.Domain.Models.Data
{
    public class TrainingTest
    {
        public long Id { get; set; }
        public TrainingTestType TrainingTestType { get; set; }
        public string Question { get; set; }
        public bool Publish { get; set; }
        public int SortOrder { get; set; } 
        public ICollection<TrainingTestOption> TrainingTestOptions { get; set; }

        public long? CourseId   { get; set; }

    }
}
