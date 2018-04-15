using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Models;
using SurveyApp.Service;

namespace SurveyApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Survey")]
    public class SurveyController : Controller
    {
        private ISurveyRepository _surveyRepo;

        public SurveyController(ISurveyRepository surveyRepo)
        {
            _surveyRepo = surveyRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var surveys = await _surveyRepo.GetAsync();

                return Ok(surveys);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id < 1) throw new Exception("Wrong survey id");

                var survey = await _surveyRepo.GetByIdAsync(id);

                return Ok(survey);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody]Survey survey)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(survey.Name) || survey.Questions.Count < 1) return BadRequest();

                var created = await _surveyRepo.CreateAsync(survey);

                return Ok(created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

           
        }
    }
}