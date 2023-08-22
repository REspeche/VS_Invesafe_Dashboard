mainApp.controller("homeController", homeController);
homeController.$inject = ["$scope", "mainService", "$filter"];

function homeController($scope, mainService, $filter) {
    $scope.alerts = [];
    $scope.showMessage = {};
    $scope.loadPage = false;
    $scope.ammounts = {};
    $scope.percents = {};

    /* Home */
    $scope.loadHome = function () {
        /* Get Alerts */
        mainService.ajax('/alert/getalerts',
            undefined,
            function (data) {
                if (data.code === 0) {
                    $scope.alerts = angular.copy(data.items);
                    $scope.showMessage = {
                        pending: ($filter('filter')($scope.alerts, { state: 1 }).length > 0) ? true : false,
                        validating: ($filter('filter')($scope.alerts, { state: 2 }).length > 0) ? true : false,
                        rejected: ($filter('filter')($scope.alerts, { state: 3 }).length > 0) ? true : false
                    };
                }
                $scope.loadPage = true;
            });

        /* Get Balance Account */
        mainService.ajax('/bank/getpatrimonialsituation',
            {
                returnFull: 0
            },
            function (data) {
                if (data.code === 0) {
                    $scope.ammounts = angular.copy(data.item);
                    $scope.percents = {
                        patrimony: Math.floor($scope.ammounts.available * 100 / $scope.ammounts.total),
                        benefit: Math.floor($scope.ammounts.benefit * 100 / $scope.ammounts.benefitTotal)
                    }
                }
            });
    }

}
