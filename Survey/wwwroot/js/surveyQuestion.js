var SurveyQuestion = (function () {

    var $newQuestionBtn, $newSurveyQuestions;

    function showNew() {
       var questionCount=  $('.question').length || 0;

       $newSurveyQuestions.append(addNewQuestionDiv(questionCount));
    };


    function addNewQuestionDiv(count) {
        return `
                    <label >Description: </label>
                    <input class="form-control question" name="questions[`+count+`][description]"/>
                    <div id='answers'>
                       <label >Answers: </label>
                       <label >A. </label>
                       <input class="form-control" name="questions[`+ count +`][answersDict][0][description]"/>
                       <label >B. </label>
                       <input class="form-control" name="questions[`+ count +`][answersDict][1][description]"/>
                       <label >C. </label>
                       <input class="form-control" name="questions[`+ count +`][answersDict][2][description]"/>
                    </div>
               `;
    };

    function init() {

        $newSurveyQuestions = $('#newSurveyForm > #questions');

        $newQuestionBtn = $('#newQuestionBtn');

        $newQuestionBtn.on('click', showNew)

    }

    EVT.on("init", init);


    return {
        init: init
    };


})();