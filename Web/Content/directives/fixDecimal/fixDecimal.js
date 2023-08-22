mainApp.
    directive('fixDecimal', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, elem, attrs, ngModelCtrl) {
            function roundNumber(val) {
                var parsed = parseFloat(val);
                if (parsed !== parsed) { return null; } // check for NaN
                return parsed.toFixed(2);
            }
            ngModelCtrl.$parsers.push(roundNumber); // Parsers take the view value and convert it to a model value.
        }
    };
});