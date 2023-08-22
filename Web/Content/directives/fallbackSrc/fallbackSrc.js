mainApp
    .directive('fallbackSrc', function () {
        return {
            link: function (scope, element, attrs) {
                attrs.$observe('fsrc', function (fsrc) {
                    if (fsrc) {
                        imageExists(fsrc, function (exists) {
                            var imgNotFound = '/content/assets/img/not-found.png';
                            if (element[0].localName == 'div') element.attr("style", "background-image: url('" + ((exists) ? fsrc : imgNotFound) + "')");
                            else element.attr("src", (exists) ? fsrc : imgNotFound);
                            return true;
                        });
                    }
                });
            }
        };
    });