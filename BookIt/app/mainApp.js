var mainApp = angular.module('mainApp', ['ngResource']);

mainApp.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
}
]);

mainApp.factory("doctors", function ($resource) {
    return $resource("http://localhost:63579/doctors/");
});

