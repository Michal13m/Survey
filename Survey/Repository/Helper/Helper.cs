using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SurveyApp.Models;
using SurveyApp.ViewModel;

namespace SurveyApp.Repository.Helper
{
    public static class Helper
    {
        internal static void AddNotAswerdQuestion(ref List<StatsVM> stats, IIncludableQueryable<Question, List<AnswerDict>> questionsAns)
        {
            try
            {

                foreach (var quest in questionsAns)
                {
                    foreach (var ans in quest.AnswersDict)
                    {
                        var stat = new StatsVM()
                        {
                            Question = quest.Description,
                            Answer = ans.Description
                        };

                        if (!stats.Any(s => s.Answer == stat.Answer && s.Question == stat.Question))
                        {
                            stats.Add(stat);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
