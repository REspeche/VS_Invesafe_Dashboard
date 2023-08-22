mainApp.controller("typeaccountController", typeaccountController);
typeaccountController.$inject = ["$rootScope", "$scope", "$filter", "mainService"];

function typeaccountController($rootScope, $scope, $filter, mainService) {
    $scope.alerts = [];
    $scope.showMessage = {};
    $scope.loadPage = false;
    $scope.dataForm = {
        typeInvest: 0,
        question1: 0,
        question2: 0,
        agree: false
    };
    $scope.enableSubmit = true;

    /* Type Account */

    $scope.loadTypeAccount = function () {
        /* Get Main Alert */
        mainService.ajax('/alert/verifyalert',
            {
                listAle: [1, 2, 3]
            },
            function (data) {
                if (data.code === 0) {
                    $scope.alerts = angular.copy(data.items);
                    $scope.showMessage = {
                        validating: ($filter('filter')($scope.alerts, { state: 2 }).length > 0) ? true : false
                    };
                }
                $scope.loadPage = true;
            });

        /* Get Type Inverter */
        mainService.ajax('/investment/gettypeinverter',
            undefined,
            function (data) {
                if (data.code === 0) {
                    $scope.dataForm = {
                        typeInvest: data.typeInvest,
                        question1: data.question1,
                        question2: data.question2,
                        agree: data.agree
                    };
                    if ($scope.dataForm.question1 > 0 && $scope.dataForm.question2 > 0) $scope.enableSubmit = false;
                }
            });
    }

    $scope.enableSaveForm = function () {
        if ($scope.dataForm.question1 > 0 && $scope.dataForm.question2 > 0) $scope.enableSubmit = false;
        else $scope.enableSubmit = true;
    }

    $scope.selectOption = function (typeInvest) {
        if (typeInvest == 2 && !$scope.dataForm.agree) {
            $rootScope.showAlert().notifyWarning($("span.aceptAgree").text());
            return false;
        }
        $scope.dataForm.typeInvest = typeInvest;
        mainService.ajax('/investment/savetypeinverter',
            $scope.dataForm,
            function (data) {
                if (data.code === 0) $rootScope.showAlert().notifySuccess(data.message);
            });
        if (typeInvest == 1) $('#modalInfoInversorNoAcreditado').modal('hide');
        else $('#modalInfoInversorAcreditado').modal('hide');
    }

}
