using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Models;
using SurveyApp.Repository;

namespace SurveyApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Answer")]
    public class AnswerController : Controller
    {
        private IAnswerRepository _aswerRepo;

        public AnswerController(IAnswerRepository aswerRepo)
        {
            _aswerRepo = aswerRepo;
        }

        [HttpPost]
        [Route("SaveMany")]
        public async Task<IActionResult> SaveMany([FromBody]List<Answer> answers)
        {
            try
            {
                if (answers.Count == 0) throw new Exception("Empty");
                if (answers.Any(a => a.SurveyId < 1 || a.QuestionId < 1 || a.AnswerDictId < 1)) throw new Exception("Wrong data");

                var res = await _aswerRepo.SaveManyAsync(answers);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetAnsersBySurveyId")]
        public async Task<IActionResult> GetAnsersBySurveyId(int surveyId)
        {
            try
            {
                if (surveyId < 1) throw new Exception("survey not exists");

                var res = await _aswerRepo.GetAnswersBySurveyIdAsync(surveyId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}