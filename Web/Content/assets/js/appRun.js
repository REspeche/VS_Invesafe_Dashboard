mainApp.run(["$rootScope", "$translate",
    function ($rootScope, $translate) {
        $rootScope.isBusy = false;
        $rootScope.transitionPage = false;

        // is mobile
        $rootScope.isMobile = {
            Android: function () {
                return navigator.userAgent.match(/Android/i);
            },
            BlackBerry: function () {
                return navigator.userAgent.match(/BlackBerry/i);
            },
            iOS: function () {
                return navigator.userAgent.match(/iPhone|iPad|iPod/i);
            },
            Opera: function () {
                return navigator.userAgent.match(/Opera Mini/i);
            },
            Windows: function () {
                return navigator.userAgent.match(/IEMobile/i);
            },
            any: function () {
                return ($rootScope.isMobile.Android() || $rootScope.isMobile.BlackBerry() || $rootScope.isMobile.iOS() || $rootScope.isMobile.Opera() || $rootScope.isMobile.Windows());
            }
        };

        // Alerts
        $rootScope.showAlert = function () {
            return {
                notifySuccess: function (msg) {
                    toastr.success(msg);
                },
                notifyError: function (msg) {
                    toastr.error(msg);
                },
                notifyWarning: function (msg) {
                    toastr.warning(msg);
                },
                notifyInfo: function (msg) {
                    toastr.info(msg);
                }
            }
        };

        //Set var session
        if (document.getElementById("SessionVars")) {
            console.log('session var set!')
            var objDestroy = $("#SessionVars");
            $rootScope.session = angular.fromJson(objDestroy.val());
            objDestroy.remove();
        };

        // Dialog confirm message
        var dialogMsg = $('#dialogMessage');
        $rootScope.confirmMessage = function (yesFunction) {
            $rootScope.yesConfirmFunction = yesFunction;
            dialogMsg.modal(_optionModal);
        };
        dialogMsg.find('.accept').on('click', function () {
            if (typeof $rootScope.yesConfirmFunction == 'function') $rootScope.yesConfirmFunction();
            dialogMsg.modal('hide');
        });
        dialogMsg.find('.cancel').on('click', function () {
            dialogMsg.modal('hide');
        });

        //language
        $translate.use($('#language').val() || $translate.proposedLanguage() || $translate.use());
    }]);