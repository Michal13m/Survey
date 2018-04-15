using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; }
        public int SurveyId { get; set; }
        public int AnswerDictId { get; set; }

        public List<AnswerDict> AnswersDict { get; set; }
    }
}
