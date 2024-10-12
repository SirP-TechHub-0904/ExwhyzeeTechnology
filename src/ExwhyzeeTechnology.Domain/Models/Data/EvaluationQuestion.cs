using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExwhyzeeTechnology.Domain.Enum.Enum;

namespace ExwhyzeeTechnology.Domain.Models.Data
{
    public class EvaluationQuestion
    {
        public long Id { get; set; } 
        public string Question { get; set; }
        public bool Publish { get; set; }
        public int SortOrder { get; set; } 

        public EvaluationAnswerType EvaluationAnswerType { get; set; }


        public long? EvaluationQuestionCategoryId {  get; set; }
        public EvaluationQuestionCategory EvaluationQuestionCategory {  get; set; }

    }
}
