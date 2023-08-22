mainApp.controller("supportController", supportController);
supportController.$inject = ["$scope", "$rootScope"];

function supportController($scope, $rootScope) {
    $scope.support = {
        query: ''
    };

    /* Fiscal Reports */

    $scope.loadSupport = function () {

    }

    $scope.submitSupport = function () {
        if ($scope.support.query) {

        }
        else $rootScope.showAlert().notifyWarning($("span.formIncomplete").text());
    }

}
