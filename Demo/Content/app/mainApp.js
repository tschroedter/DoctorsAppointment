var mainApp = angular.module("mainApp", ["ngResource"]);

mainApp.factory("doctors", function ($resource) {
    return $resource("/doctors/:id");
});