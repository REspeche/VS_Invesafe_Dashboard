var mainApp = angular.module('mainApp',
    [
        'ngCookies',
        'ngIdle',
        'ngFileUpload',
        'pascalprecht.translate'
    ]);

mainApp.service("mainService", mainService);
mainService.$inject = ["$http", "$rootScope", "$q"];

function mainService($http, $rootScope, $q) {
    return {
        'ajax': function (url, parameters, successFunc) {
            $rootScope.isBusy = true;
            $http({
                method: 'POST',
                url: url,
                data: (parameters === undefined) ? {} : parameters,
                headers: {
                    'Content-Type': 'application/json',
                    'X-Authorization': $rootScope.session.token
                }
            }).
            then(function successCallback(response) {
                var showAlert = function (code, msg) {
                    if (code >= 100 && code <= 199) $rootScope.showAlert().notifyInfo(msg);
                    else if (code >= 200 && code <= 299) $rootScope.showAlert().notifyWarning(msg);
                    else if (code >= 300 && code <= 399) $rootScope.showAlert().notifyWarning(msg);
                    else if (code < 0) $rootScope.showAlert().notifyError(msg);
                }
                switch (successFunc.length) {
                    case 1:
                        successFunc(response.data);
                        $rootScope.isBusy = false;
                        executeActionMensage(response.data.action);
                        if (response.data.code != 0) {
                            showAlert(response.data.code, response.data.message);
                        }
                        break;
                    case 2:
                        successFunc(response.data, function () {
                            $rootScope.isBusy = false;
                            executeActionMensage(response.data.action);
                            if (response.data.code != 0) {
                                showAlert(response.data.code, response.data.message);
                            }
                        });
                        break;
                };
            }, function errorCallback(response) {
                $rootScope.isBusy = false;
                if (response.status == 403) $rootScope.closeSession({endToken: 1});
                else $rootScope.showAlert().notifyError('Hubo un error en la petición.');
            });
        },
        'loadPartial': function (url) {
            $rootScope.isBusy = true;
            var deferred = $q.defer();
            $http.get(url).
            then(function successCallback(response) {
                deferred.resolve(response.data);
                $rootScope.isBusy = false;
            }, function errorCallback(response) {
                $rootScope.isBusy = false;
                $rootScope.showAlert().notifyError('Hubo un error en la busqueda del archivo.');
            });
            return deferred.promise;
        },
        'fileUpload': function (uploadUrl, files, data, successFunc) {
            $rootScope.isBusy = true;
            $http({
                method: 'POST',
                url: uploadUrl,
                data: {
                    data: data,
                    files: files
                },
                transformRequest: function (data) {
                    var formData = new FormData();
                    formData.append("data", JSON.stringify(data.data));
                    for (var i = 0; i < data.files.length; i++) {
                        formData.append("files[" + i + "]", data.files[i]);
                    }
                    return formData;
                },
                headers: { 'Content-Type': undefined }
            }).then(function successCallback(data) {
                switch (successFunc.length) {
                    case 1:
                        successFunc(data.data);
                        $rootScope.isBusy = false;
                        executeActionMensage(data.data.action);
                        break;
                    case 2:
                        successFunc(data.data, function () {
                            $rootScope.isBusy = false;
                            executeActionMensage(data.data.action);
                        });
                        break;
                };
            }, function errorCallback(data) {
                $rootScope.isBusy = false;
                $rootScope.showAlert().notifyError('Hubo un error en la petición.');
            });
        }
    };
}