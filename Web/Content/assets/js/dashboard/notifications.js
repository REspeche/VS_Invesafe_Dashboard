mainApp.controller("notificationsController", notificationsController);
notificationsController.$inject = ["$rootScope", "$scope", "mainService"];

function notificationsController($rootScope, $scope, mainService) {
    $scope.notifications = [];
    $scope.editForm = false;
    $scope.loadForm = false;

    /* Notifications */

    $scope.loadNotifications = function () {
        /* Load Notifications */
        mainService.ajax('/notification/getnotifications',
            undefined,
            function (data) {
                if (data.code === 0) $scope.notifications = angular.copy(data.items);
                $scope.loadForm = true;
            });
    }

    $scope.isEditingForm = function () {
        if (!$scope.editForm) $scope.editForm = true;
    }

    $scope.submitNotifications = function () {
        mainService.ajax('/notification/savenotifications',
            {
                data: JSON.stringify($scope.notifications, function (key, value) {
                    if (key === "$$hashKey") {
                        return undefined;
                    }
                    return value;
                })
            },
            function (data) {
                if (data.code === 0) {
                    $rootScope.showAlert().notifySuccess(data.message);
                }
            });
    }
}
