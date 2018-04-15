using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int AnswerDictId { get; set; }
        public int QuestionId { get; set; }
    }
}
