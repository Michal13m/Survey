using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Contex;
using SurveyApp.Models;
using SurveyApp.ViewModel;

namespace SurveyApp.Repository
{
    public class AnswerRepository: IAnswerRepository
    {

        private SurveyContext _surveyContext;

        public AnswerRepository(SurveyContext surveyContext)
        {
            _surveyContext = surveyContext;
        }

        public async  Task<List<StatsVM>> GetAnswersBySurveyIdAsync(int surveyId)
        {
            var ansers = _surveyContext.Answers.Where(s => s.SurveyId == surveyId);

            var questions = _surveyContext.Questions;

            var questionsAns = _surveyContext.Questions
                                .Where(q => q.SurveyId == surveyId)
                                    .Include(a => a.AnswersDict);

            var answersDict = _surveyContext.AnswersDict;

             var stats =await ansers
                 .Join(questions, 
                        a => a.QuestionId, 
                        q => q.Id,
                       (a,q)=>new { a, qestName =q.Description })
                  .Join(answersDict
                        ,a =>a.a.AnswerDictId
                        ,ad=>ad.Id,
                        (a, ad) => new { a,ad})
                  .GroupBy(s=>new { s.a.qestName,s.ad.Description})
                  .Select(group => new StatsVM
                  {
                      Answer = group.Key.Description,
                      Question=group.Key.qestName,
                      AnswerCount = group.Count()
                  }).ToListAsync();

            Helper.Helper.AddNotAswerdQuestion(ref stats, questionsAns);
             
            return stats.OrderBy(o=>o.Question).ThenBy(t=>t.AnswerCount).ToList();
        }


        public async Task<bool> SaveManyAsync(List<Answer> answers)
        {
            try
            {
                await _surveyContext.Answers.AddRangeAsync(answers);
                await _surveyContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
    }
}
