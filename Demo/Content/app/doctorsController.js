mainApp.controller("doctorsController",
    function($scope,
        doctorsService) {

        var loading = {
            FirstName: "Loading",
            LastName: "...",
            Id: -1
        };

        $scope.init = function() {
            doctorsService.query();
        };

        $scope.setSelectedSlot = function(doctor) {
            if ($scope.doctors === null) {
                return false;
            }

            $scope.doctor = doctor;
            $scope.doctorId = doctor.Id;

            return true;
        };

        /* BEGIN: Handlers */

        var handleQueryResult = function (data) {
            $scope.doctors = angular.fromJson(data);
        };

        var handleGetResult = function (data) {
            $scope.doctor = data;
        };

        var handleSaveResult = function () {
            alert("Created new doctor!");
        };

        var handleUpdateResult = function () {
            alert("Updated doctor!");;
        };

        var handleDeleteResult = function () {
            alert("Updated doctor!");;
        };

        var handleSearchResult = function (data) {
            $scope.searchResult = angular.fromJson(data);
            alert("Searched!");;
        };

        /* END: Handlers */

        /* BEGIN: CRUD */

        $scope.query = function () {
            doctorsService.query(handleQueryResult);
        };

        $scope.get = function() {
            handleGetResult(doctorsService.get($scope.doctorId));
        };

        $scope.save = function() {
            doctorsService.save($scope.toCreate, handleSaveResult);
        };

        $scope.update = function() {
            doctorsService.update($scope.toUpdate, handleUpdateResult);
        };

        $scope.delete = function() {
            doctorsService.delete($scope.toDelete, handleDeleteResult);
        };

        $scope.search = function() {
            doctorsService.search($scope.searchByLastName, handleSearchResult);
        };

        /* END: CRUD */

        $scope.doctors = [loading];
        $scope.doctor = loading;
        $scope.doctorId = 1;
        $scope.toCreate = {};
        $scope.toUpdate = {};
        $scope.toDelete = {};
        $scope.searchResult = [loading];
        $scope.searchByLastName = "";

        $scope.init();
    });