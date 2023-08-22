mainApp.controller("entryfundsController", entryfundsController);
entryfundsController.$inject = ["$scope", "mainService", "$filter"];

function entryfundsController($scope, mainService, $filter) {
    $scope.bankAccount = [];
    $scope.referenceCode = undefined;
    $scope.alerts = [];
    $scope.showMessage = {};
    $scope.loadPage = false;
    $scope.loadForm = false;
    $scope.listProjects = [];

    /* Enter Founds */

    $scope.loadEntryFunds = function () {
        /* Load combos */
        mainService.ajax('/project/getlistprojects',
            undefined,
            function (data) {
                if (data.code === 0) $scope.listProjects = angular.copy(data.items);
                $scope.loadForm = true;
            });

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
            });
    };

    $scope.filterExecute = function () {
        $scope.loadList = false;
        $scope.editForm = false;

        /* Load items Marketplace with filter */
        mainService.ajax('/bank/getprojectbankaccount',
            {
                proId: $scope.filterForm.project
            },
            function (data) {
                if (data.code === 0) $scope.bankAccount = angular.copy(data);
                $scope.loadPage = true;
            });
    }

    $scope.isEditingForm = function () {
        if (!$scope.editForm) $scope.editForm = true;
    }

}
