mainApp.controller("activeaccountController", activeaccountController);
activeaccountController.$inject = ["$scope", "$rootScope", "mainService"];

function activeaccountController($scope, $rootScope, mainService) {

    /* Active Account */

    $scope.sendEmail = function () {
        mainService.ajax('/account/sendemailactiveaccount',
            undefined,
            function (data) {
                if (data.code === 0) {
                    $rootScope.showAlert().notifySuccess(data.message);
                }
            });
    }

}
