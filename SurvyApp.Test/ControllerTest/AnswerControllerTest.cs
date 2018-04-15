using Microsoft.VisualStudio.TestTools.UnitTesting;
using SurveyApp.Models;
using Moq;
using System.Collections.Generic;
using SurveyApp.Repository;
using SurveyApp.Controllers;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FluentAssertions;
using System;
using Microsoft.AspNetCore.Mvc;

namespace SurvyApp.Test.ControllerTest
{
    [TestClass]
    public class AnswerControllerTest
    {
        [TestMethod]
        public async Task SaveMany_ShoulReturnTrue()
        {
            //Arange
            var repo = new Mock<IAnswerRepository>();

            var answers = new List<Answer>() {
                new Answer(){SurveyId=1,AnswerDictId=1,QuestionId=1}
            };

            repo.Setup(arg => arg.SaveManyAsync(It.IsAny<List<Answer>>()))
                    .ReturnsAsync(true);

            var ctr = new AnswerController(repo.Object);

            // Act
            var actionResult = await ctr.SaveMany(answers);
          
            var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;

            //Assert
            var created = okResult.Value.Should()
                .BeAssignableTo<bool>().Subject;
            
            created.Should().Be(true);
        }

        [DataTestMethod]
        [DataRow(1,1,0)]
        [DataRow(1, 0, 1)]
        [DataRow(0, 1, 1)]
        public async Task SaveMany_ShoulThrowBadRequest(int surveyId,int questId,int ansDictId)
        {
            // Arrange
            //Arange
            var repo = new Mock<IAnswerRepository>();

            var answers = new List<Answer>() {
                new Answer(){
                    SurveyId =surveyId,
                    AnswerDictId =questId,
                    QuestionId =ansDictId}
            };

            repo.Setup(arg => arg.SaveManyAsync(It.IsAny<List<Answer>>()))
                    .ReturnsAsync(true);

            var ctr = new AnswerController(repo.Object);

            // Act
            var actionResult = await ctr.SaveMany(answers);

            // Assert
            var badResult = actionResult.Should().BeOfType<BadRequestObjectResult>().Subject;

            badResult.StatusCode.Should().Be(400);

        }
    }
}
