var Carousel = (function () {

    var $surveyBtns, $items,caruselItems=[];

    function newItems(oldArr, newArr) {
        var retArr = [];

        for (let i = 0; i < newArr.length; i++) {
            let isInArray = _.where(oldArr, { id: newArr[i].id });
            if (isInArray.length===0) {
                retArr.push(newArr[i]);
                caruselItems.push(newArr[i]);
            }

        }

        return retArr;
    }

    function refreshSurveys() {
        Service.getSurveys().done(function (data) {

            var oldLength = caruselItems.length;

            data = newItems(caruselItems,data)

            var $sCarousel = $('#surveyCarousel > ol')
            var $caruselInner = $('#surveyCarousel > .carousel-inner');

            for (let i = 0; i < data.length; i++) {

                let activate = i + oldLength === 0 ? 'active' : '';

                $sCarousel.append(caruselCreateLi(i + oldLength, activate));

                $caruselInner.append(caruselCreateInnerDiv(data[i].name, activate,data[i].id));
            }
        })
        .fail(x => console.log(x));
    }

    function caruselCreateLi(i, activate) {
        return '<li data-target="#surveyCarousel" data-slide-to=' + i + '" class="' + activate + ' slide"></li>';
    }

    function caruselCreateInnerDiv(name, activate,id) {
        return ` <div class="text-center item ` + activate + ` caruselItem">
                        <h3>`+ name + ` </h3>
                        <br/>
                    <button type="button" class="btn btn-info survey-btn"  rel="survey-` + id +`">Take</button>
                       <br/>
                    <button type="button" class="btn btn-warning stat-btn"  rel="survey-` + id +`">Stats</button>
                 </div >`;
    }

 

    function toogleCarousel() {
        $('#surveyCarousel').toggleClass('hidden');
    }

    function init() {

        refreshSurveys();

        EVT.on("toogleCarousel", toogleCarousel);

        EVT.on("refreshSurveys", refreshSurveys)
    }

    EVT.on("init", init);

    return {
        init: init
    };

})();
