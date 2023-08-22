mainApp.controller("patrimonialsituationController", patrimonialsituationController);
patrimonialsituationController.$inject = ["$scope", "mainService", "$filter", "$timeout"];

function patrimonialsituationController($scope, mainService, $filter, $timeout) {
    $scope.blockPage = true;
    $scope.viewChart = false;
    $scope.ammounts = {};

    /* Patrimonial Situation */

    $scope.loadPatrimonialSituation = function () {
        /* Get Main Alert */
        mainService.ajax('/alert/verifyalert',
            {
                listAle: [2]
            },
            function (data) {
                if (data.code === 0 && data.items.length > 0) {
                    $scope.blockPage = ($filter('filter')(data.items, { state: 1, state: 3 }).length > 0) ? true : false;
                }
            });

        /* Get Balance Account */
        mainService.ajax('/bank/getpatrimonialsituation',
            {
                returnFull: 1
            },
            function (data) {
                if (data.code === 0) {
                    $scope.ammounts = angular.copy(data.item);
                    if ($scope.ammounts.chartValues != null) {
                        $timeout(function () {
                            renderChart($scope.ammounts.chartValues);
                            $scope.viewChart = true;
                        }, 1000);
                    }
                }
            });
    }

    var renderChart = function (pValues) {
        $(function () {
            var arrValues = pValues.split(",");
            var arrLabels = [];
            var arrData = [];
            for (var t = arrValues.length-1; t >= 0; t--) {
                var arrItem = arrValues[t].split("|");
                arrLabels.push(_datePickerDefault.monthsShort[arrItem[1]-1] + ' ' + arrItem[0]);
                arrData.push(arrItem[2]);
            }
            var dataOneLine = {
                labels: arrLabels,
                datasets: [{
                    label: "Beneficio Neto",
                    fillColor: "rgba(220,220,220,0.2)",
                    strokeColor: "rgba(220,220,220,1)",
                    pointColor: "rgba(220,220,220,1)",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(0,0,0,.15)",
                    data: arrData,
                    backgroundColor: "#4CAF50"
                }]
            };

            var option = {
                responsive: true,
                maintainAspectRatio: false,
                // set font color
                scaleFontColor: "#fff",
                // background grid lines color
                scaleGridLineColor: "rgba(255,255,255,.1)",
                // hide vertical lines
                scaleShowVerticalLines: false,
            };

            // Get the context of the canvas element we want to select
            var ctx = document.getElementById("mychart").getContext('2d');
            var myLineChart = new Chart(ctx).Line(dataOneLine, option); //'Line' defines type of the chart.
        });
    }

}
