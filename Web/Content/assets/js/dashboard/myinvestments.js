mainApp.controller("myinvestmentsController", myinvestmentsController);
myinvestmentsController.$inject = ["$rootScope", "$scope", "mainService", "$timeout"];

function myinvestmentsController($rootScope, $scope, mainService, $timeout) {
    $scope.blockPage = true;
    $scope.invest = [];
    $scope.typeProject = [];
    $scope.myinvestments = [];
    $scope.filterForm = {
        typeProject: 0,
        search: ''
    }
    $scope.loadForm = false;
    $scope.loadList = false;
    $scope.editForm = false;
    $scope.fractionToSale = 0;
    $scope.fractionValue = 1;
    $scope.loadPage = false;

    $(document).ready(function () {
        // Material Select Initialization    
        $timeout(function () {
            $('[data-toggle="tooltip"]').tooltip();
        }, 1000);
    });

    /* My Investments */

    $scope.loadMyInvestments = function () {
        /* Load combos */
        mainService.ajax('/common/getlisttypeprojects',
            undefined,
            function (data) {
                if (data.code === 0) $scope.typeProject = angular.copy(data.items);
                $scope.loadForm = true;
            });
        /* Load My Investments */
        mainService.ajax('/investment/getmyinvestments',
            undefined,
            function (data) {
                if (data.code === 0) $scope.myinvestments = angular.copy(data.items);
                $scope.loadList = true;
            });
    }

    $scope.isEditingForm = function () {
        if (!$scope.editForm) $scope.editForm = true;
    }

    $scope.filterExecute = function () {
        $scope.loadList = false;
        $scope.editForm = false;

        /* Load My Investments with filter */
        mainService.ajax('/investment/getmyinvestments',
            {
                search: $scope.filterForm.search,
                typeProject: $scope.filterForm.typeProject
            },
            function (data) {
                if (data.code === 0) $scope.myinvestments = angular.copy(data.items);
                $scope.loadList = true;
            });
    }

    $scope.getDate = function (valueTimeStamp) {
        return UnixTimeStampToDateTime(valueTimeStamp);
    }

    $scope.saleInvest = function (investItem) {
        sessionStorage.setItem("invest", JSON.stringify({
            id: investItem.id
        }));
        location.href = "/investment/sale";
    }

    /* Sale Invest --------------------- */

    $scope.loadSaleInvest = function () {
        /* Load project */
        var objInv = JSON.parse(sessionStorage.getItem("invest"));
        mainService.ajax('/investment/getmyinvest',
            {
                pxcId: objInv.id
            },
            function (data) {
                if (data.code === 0) {
                    $scope.invest = data;
                    $scope.fractionToSale = $scope.invest.amountSin;
                    $scope.fractionValue = angular.copy(data.fractionValue);
                    $scope.loadPage = true;
                }
            });
    }

    $scope.confirmSale = function () {
        if ($scope.formInvest.agree) {
            /* Edit or Save Invest To Sale */
            mainService.ajax('/investment/saveinvesttosale',
                {
                    pxcId: $scope.invest.id,
                    amount: $scope.fractionToSale,
                    fraction: parseFloat($scope.fractionValue).toFixed(2),
                    agree: ($scope.formInvest.agree) ? 1 : 0
                },
                function (data) {
                    if (data.code === 0) {
                        $rootScope.showAlert().notifySuccess(data.message);
                        $scope.clickSlideAjax('myinvestments');
                    }
                });
        }
        else $rootScope.showAlert().notifyWarning($("span.aceptAgree").text());
    }

    $scope.removeSale = function () {
        $rootScope.confirmMessage(function () {
            /* Remove Invest To Sale */
            mainService.ajax('/investment/removeinvesttosale',
                {
                    pxcId: $scope.invest.id,
                    sinId: $scope.invest.idSil
                },
                function (data) {
                    if (data.code === 0) {
                        $rootScope.showAlert().notifySuccess(data.message);
                        $scope.clickSlideAjax('myinvestments');
                    }
                });
        });
    }
}
