mainApp.controller("authenticationmethodController", authenticationmethodController);
authenticationmethodController.$inject = ["$rootScope", "$scope", "mainService", "$timeout"];

function authenticationmethodController($rootScope, $scope, mainService, $timeout) {

    /* Authentication Method */
    $scope.loadForm = false;
    $scope.showPhone = false;
    $scope.editForm = false;
    $scope.data = {
        codePhone: '',
        phone: ''
    };
    $scope.formCode = {
        imgQR: null,
        code: ''
    };
    $scope.enableTwoFactor = false;
    $scope.enableSMS = false;

    $scope.loadAuthenticationMethod = function () {
        mainService.ajax('/code/enableauthenticationmethod',
            undefined,
            function (data) {
                if (data.code == 0) {
                    $scope.enableTwoFactor = (data.authenticationMethod==1) ? true:false;
                    $scope.enableSMS = (data.authenticationMethod == 2) ? true : false;
                    if ($scope.enableSMS) $scope.showPhone = true;
                    $scope.data.codePhone = data.codePhone;
                    $scope.data.phone = data.phone;
                    $scope.loadForm = true;
                }
            });
    }

    $scope.enableTwoFactorCode = function () {
        mainService.ajax('/code/gettwofactorauthenticator',
            undefined,
            function (data) {
                if (data.code == 0) {
                    $scope.formCode.code = '';
                    $scope.formCode.imgQR = data.qrCodeImageUrl;
                    $("#enableTwoFactor")
                        .on('show.bs.modal', function () {
                            $timeout(function () {
                                $("#txtDigit1_group #digit_1").focus();
                            }, 1000);
                        }).on('hide.bs.modal', function () {
                            $("#txtDigit1_group input").val("");
                        }).modal('show');
                }
            });
    }

    $scope.showVerifySMS = function () {
        if ($scope.data.codePhone == '' || $scope.data.phone == '') {
            $rootScope.showAlert().notifyWarning($("span.formIncomplete").text());
        }
        else {
            /* Send code verification */
            mainService.ajax('/code/VerifyCodeBySMS',
                {
                    codePhone: $scope.data.codePhone,
                    phone: $scope.data.phone
                },
                function (data) {
                    if (data.code === 0) {
                        $scope.formCode.code = '';
                        $("#verifySMS")
                            .on('show.bs.modal', function () {
                                $timeout(function () {
                                    $("#txtDigit2_group #digit_1").focus();
                                }, 1000);
                            }).on('hide.bs.modal', function () {
                                $("#txtDigit2_group input").val("");
                            }).modal('show');
                    }
                });
        }
    }

    $scope.enableAuthenticationTwoFactor = function () {
        if ($scope.formCode.code != '') {
            mainService.ajax('/code/validateCodeTwoFactor',
                {
                    code: $scope.formCode.code
                },
                function (data) {
                    if (data.code == 0) {
                        $scope.enableTwoFactor = true;
                        $scope.enableSMS = false;
                        $rootScope.dataHeader.authenticationMethod = 1;
                        $rootScope.showAuthenticationMethod = false;
                        $rootScope.showAlert().notifySuccess(data.message);
                        $("#enableTwoFactor").modal('hide');
                        $scope.showPhone = false;
                    }
                });
        }
        else $rootScope.showAlert().notifyWarning($("span.codeFail").text());
    }

    $scope.enableAuthenticationSMS = function () {
        if ($scope.formCode.code != '') {
            /* Validation code verification */
            mainService.ajax('/code/EnabledSMSAuthenticator',
                {
                    code: $scope.formCode.code
                },
                function (data) {
                    if (data.code === 0) {
                        $scope.enableTwoFactor = false;
                        $scope.enableSMS = true;
                        $rootScope.dataHeader.authenticationMethod = 2;
                        $rootScope.showAuthenticationMethod = false;
                        $rootScope.showAlert().notifySuccess(data.message);
                        $("#verifySMS").modal('hide');
                    }
                });
        }
        else $rootScope.showAlert().notifyWarning($("span.codeFail").text());
    }

    $scope.wannaPhone = function () {
        $scope.showPhone = ($scope.showPhone) ? false : true;
    }

    $scope.removeMethod = function () {
        $rootScope.confirmMessage(function () {
            mainService.ajax('/code/CancelAuthenticatorMethod',
                undefined,
                function (data) {
                    if (data.code === 0) {
                        $scope.enableTwoFactor = false;
                        $scope.enableSMS = false;
                        $rootScope.dataHeader.authenticationMethod = 0;
                        $rootScope.showAlert().notifySuccess(data.message);
                    }
                });
        });
    }
}
