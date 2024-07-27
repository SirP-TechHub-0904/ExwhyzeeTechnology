using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExwhyzeeTechnology.Domain.Models
{
    public class SelectedJobRole
    {
        public long Id { get; set; }
        public long? CareerFileId { get; set; }
        public CareerFile CareerFile { get; set; }

        public long? CareerTrainingJobRoleId { get; set; }
        public CareerTrainingJobRole CareerTrainingJobRole { get; set; }
    }
}
