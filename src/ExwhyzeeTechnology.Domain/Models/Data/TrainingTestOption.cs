﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExwhyzeeTechnology.Domain.Models.Data
{
    public class TrainingTestOption
    {
        public long Id { get; set; }
        public string Option { get; set; }
        public int SortOrder { get; set; }
        public bool CorrectAnswer { get; set; }

        public long? TrainingTestId { get; set; }
        public TrainingTest TrainingTest { get; set; }
    }
}
