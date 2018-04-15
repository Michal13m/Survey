var Survey = (function () {

    var $newSurveyBtn, $backNewSurveyBtn, $submitNewBtn, $newSurveyForm, $survey
        , $backTakeSurveyBtn, $submitBtn, $surveyForm,ID;

    function postNew(event) {
        event.preventDefault();

        var data = JSON.stringify($newSurveyForm.serializeObject());

        Service.createSurvey(data)
            .done(function (data) {

                alert("Survey added");

                //clear form
                $newSurveyForm[0].reset();
                $('#questions').empty();

                toogleNew();

                EVT.emit("refreshSurveys");
            })
            .fail(function (s) {
                console.log(s);
                alert("Error " + s);
            })
    }



    function showNew() {

        toogleNew();

    }

    function toogleNew() {

        $('.newSurveyBtns').toggleClass('hidden');

        $('#newSurveyForm').toggleClass('hidden');

        EVT.emit('toogleCarousel');
    }


    function takeSurvey(evt) {
        
        ID = $(evt.target).attr("rel").replace(/[^0-9]/g, '');

        Service.getSurveyById(ID)
            .done(function (data) {

                createSurveyHtml(data);

                toogleTakeSurvey();
            })
            .fail(function (s) {
                console.log(s);
                alert("Error " + s);
            });

    }

    function createSurveyHtml(data) {
        $('#surveyName').val(data.name)

        createSurveyQuestionsDiv(data.questions);
    }

    function createSurveyQuestionsDiv(questions) {

        for (let i = 0; i < questions.length; i++) {
            $('#surveyQuestions').append(questionHtml(questions[i],i));
        }

    }



    function questionHtml(question,i) {

        let div = `<div>
                    <input name="answers[`+ i + `][surveyId]" value="` + ID +`"  class="hidden">
                    <input name="answers[`+ i + `][questionId]" value="` + question.id +`"  class="hidden">
                    <label >`+ question.description + ` ? </label>
                    <div class="answers">
                        <div class="radio`+ i +`">
                        <div class="form-check">
                            <label><input type="radio" name="answers[`+ i + `][answerDictId]" value="`
                                    + question.answersDict[0].id + `">`
                                    + question.answersDict[0].description +`</label>
                             </div>
                            <div class="form-check">
                              <label><input type="radio" name="answers[`+ i + `][answerDictId]" value="`
                                    + question.answersDict[1].id + `">`
                                    + question.answersDict[1].description +`</label>
                               </div>
                                <div class="form-check">
                              <label><input type="radio" name="answers[`+ i + `][answerDictId]" value="`
                                    + question.answersDict[2].id + `">`
                                    + question.answersDict[2].description +`</label>
                               </div>
                            </div>
                    </div>
                  <div>`;

        return div;

    }

    function cleanTakeSurvey() {

        $surveyForm[0].reset();
        $('#surveyQuestions').empty();

        toogleTakeSurvey();
    }

    function toogleTakeSurvey() {
        $survey.toggleClass('hidden');
        $('#surveyCarousel').toggleClass('hidden');

        $backTakeSurveyBtn.toggleClass('hidden');
        $newSurveyBtn.toggleClass('hidden');
    }

    function postSurvey(eevent) {
        event.preventDefault();

        var surveyForm =$surveyForm.serializeObject();

        var answers = JSON.stringify(surveyForm.answers)

        Service.saveSurvey(answers)
            .done(function (data) {

                alert('saved');
                cleanTakeSurvey()
            })
            .fail(function (s) {
                console.log(s);
                alert("Error " + s);
            }).
            always(function () {
                console.log('done');
            });

    }

    function init() {
        $newSurveyBtn = $('#newSurveyBtn');
        $backNewSurveyBtn = $('#backNewSurveyBtn');
        $newSurveyForm = $('#newSurveyForm');
        $submitNewBtn = $('#submitNewBtn');
        $survey = $('#survey');
        $surveyForm = $('#surveyForm');
        $submitBtn = $('#submitBtn');
        $backTakeSurveyBtn = $('#backTakeSurveyBtn');

        $backTakeSurveyBtn.on('click', cleanTakeSurvey);

        $newSurveyBtn.on('click', showNew);

        $backNewSurveyBtn.on('click', toogleNew);

        $newSurveyForm.on('submit', postNew);

        $surveyForm.on('submit', postSurvey);

        $('body').on('click', '.survey-btn', takeSurvey);
    }

    EVT.on("init", init);


    return {
        init: init
    };

})();
