mainApp.controller("loginController", loginController);
loginController.$inject = ["$rootScope", "$scope", "$cookies", "mainService"];

function loginController($rootScope, $scope, $cookies, mainService) {

    /* Login */

    $scope.loadLogin = function () {
        $scope.querystring = {
            urlback: getQueryStringValue('urlback'),
            email: getQueryStringValue('email'),
            endSession: getQueryStringValue('endSession'),
            endToken: getQueryStringValue('endToken')
        };

        // init vars
        $scope.passNewR = '';
        $scope.dataL = {
            email: "",
            password: ""
        }; //login
        $scope.dataR_person = {
            firstname: "",
            lastname: "",
            email: "",
            password: "",
            passwordRep: "",
            idFacebook: ""
        }; //register
        $scope.dataP = {
            email: ""
        }; //send mail password reset
        $scope.dataRP = {
            passNew: '',
            hash: getUrlVars()['c']
        }; //reset password

        //load email
        if ($scope.querystring.email != "") {
            $scope.dataL.email = $scope.querystring.email;
            $scope.dataP.email = $scope.querystring.email;
        }

        //load popup end session
        if ($scope.querystring.endSession == '1' || $scope.querystring.endToken == '1') {
            $('#modalInfoEndSession').modal('show').on('hidden.bs.modal', function () {
                setHash('/account/login');
            });
        }
    };

    $scope.goToDashboard = function () {
        $rootScope.transitionPage = true;
        if ($scope.querystring && $scope.querystring.urlback) executeActionMensage(decodeURIComponent($scope.querystring.urlback));
        else executeActionMensage(2); //go to home
    }

    $scope.submitSendResetMail = function () {
        if (!$scope.frmPassword.$invalid) {
            mainService.ajax('/account/resetpasswordsendMail',
                $scope.dataP,
                function (data) {
                    if (data.code == 0) {
                        $scope.dataP = {
                            email: ""
                        };
                        $rootScope.showAlert().notifyInfo(data.message);
                        setTimeout(function () {
                            $scope.$applyAsync(function () {
                                $scope.goToLogin();
                            });
                        }, 2000);
                    }
                });
        }
        else $rootScope.showAlert().notifyWarning($("span.formIncomplete").text());
    };

    $scope.submitResetPass = function () {
        if ($scope.dataRP.passNew != $scope.passNewR) {
            $rootScope.showAlert().notifyWarning($("span.errorPasswordDiferent").text());
            return false;
        }

        if (!$scope.frmResetPass.$invalid) {
            mainService.ajax('/account/resetpasswordnewpass',
                $scope.dataRP,
                function (data) {
                    switch (data.code) {
                        case 0:
                            $scope.dataRP.passNew = '';
                            $scope.passNewR = '';
                            $rootScope.showAlert().notifySuccess(data.message);
                            setTimeout(function () {
                                $scope.$applyAsync(function () {
                                    $scope.goToLogin();
                                });
                            }, 2000);
                            break;
                    }
                });
        }
        else $rootScope.showAlert().notifyWarning($("span.formIncomplete").text());
    };

    $scope.submitLoginKeyPress = function () {
        if (frmLogin.$valid) frmLogin.submit();
    };

    $scope.submitLogin = function () {
        if (!$scope.frmLogin.$invalid) {
            $scope.loginCall($scope.dataL, false);
        }
        else $rootScope.showAlert().notifyWarning($("span.formIncomplete").text());
    };

    $scope.submitRegister = function () {
        if (!$scope.frmRegisterPerson.$invalid) {
            if ($scope.dataR_person.password == $scope.dataR_person.passwordRep) {
                mainService.ajax('/account/registerPerson',
                    $scope.dataR_person,
                    function (data) {
                        if (data.code == 0) {
                            $scope.goToDashboard();
                        }
                    });
            }
            else {
                $scope.dataR_person.password = "";
                $scope.dataR_person.passwordRep = "";
                $rootScope.showAlert().notifyWarning($("span.PassDiferent").text());
            }
        }
        else $rootScope.showAlert().notifyWarning($("span.formIncomplete").text());
    };

    $rootScope.submitRegisterFB = function (data) {
        mainService.ajax('/account/registerPerson',
            {
                firstname: data.firstName,
                lastname: data.lastName,
                email: data.email,
                password: data.id,
                idFacebook: data.id,
                gender: data.gender,
                birthday: data.birthday,
                location: data.location
            },
            function (data) {
                if (data.code == 0) {
                    $scope.goToDashboard();
                }
            });
    };

    $rootScope.loginCall = function (data, isFacebook) {
        if (isFacebook) {
            var expireDate = new Date();
            expireDate.setDate(expireDate.getDate() + 30);
            $cookies.put('FBid', data.id, { 'expires': expireDate });
            $cookies.put('FBemail', data.email, { 'expires': expireDate });
        }
        mainService.ajax('/account/login' + ((isFacebook) ? 'facebook' : ''),
            data,
            function (data) {
                switch (data.code) {
                    case 0:
                        $scope.goToDashboard();
                        break;
                }
            });
    };

    // Login / Register and Reset Password

    $scope.goToLogin = function () {
        executeActionMensage(1);
    };

    $scope.goToSignup = function () {
        executeActionMensage(3);
    };

    $scope.goToForgot = function () {
        if ($scope.dataL.email != "") {
            $scope.dataL.password = "";
            executeActionMensage("/account/forgot?email=" + $scope.dataL.email);
        }
        else executeActionMensage(4);
    };

    $scope.goToSite = function () {
        executeActionMensage(5);
    };
}

//Facebook Functions
window.fbAsyncInit = function () {
    FB.init({
        appId: '1063423837149407',
        xfbml: true,
        version: 'v2.7'
    });
};

// Load the SDK asynchronously
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/es_LA/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

function statusChangeCallback(response, signup) {
    if (response.status === 'connected') {
        if (signup == 1) autoSignUp();
        else autoLogin();
    }
}

function loginFacebook(signup) {
    FB.login(function (response) {
        statusChangeCallback(response, signup);
    }, { scope: 'public_profile,email,user_birthday,user_location' });
}

function autoLogin() {
    FB.api('/me?fields=id,email,name,first_name,last_name,gender,location,birthday', function (response) {
        var elem = angular.element(document.querySelector('[ng-app]'));
        var injector = elem.injector();
        var $rootScope = injector.get('$rootScope');
        if (response.email && response.id) {
            $rootScope.$apply(function () {
                $rootScope.loginCall(
                            {
                                email: response.email,
                                id: response.id,
                                firstName: response.first_name,
                                lastName: response.last_name,
                                gender: response.gender,
                                birthday: (response.birthday) ? response.birthday : "",
                                location: (response.location) ? response.location.name : ""
                            }, true);
            });
        }
    });
}

function autoSignUp() {
    FB.api('/me?fields=id,email,name,first_name,last_name,gender,location,birthday', function (response) {
        var elem = angular.element(document.querySelector('[ng-app]'));
        var injector = elem.injector();
        var $rootScope = injector.get('$rootScope');
        if (response.email && response.id) {
            $rootScope.$apply(function () {
                $rootScope.submitRegisterFB(
                            {
                                email: response.email,
                                id: response.id,
                                firstName: response.first_name,
                                lastName: response.last_name,
                                gender: response.gender,
                                birthday: (response.birthday) ? response.birthday : "",
                                location: (response.location) ? response.location.name : ""
                            });
            });
        }
    });
}