using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Contex;
using SurveyApp.Models;

namespace SurveyApp.Service
{
    public class SurveyRepository : ISurveyRepository
    {
        private SurveyContext _surveyContext;

        public SurveyRepository(SurveyContext surveyContext)
        {
            _surveyContext = surveyContext;
        }
        public async Task<bool> CreateAsync(Survey survey)
        {
            try
            {
                if (SurveyNameExists(survey.Name)) throw new Exception("This name allready exists");

                await _surveyContext.Surveys.AddAsync(survey);
                await _surveyContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Error on create"+ex.Message);
            }
        }

        public async Task<List<Survey>> GetAsync()
        {
            return await _surveyContext.Surveys.ToListAsync();
        }

        public async Task<Survey> GetByIdAsync(int id)
        {
            try
            {
                return await _surveyContext.Surveys.Where(s => s.Id == id)
                    .Include(q=>q.Questions)
                    .ThenInclude(a=>a.AnswersDict)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SurveyNameExists(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name)) throw new Exception("Name cant be empty");

                var exists = _surveyContext.Surveys.Where(x => x.Name == name).Count() > 0 ? true : false;

                return exists;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
