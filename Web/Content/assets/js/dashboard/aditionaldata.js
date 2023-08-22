mainApp.controller("aditionaldataController", aditionaldataController);
aditionaldataController.$inject = ["$rootScope", "$scope", "$timeout", "mainService"];

function aditionaldataController($rootScope, $scope, $timeout, mainService) {
    $scope.aditionalData = {
        gender: '',
        civilState: '',
        profession: '',
        annualIncome: 0
    };
    $scope.genderList = [
        { id: 1, label: 'Masculino' },
        { id: 2, label: 'Femenino' }
    ];
    $scope.civilStateList = [
        { id: 1, label: 'Soltero/a' },
        { id: 2, label: 'Casado/a' },
        { id: 3, label: 'Divorciado/a' },
        { id: 4, label: 'Viudo/a' }
    ];
    $scope.loadForm = false;
    $scope.editForm = false;

    $(document).ready(function () {
        // Material Select Initialization    
        $timeout(function () {
            $('[data-toggle="tooltip"]').tooltip();
        }, 1000);
    });

    /* Aditional Data */
    $scope.loadAditionalData = function () {
        /* Load data form */
        mainService.ajax('/personaldata/getaditionaldata',
            undefined,
            function (data) {
                if (data.code === 0) {
                    $scope.aditionalData = {
                        gender: data.gender,
                        civilState: data.civilState,
                        profession: data.profession,
                        annualIncome: data.annualIncome
                    };
                    $scope.loadForm = true;
                }
            });
    }

    $scope.isEditingForm = function () {
        if (!$scope.editForm) $scope.editForm = true;
    }

    $scope.submitAditionalData = function () {
        if (!$scope.frmAditionalData.$invalid) {
            mainService.ajax('/personaldata/saveaditionaldata',
                $scope.aditionalData,
                function (data) {
                    if (data.code === 0) {
                        $rootScope.showAlert().notifySuccess(data.message);
                        $("form .valid").removeClass("valid");
                    }
                });
        }
        else $rootScope.showAlert().notifyWarning($("span.formIncomplete").text());
    }

}
