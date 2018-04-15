var Service = (function () {

    var getSurveys = function () {

        return $.ajax(
            "api/Survey"
            , { contentType: "application / json; charset = utf - 8" });
    };


    var createSurvey = function (data) {

        return $.ajax({
            type: "POST",
            contentType: "application / json; charset = utf - 8",
            url: "api/Survey/Create",
            data: data,
        });

    };

    var getSurveyById = function (id) {
        return $.ajax(
            "api/Survey/GetById?="+id
            , { contentType: "application / json; charset = utf - 8" });
    };

    var saveSurvey = function (answers) {
        return $.ajax({
            type: "POST",
            contentType: "application / json; charset = utf - 8",
            url: "api/Answer/SaveMany",
            data: answers
        });
    };

    var getStatsBySurveyId = function (id) {
        return $.ajax(
            "api/Answer/GetAnsersBySurveyId?=" + id
            , { contentType: "application / json; charset = utf - 8" });
    };
        
    function init() {
        getSurveys;
    }

    EVT.on("init", init);

    return {
        init: init,
        getSurveys: getSurveys,
        createSurvey: createSurvey,
        getSurveyById: getSurveyById,
        saveSurvey: saveSurvey,
        getStatsBySurveyId: getStatsBySurveyId
    };

})();