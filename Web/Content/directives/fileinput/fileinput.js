mainApp.
    directive("fileinput", [function () {
        return {
            scope: {
                fileinput: "="
            },
            link: function (scope, element, attributes) {
                var fileReader = new FileReader();
                element.bind("change", function (changeEvent) {
                    scope.fileinput = changeEvent.target.files[0];
                    fileReader.readAsDataURL(scope.fileinput);
                });
            }
        }
    }]);