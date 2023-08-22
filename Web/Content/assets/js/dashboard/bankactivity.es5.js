"use strict";

mainApp.controller("bankactivityController", bankactivityController);
bankactivityController.$inject = ["$rootScope", "$scope", "mainService", "$filter", "$timeout"];

function bankactivityController($rootScope, $scope, mainService, $filter, $timeout) {
    $scope.blockPage = true;
    $scope.typeOperation = [];
    $scope.movements = [];
    $scope.filterForm = {
        typeOperation: 0,
        startDate: null,
        endDate: null
    };
    $scope.loadForm = false;
    $scope.loadList = false;
    $scope.editForm = false;

    $(document).ready(function () {
        // Material Select Initialization   
        $timeout(function () {
            $('[data-toggle="tooltip"]').tooltip();

            // Data Picker Initialization
            $('.datepicker').pickadate(Object.assign({}, _datePickerDefault, { selectYears: 1 }));
        }, 1000);
    });

    /* Bank Activity */

    $scope.loadBankActivity = function () {
        /* Get Main Alert */
        mainService.ajax('/alert/verifyalert', {
            listAle: [2, 3, 4]
        }, function (data) {
            if (data.code === 0 && data.items.length > 0) {
                $scope.blockPage = $filter('filter')(data.items, { state: 1, state: 3 }).length > 0 ? true : false;
            }
        });
        /* Load combos */
        mainService.ajax('/common/getlisttypeoperations', undefined, function (data) {
            if (data.code === 0) $scope.typeOperation = angular.copy(data.items);
            $scope.loadForm = true;
        });
        /* Load Movements */
        mainService.ajax('/bank/getbankmovements', undefined, function (data) {
            if (data.code === 0) $scope.movements = angular.copy(data.items);
            $scope.loadList = true;
        });
    };

    $scope.isEditingForm = function () {
        if (!$scope.editForm) $scope.editForm = true;
    };

    $scope.getDate = function (valueTimeStamp) {
        return UnixTimeStampToDateTime(valueTimeStamp);
    };

    $scope.filterExecute = function () {
        $scope.loadList = false;
        $scope.editForm = false;
        mainService.ajax('/bank/getbankmovements', {
            topId: $scope.filterForm.typeOperation,
            startDate: DateTimeToUnixTimestamp($scope.filterForm.startDate),
            endDate: DateTimeToUnixTimestamp($scope.filterForm.endDate)
        }, function (data) {
            if (data.code === 0) $scope.movements = angular.copy(data.items);
            $rootScope.showAlert().notifySuccess(data.message);
            $scope.loadList = true;
        });
    };
}

