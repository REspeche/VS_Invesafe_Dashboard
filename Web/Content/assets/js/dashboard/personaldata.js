mainApp.controller("personaldataController", personaldataController);
personaldataController.$inject = ["$rootScope", "$scope", "mainService", "$timeout", "$filter"];

function personaldataController($rootScope, $scope, mainService, $timeout, $filter) {
    $scope.personalData = {
        firstName: '',
        lastName: '',
        email: '',
        codePhone: '',
        phone: '',
        bornDate: '',
        nationality: 0,
        live: 0,
        city: '',
        cp: '',
        address: '',
        cuit: '',
        exposedPolitician: 0,
        photoDocumentFront: undefined,
        photoDocumentBack: undefined,
        authenticationMethod: 0
    };
    $scope.countryList = [];
    $scope.yesNoList = [];
    $scope.fileFront = undefined;
    $scope.fileBack = undefined;
    $scope.loadForm = false;
    $scope.editForm = false;
    $scope.verifyDocument = true;
    $scope.stateDocument = 0;

    $(document).ready(function() {
        // Material Select Initialization    
        $timeout(function () {
            $('[data-toggle="tooltip"]').tooltip();

            // Data Picker Initialization
            $('.datepicker').pickadate(Object.assign({}, _datePickerDefault, { max: -6570 }));
        }, 1000);
    });

    /* Personal Data */

    $scope.loadPersonalData = function () {
        /* Load combos */
        mainService.ajax('/common/getlistcountry',
            undefined,
            function (data) {
                if (data.code === 0) $scope.countryList = angular.copy(data.items);
            });
        mainService.ajax('/common/getlistyesno',
            undefined,
            function (data) {
                if (data.code === 0) $scope.yesNoList = angular.copy(data.items);
            });

        /* Load data form */
        mainService.ajax('/personaldata/getpersonaldata',
            undefined,
            function (data) {
                if (data.code === 0) {
                    $scope.personalData = {
                        firstName: data.firstName,
                        lastName: data.lastName,
                        email: data.email,
                        codePhone: data.codePhone,
                        phone: data.phone,
                        _bornDate: UnixTimeStampToDateTime(data.bornDate),
                        bornDate: data.bornDate,
                        nationality: data.nationality,
                        live: data.live,
                        city: data.city,
                        cp: data.cp,
                        address: data.address,
                        cuit: data.cuit,
                        exposedPolitician: data.exposedPolitician,
                        authenticationMethod: data.authenticationMethod
                    };
                    $scope.loadForm = true;
                }
            });
    }

    $scope.loadPersonalDocuments = function () {
        /* Load data form */
        mainService.ajax('/personaldata/getpersonaldata',
            undefined,
            function (data) {
                if (data.code === 0) {
                    $scope.personalData = {
                        photoDocumentFront: data.photoDocumentFront,
                        photoDocumentBack: data.photoDocumentBack,
                    };
                    $scope.loadForm = true;

                    /* Get Alert for documents */
                    mainService.ajax('/alert/verifyalert',
                        {
                            listAle: [3]
                        },
                        function (data) {
                            if (data.code === 0 && data.items.length > 0) $scope.stateDocument = data.items[0].state;
                        });
                }
            });

    }

    $scope.isEditingForm = function () {
        if (!$scope.editForm) $scope.editForm = true;
    }

    $scope.submitPersonalData = function () {
        if (!$scope.frmPersonalData.$invalid) {
            $scope.personalData.bornDate = DateTimeToUnixTimestamp($scope.personalData._bornDate);

            mainService.ajax('/personaldata/savepersonaldata',
                $scope.personalData,
                function (data) {
                    if (data.code === 0) {
                        $rootScope.session.fullname = data.firstName + " " + data.lastName;
                        $rootScope.showAlert().notifySuccess(data.message);
                        $("form .valid").removeClass("valid");
                    }
                });
        }
        else $rootScope.showAlert().notifyWarning($("span.formIncomplete").text());
    }

    $scope.submitPersonalDocuments = function () {
        //Save Front or Back
        if ($scope.fileFront && $scope.fileBack) {
            var files = [];
            files.push($scope.fileFront);
            files.push($scope.fileBack);
            mainService.fileUpload('/personaldata/savedocuments',
                files,
                undefined,
                function (data) {
                    if (data.code === 0) {
                        $scope.fileFront = undefined;
                        $scope.fileBack = undefined;
                        $scope.personalData.photoDocumentFront = data.photoDocumentFront;
                        $scope.personalData.photoDocumentBack = data.photoDocumentBack;
                        $rootScope.showAlert().notifySuccess(data.message);
                    }
                });
        }
        else $rootScope.showAlert().notifyWarning($("span.formIncomplete").text());
    }

}
