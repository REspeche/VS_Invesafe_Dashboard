mainApp.controller("retirefundsController", retirefundsController);
retirefundsController.$inject = ["$rootScope", "$scope", "mainService", "$filter"];

function retirefundsController($rootScope, $scope, mainService, $filter) {
    $scope.blockPage = false;
    $scope.loadForm = false;
    $scope.accounts = [];
    $scope.filterForm = {
        account: 0,
        concept: '',
        ammount: ''
    };
    $scope.editForm = false;

    /* Retire Founds */

    $scope.loadRetireFunds = function () {
        /* Get Main Alert */
        mainService.ajax('/alert/verifyalert',
            {
                listAle: [4]
            },
            function (data) {
                if (data.code === 0 && data.items.length > 0) {
                    $scope.blockPage = ($filter('filter')(data.items, { state: 1, state: 3 }).length > 0) ? true : false;
                }
                $scope.loadForm = true;
            });

        /* Load Accounts */
        mainService.ajax('/bank/getbankaccountsvalidated',
            undefined,
            function (data) {
                if (data.code === 0) $scope.accounts = angular.copy(data.items);
            });
    };

    $scope.isEditingForm = function () {
        if (!$scope.editForm) $scope.editForm = true;
    }

    $scope.transferExecute = function () {
        if ($scope.filterForm.account > 0 && $scope.filterForm.ammount > 0) {
            if ($scope.filterForm.ammount <= $rootScope.dataHeader.gainamount) {
                $scope.editForm = false;
                mainService.ajax('/bank/retirefunds',
                    {
                        account: $scope.filterForm.account,
                        concept: $scope.filterForm.concept,
                        ammount: $scope.filterForm.ammount
                    },
                    function (data) {
                        if (data.code === 0) {
                            $rootScope.showAlert().notifySuccess(data.message);
                            $scope.filterForm = {
                                account: 0,
                                concept: '',
                                ammount: ''
                            };
                        }
                    });
            }
            else $rootScope.showAlert().notifyWarning($("span.errorAmountMaxRetry").text().format($scope.filterForm.ammount, $rootScope.dataHeader.gainamount));
        }
        else $rootScope.showAlert().notifyWarning($("span.formIncomplete").text());
    }

}
