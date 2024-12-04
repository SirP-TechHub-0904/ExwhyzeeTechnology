using ExwhyzeeTechnology.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExwhyzeeTechnology.Application.Dtos
{
    public class UserTestListDto
    {
        public string UserId { get; set; }
        public Profile User { get; set; }
        public string PreTestScore { get; set; }
        public string PostTestScore { get; set; }

        public bool PreTestTaken { get; set; }
        public bool PostTestTaken { get; set; }
    }
}
