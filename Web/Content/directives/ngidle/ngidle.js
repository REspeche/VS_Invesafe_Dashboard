mainApp
    .directive('ngIdle', function() {
        return {
            restrict: 'E',
            controller:['$rootScope', '$scope', 'Idle',
            function ($rootScope, $scope, Idle) {
              $scope.loadIdle = function () {
                $scope.started = false;
                $scope.countdown = $scope.timeout;

                //inicia watch de IDLE
                Idle.watch();
              };

              $scope.$on('Keepalive', function () {
                  if (!$rootScope.session.isLogin) logout();
              });

              $scope.$on('IdleStart', function() {
                closeModals();
                $scope.warning = $('#idleModal')
                  .modal({
                    backdrop: 'static',
                    keyboard: false
                  });
              });

              $scope.$on('IdleEnd', function() {
                closeModals();
              });

              $scope.$on('IdleTimeout', function() {
                closeModals();
                logout();
              });

              $scope.start = function() {
                closeModals();
                Idle.watch();
                $scope.started = true;
              };

              $scope.stop = function() {
                closeModals();
                Idle.unwatch();
                $scope.started = false;
              };

              function logout() {
                  $rootScope.closeSession({endSession: 1});
              }

              function closeModals() {
                if ($scope.warning) {
                  $scope.warning.modal('toggle');
                  $scope.warning = null;
                }
              }
            }],
            templateUrl: '/content/directives/ngidle/ngidle.html'
        };
    });
