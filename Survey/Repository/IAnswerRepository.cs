using SurveyApp.Models;
using SurveyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Repository
{
    public interface IAnswerRepository
    {
        Task<bool> SaveManyAsync(List<Answer> answers);
        Task<List<StatsVM>> GetAnswersBySurveyIdAsync(int surveyId);
     
    }
}
