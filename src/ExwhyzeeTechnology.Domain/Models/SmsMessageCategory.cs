using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExwhyzeeTechnology.Domain.Models
{
    public class SmsMessageCategory
    {
        public long Id { get; set; }
        public string? Title { get; set; }

        public ICollection<SmsMessageCategory> SmsList { get; set; }
    }
}
