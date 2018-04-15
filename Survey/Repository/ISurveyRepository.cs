using SurveyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Service
{
    public interface ISurveyRepository
    {
         Task<List<Survey>> GetAsync();

        Task<bool> CreateAsync(Survey survey);

        bool SurveyNameExists(string name);

        Task<Survey> GetByIdAsync(int id);
    }
}
