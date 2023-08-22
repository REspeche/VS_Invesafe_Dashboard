mainApp.controller("dashboardCtrl", dashboardCtrl);
dashboardCtrl.$inject = ["$rootScope", "$scope", "$cookies", "$compile", "mainService", "$filter", "$location"];

function dashboardCtrl($rootScope, $scope, $cookies, $compile, mainService, $filter, $location) {
    $rootScope.dataHeader = {
        balanceamount: undefined,
        gainamount: undefined,
        authenticationMethod: 0
    }
    $rootScope.showActiveAccount = false;
    $rootScope.showAuthenticationMethod = false;

    angular.element(document).ready(function () {
        // Sidenav Initialization
        $(".button-collapse").sideNav();

        // Tooltips Initialization
        $('[data-toggle="tooltip"]').tooltip();
    });

    $scope.loadDashboard = function () {
        $scope.selectOptionMenu(getAction()); //select default option
        if ($rootScope.session.id) {
            /* Get Balance Account */
            mainService.ajax('/bank/getbalanceaccount',
                undefined,
                function (data) {
                    if (data.code === 0) {
                        $rootScope.dataHeader.balanceamount = angular.copy(data.balance);
                        $rootScope.dataHeader.gainamount = angular.copy(data.gain);
                        $rootScope.dataHeader.authenticationMethod = angular.copy(data.authenticationMethod);
                    }
                });
            /* Get Active Account Alert */
            mainService.ajax('/alert/verifyalert',
                {
                    listAle: [6, 7]
                },
                function (data) {
                    if (data.code === 0) {
                        $rootScope.showActiveAccount = ($filter('filter')(data.items, { id: 6, state: 1 }).length > 0) ? true : false;
                        $rootScope.showAuthenticationMethod = ($filter('filter')(data.items, { id: 7, state: 1 }).length > 0) ? true : false;
                    }
                });
        }
    }

    $rootScope.closeSession = function (objParam) {
        mainService.ajax('/account/closesession',
            {},
            function (data, callBackFunction) {
                if (data.code == 0) {
                    $cookies.remove('FBid');
                    $cookies.remove('FBemail');
                }
                if (objParam) {
                    $rootScope.isBusy = false;
                    if (objParam.endSession == 1) executeActionMensage(6);
                    if (objParam.endToken == 1) executeActionMensage(7);
                }
                else callBackFunction();
            });
    }

    $scope.closeSessionWithConfirm = function () {
        $rootScope.confirmMessage(function () {
            $rootScope.closeSession();
        });
    }

    $scope.goToSite= function () {
        executeActionMensage(5);
    }

    $scope.clickSlide = function (option) {
        location.href = "/dashboard/" + option;
    }

    $scope.clickSlideAjax = function (action, successFunc) {
        var lan = $('#language').val();
        var url = '/' + lan + '/dashboard/_' + action;
        var hash = '/' + lan + '/dashboard/' + action;
        $scope.selectOptionMenu(action);
        mainService.loadPartial(url).then(function (data) {
            setHash(hash);
            if (typeof successFunc === 'function') successFunc();
            $("main").html($compile(data)($scope));
            $("html, body").animate({ scrollTop: 0 }, "slow");           
        });
    }

    $scope.selectOptionMenu = function (option) {
        $('#slide-out').find('li.active').removeClass('active').find('i.rotate-element').removeClass('rotate-element');
        $('#slide-out').find('a.active').removeClass('active');
        $('#slide-out').find('a.select').removeClass('select');
        $('#slide-out').find('div.collapsible-body').hide('slow');
        $('#mnu_' + option).find("a.waves-effect").addClass('active').parents('.collapsible-body').show().siblings('a.collapsible-header').addClass('active select').parent('li').addClass('active').find('i.fa-angle-down').addClass('rotate-element');
    }

    $scope.changeLanguage = function (langKey) {
        var actualUrl = $location.url();
        var newUrl = actualUrl.replace(/^(\/[a-z]{2}\/)?/, '/' + langKey + '/').replace('//','/');
        executeActionMensage(newUrl);
    };
}