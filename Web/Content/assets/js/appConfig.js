mainApp.config(['$compileProvider', '$locationProvider', 'IdleProvider', 'KeepaliveProvider', '$translateProvider',
    function ($compileProvider, $locationProvider, IdleProvider, KeepaliveProvider, $translateProvider) {
        IdleProvider.idle(600); //10 minutes: 600
        IdleProvider.timeout(20);
        KeepaliveProvider.interval(5);

        $compileProvider.debugInfoEnabled(false);
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
        $translateProvider
            .useStaticFilesLoader({
                prefix: '/translations/locale-',
                suffix: '.json'
            })
            .preferredLanguage('en')
            .useLocalStorage();
    }
]);