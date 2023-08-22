mainApp.controller("changepasswordController", changepasswordController);
changepasswordController.$inject = ["$rootScope", "$scope", "mainService"];

function changepasswordController($rootScope, $scope, mainService) {

    /* Change Password */

    $scope.loadChangePassword = function () {
        $scope.passNewR = '';
        $scope.dataCP = {
            passOld: '',
            passNew: ''
        };
    }

    $scope.submitChangePass = function () {
        if ($scope.dataCP.passNew != $scope.passNewR) {
            $rootScope.showAlert().notifyWarning($("span.errorPasswordDiferent").text());
            return false;
        }

        if (!$scope.frmChangePass.$invalid) {
            mainService.ajax('/account/ChangePassword',
                $scope.dataCP,
                function (data) {
                    switch (data.code) {
                        case 0:
                            $rootScope.showAlert().notifySuccess(data.message);
                            $scope.clickSlideAjax('changepassword');
                            break;
                    }
                });
        }
        else $rootScope.showAlert().notifyWarning($("span.formIncomplete").text());
    }
}
