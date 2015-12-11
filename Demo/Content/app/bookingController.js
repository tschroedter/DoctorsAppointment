mainApp.controller("bookingController",
    function($scope,
        doctorsService,
        daysService) {

        var loading = {
            FirstName: "Loading",
            LastName: "...",
            Id: -1
        };

        var handleQueryResult = function (data) {
            $scope.doctors = angular.fromJson(data);
        };

        var handleGetByDoctorIdResult = function (data) {
            $scope.days = angular.fromJson(data);
        };

        $scope.query = function () {
            doctorsService.query(handleQueryResult);
        };

        $scope.updateDays = function () {
            daysService.getByDoctorId($scope.doctorId, handleGetByDoctorIdResult);
        }

        $scope.init = function () {
            doctorsService.query(handleQueryResult);
        };

        $scope.doctors = [loading];
        $scope.doctor = loading;
        $scope.doctorId = -1;

        $scope.days = [loading];
        $scope.day = loading;
        $scope.dayId = -1;
        $scope.searchResult = [loading];

        $scope.init();
   });