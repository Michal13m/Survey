var Stats = (function () {

    var $statsContainer, $surveyConteiner, $backStatsBtn, $chartConteiner;

    function showStats(evt) {
        ID = $(evt.target).attr("rel").replace(/[^0-9]/g, '');

        Service.getStatsBySurveyId(ID)
            .done(function (data) {

                createChart(data);

                console.log(data);
                tooggleStatsVis();

            }).fail(function (s) {
                console.log(s);
                alert("Error " + s);
            }).
            always(function () {
                console.log('stats loaded');
            });


    }

    function createChart(data) {
        // Load the Visualization API and the corechart package.
        function drCallBack() {
            drawChart(data);
        };

        google.charts.load('current', { packages: ['corechart', 'bar'] });

        // Set a callback to run when the Google Visualization API is loaded.
        google.charts.setOnLoadCallback(drCallBack);


    }

    function drawChart(dataJ) {

        $chartConteiner.empty();

        var uniqueQuest = [...new Set(dataJ.map(item => item.question))];

        for (var i = 0; i < uniqueQuest.length; i++) {

            var answers = _.where(dataJ, { question: uniqueQuest[i] });

            var chartRows = answers.map(function (a) { var ar = [a['answer'], a['answerCount']]; return ar })

            //add header for chart
            chartRows.unshift(["answers", "count"]);

            // Create the data table.
            var data = new google.visualization.arrayToDataTable(chartRows);

            var materialOptions = {
                chart: {
                    title: uniqueQuest[i],
                    subtitle: 'foo bar'
                },
                hAxis: {
                    title: 'Total Population',
                    minValue: 0,
                },
                vAxis: {
                    title: 'Survey'
                },
                bars: 'vertical'
            };

            var chartDiv = `<div id="chart`+i+`"> </div>`;
                
            $chartConteiner.append(chartDiv);

            var materialChart = new google.charts.Bar(document.getElementById('chart'+i));
            materialChart.draw(data, materialOptions);
        }
    }

    function tooggleStatsVis() {
        $statsContainer.toggleClass('hidden');
        $surveyConteiner.toggleClass('hidden');
    }

    function init() {
        $statsContainer = $('#statsContainer');
        $chartConteiner = $('#chartConteiner');
        $surveyConteiner = $('#surveyConteiner');
        $backStatsBtn = $('#backStatsBtn');

        $backStatsBtn.on('click', tooggleStatsVis);

        $('body').on('click', '.stat-btn', showStats);
    }

    EVT.on("init", init);


    return {
        init: init
    };

})();