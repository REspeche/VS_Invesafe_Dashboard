mainApp.controller("marketplaceController", marketplaceController);
marketplaceController.$inject = ["$rootScope", "$scope", "mainService", "$filter"];

function marketplaceController($rootScope, $scope, mainService, $filter) {
    $scope.filterForm = {
        project: 0,
        typeProject: 0
    };
    $scope.agree = false;
    $scope.listProjects = [];
    $scope.typeProject = [];
    $scope.marketplace = [];
    $scope.invest = [];
    $scope.loadForm = false;
    $scope.loadList = false;
    $scope.loadPage = false;
    $scope.editForm = false;
    $scope.alerts = [];
    $scope.showMessage = {};

    /* Market Place */

    $scope.loadMarketPlace = function () {
        /* Get Main Alert */
        mainService.ajax('/alert/verifyalert',
            {
                listAle: [3, 4]
            },
            function (data) {
                if (data.code === 0) {
                    $scope.alerts = angular.copy(data.items);
                    $scope.showMessage = {
                        pending: ($filter('filter')($scope.alerts, { state: 1 }).length > 0) ? true : false,
                        validating: ($filter('filter')($scope.alerts, { state: 2 }).length > 0) ? true : false,
                        rejected: ($filter('filter')($scope.alerts, { state: 3 }).length > 0) ? true : false
                    };
                }

                /* Load combos */
                mainService.ajax('/common/getlisttypeprojects',
                    undefined,
                    function (data) {
                        if (data.code === 0) $scope.typeProject = angular.copy(data.items);
                    });
                mainService.ajax('/project/getlistprojects',
                    undefined,
                    function (data) {
                        if (data.code === 0) $scope.listProjects = angular.copy(data.items);
                        $scope.loadForm = true;
                    });

                /* Load items Marketplace */
                mainService.ajax('/investment/getmarketplace',
                    undefined,
                    function (data) {
                        if (data.code === 0) $scope.marketplace = angular.copy(data.items);
                        $scope.loadList = true;
                    });
            });
    }

    $scope.isEditingForm = function () {
        if (!$scope.editForm) $scope.editForm = true;
    }

    $scope.filterExecute = function () {
        $scope.loadList = false;
        $scope.editForm = false;

        /* Load items Marketplace with filter */
        mainService.ajax('/investment/getmarketplace',
            {
                project: $scope.filterForm.project,
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

    $scope.getInvest = function (investItem) {
        if ($scope.showMessage.pending && !$scope.showMessage.validating && !$scope.showMessage.rejected) {
            $rootScope.showAlert().notifyWarning($('div.alert-warning').text());
        }
        else {
            sessionStorage.setItem("invest", JSON.stringify({
                id: investItem.id
            }));
            location.href = "/investment/buyinvest";
        }
    }

    /* Buy Sale */

    $scope.loadBuyInvest = function () {
        /* Load project */
        var objInv = JSON.parse(sessionStorage.getItem("invest"));
        mainService.ajax('/investment/getsaleinvest',
            {
                sinId: objInv.id
            },
            function (data) {
                if (data.code === 0) {
                    $scope.invest = data;
                    $scope.loadPage = true;
                }
            });
    }

    $scope.confirmBuyInvest = function () {
        if ($scope.agree) {
            sessionStorage.setItem("project", JSON.stringify({
                id: $scope.invest.idPro
            }));
            /* Edit or Save Invest To Sale */
            mainService.ajax('/investment/savebuyinvest',
                {
                    sinId: $scope.invest.idSin,
                    amount: $scope.invest.amountSin,
                    agree: ($scope.agree) ? 1 : 0
                },
                function (data) {
                    if (data.code === 0) {
                        $rootScope.showAlert().notifySuccess(data.message);
                        location.href = "/project/entryfunds";
                    }
                });
        }
        else $rootScope.showAlert().notifyWarning($("span.aceptAgree").text());
    }
}
