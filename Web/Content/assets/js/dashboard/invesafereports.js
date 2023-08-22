mainApp.controller("invesafereportsController", invesafereportsController);
invesafereportsController.$inject = ["$scope", "mainService"];

function invesafereportsController($scope, mainService) {
    $scope.reports = [];
    $scope.loadList = false;

    /* Invesafe Reports */

    $scope.loadInvesafeReports = function () {
        /* Get Reference Code */
        mainService.ajax('/information/getreports',
            undefined,
            function (data) {
                if (data.code === 0) {
                    $scope.reports = angular.copy(data.items);
                    angular.forEach($scope.reports, function (report, key) {
                        report.title_lang = JSON.parse(report.title)[lang];
                        report.file_lang = lang + '_' + report.file; 
                    });
                }
                $scope.loadList = true;
            });
    };

}
