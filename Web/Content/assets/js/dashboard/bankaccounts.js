mainApp.controller("bankaccountsController", bankaccountsController);
bankaccountsController.$inject = ["$rootScope", "$scope", "mainService", "$timeout", "$filter"];

function bankaccountsController($rootScope, $scope, mainService, $timeout, $filter) {
    $scope.bankAccount = {
        tacId: 0,
        holder: '',
        accountNbr: '',
        bankName: '',
        cbu: '',
        documentFile: undefined
    };
    $scope.blockPage = true;
    $scope.loadForm = false;
    $scope.editForm = false;
    $scope.typeAccount = [];
    $scope.accounts = [];
    $scope.fileDocument = undefined;

    $(document).ready(function () {
        // Material Select Initialization    
        $timeout(function () {
            $('[data-toggle="tooltip"]').tooltip();
        }, 1000);
    });

    /* Bank Accounts */

    $scope.loadBankAccounts = function () {
        /* Get Main Alert */
        mainService.ajax('/alert/verifyalert',
            {
                listAle: [2, 3]
            },
            function (data) {
                if (data.code === 0 && data.items.length > 0) {
                    $scope.blockPage = ($filter('filter')(data.items, { state: 1, state: 3 }).length > 0) ? true : false;
                }
                $scope.loadForm = true;
            });

        /* Load Accounts */
        mainService.ajax('/bank/getbankaccounts',
            undefined,
            function (data) {
                if (data.code === 0) $scope.accounts = angular.copy(data.items);
            });
    }

    $scope.loadAddAccount = function () {
        /* Load combos */
        mainService.ajax('/common/getlisttypeaccounts',
            undefined,
            function (data) {
                if (data.code === 0) $scope.typeAccount = angular.copy(data.items);
                $scope.loadForm = true;
            });
    }

    $scope.openAddAccount = function () {
        location.href = "/bank/addaccount";
    }

    $scope.submitBankAccount = function () {
        if (!$scope.frmBankAccount.$invalid && $scope.fileDocument) {
            var files = [];
            files.push($scope.fileDocument);
            mainService.fileUpload('/bank/saveaddaccount',
                files,
                $scope.bankAccount,
                function (data) {
                    if (data.code === 0) {
                        $scope.clickSlideAjax('bankaccounts');
                        $rootScope.showAlert().notifySuccess(data.message);
                    }
                });
        }
        else $rootScope.showAlert().notifyWarning($("span.formIncomplete").text());
    }

    $scope.isEditingForm = function () {
        if (!$scope.editForm) $scope.editForm = true;
    }

    $scope.removeAccount = function (i) {
        $rootScope.confirmMessage(function () {
            mainService.ajax('/bank/removeaccount',
                {
                    banId: $scope.accounts[i].id
                },
                function (data) {
                    if (data.code === 0) {
                        $scope.accounts.splice(i, 1);
                        $rootScope.showAlert().notifySuccess(data.message);
                    }
                });
        });
    }
}
