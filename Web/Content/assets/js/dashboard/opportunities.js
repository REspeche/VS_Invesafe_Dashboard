mainApp.controller("opportunitiesController", opportunitiesController);
opportunitiesController.$inject = ["$rootScope", "$scope", "mainService", "$timeout", "$filter"];

function opportunitiesController($rootScope, $scope, mainService, $timeout, $filter) {
    $scope.projects = [];
    $scope.documents = [];
    $scope.docCount = {
        cad1: 0,
        cad2: 0,
        cad3: 0
    }
    $scope.formCode = {
        code: ''
    }
    $scope.formInvest = {
        paymentType: 0 ,
        agree: false,
        amount: 0,
        amountProfit: 0
    }
    $scope.project = {};
    $scope.loadPage = false;
    $scope.bankAccount = [];
    $scope.referenceCode = undefined;
    $scope.alerts = [];
    $scope.showMessage = {};

    $(document).ready(function () {
        // Material Select Initialization    
        $timeout(function () {
            $('[data-toggle="tooltip"]').tooltip();

            $(".card").flip({
                front: '.card-front',
                back: '.card-back',
                trigger: 'manual',
                forceHeight: true
            });
            $(".card-back").show();
        }, 1000);
    });

    /* Opportunities */

    $scope.loadOpportunities = function () {
        /* Load projects */
        mainService.ajax('/project/getprojects',
            undefined,
            function (data) {
                if (data.code === 0) $scope.projects = angular.copy(data.items);
            });
    }

    $scope.loadProjectDetail = function () {
        $scope.selectTab = (getHash()) ? getHash() : 'tab1';
        $scope.loadPage = false;

        /* Load project */
        mainService.ajax('/project/getproject',
            {
                proId: getQueryStringValue('id')
            },
            function (data) {
                if (data.code === 0) {
                    $scope.project = data;
                    $scope.project._startFinancing = UnixTimeStampToDateTime(data.startFinancing, true);
                    if (data.endFinancing > 0) $scope.project._endFinancing = UnixTimeStampToDateTime(data.endFinancing, true);
                    else $scope.project._endFinancing = null;
                    $("#tab1 .project-content").load('/content/partials/' + $scope.project.id + '_tab1.' + $rootScope.session.lang + '.html');
                    initInvest();
                    $scope.loadPage = true;
                }
            });

        if ($scope.selectTab == 'tab3') $scope.loadDocuments();
    }

    $scope.loadDocuments = function () {
        if ($scope.documents.length == 0) {
            /* Load documents */
            mainService.ajax('/project/getdocuments',
                {
                    proId: getQueryStringValue('id')
                },
                function (data) {
                    if (data.code === 0) {
                        $scope.documents = angular.copy(data.items);
                        angular.forEach($scope.documents, function (doc, key) {
                            doc.file_lang = lang + '_' + doc.file;
                        });
                        $scope.docCount.cad1 = $filter('filter')($scope.documents, { cadId: 1 }, true).length;
                        $scope.docCount.cad2 = $filter('filter')($scope.documents, { cadId: 2 }, true).length;
                        $scope.docCount.cad3 = $filter('filter')($scope.documents, { cadId: 3 }, true).length;
                    }
                });
        }
    }

    $scope.flipCard = function (id,move) {
        $("#pro_" + id).flip(move);
    }

    $scope.viewProject = function (id) {
        location.href = "/project/detail?id=" + id;
    }

    $scope.openDocumentation = function () {
        $('.nav-tabs a[href="#tab3"]').tab('show');
        $scope.loadDocuments();
    }

    $scope.toInvest = function (id) {
        if ($scope.project.restAmount >= $scope.formInvest.amount) {
            if ($scope.formInvest.amount > 0) {
                sessionStorage.setItem("project", JSON.stringify({
                    id: id,
                    ammount: $scope.formInvest.amount
                }));
                location.href = "/project/invest";
            }
            else $rootScope.showAlert().notifyWarning($("span.investZero").text());
        }
        else {
            $rootScope.showAlert().notifyWarning($("span.amountFail").text().format($scope.project.restAmount));
            $scope.formInvest.amount = $scope.project.restAmount;
            $scope.calculateProfit();
        }
    }

    /* To Invest --------------------- */

    $scope.loadToInvest = function () {
        /* Load project */
        var objPro = JSON.parse(sessionStorage.getItem("project"));
        mainService.ajax('/project/getproject',
            {
                proId: objPro.id
            },
            function (data) {
                if (data.code === 0) {
                    $scope.project = data;
                    initInvest();
                    $scope.formInvest.amount = objPro.ammount;
                    $scope.loadPage = true;
                }
            });
    }

    $scope.confirmInvest = function () {
        if ($scope.project.restAmount >= $scope.formInvest.amount) {
            if ($scope.formInvest.agree) {
                /* Send code verification */
                mainService.ajax(($rootScope.dataHeader.authenticationMethod == 1) ?'/code/GetTwoFactorAuthenticator':'/code/SendCodeBySMS',
                    undefined,
                    function (data) {
                        if (data.code == 0) {
                            $("#modalCode")
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
            else $rootScope.showAlert().notifyWarning($("span.aceptAgree").text());
        }
        else $rootScope.showAlert().notifyWarning($("span.amountFail").text().format($scope.project.restAmount));
    }

    $scope.continueInvest = function () {
        if ($scope.formCode.code != '') {
            /* To Invest */
            mainService.ajax('/project/saveinvest',
                {
                    proId: $scope.project.id,
                    amount: $scope.formInvest.amount,
                    payment: 2,
                    agree: ($scope.formInvest.agree) ? 1 : 0,
                    code: $scope.formCode.code
                },
                function (data) {
                    if (data.code === 0) {
                        $rootScope.showAlert().notifySuccess(data.message);
                        location.href = "/project/entryfunds";
                    }
                });
        }
        else $rootScope.showAlert().notifyWarning($("span.codeFail").text());
    }

    $scope.calculateProfit = function () {
        $scope.formInvest.amountProfit = $scope.formInvest.amount + ($scope.formInvest.amount * ($scope.project.profitability * $scope.project.timeLimit / 12) / 100);
    }

    var initInvest = function () {
        $scope.formInvest = {
            paymentType: 0,
            agree: false,
            amount: $scope.project.maxInvest,
            amountProfit: 0
        }
        $scope.calculateProfit();
    }

    /* Enter Founds --------------------- */

    $scope.loadEntryFunds = function () {
        /* Load project */
        var objPro = JSON.parse(sessionStorage.getItem("project"));
        mainService.ajax('/project/getproject',
            {
                proId: objPro.id
            },
            function (data) {
                if (data.code === 0) {
                    $scope.project = data;
                    initInvest();
                    $scope.formInvest.amount = objPro.ammount;
                    $scope.loadPage = true;
                }
            });

        /* Get BankAccount */
        mainService.ajax('/bank/getprojectbankaccount',
            {
                proId: objPro.id
            },
            function (data) {
                if (data.code === 0) $scope.bankAccount = angular.copy(data);
                $scope.loadPage = true;
            });
    };
}
