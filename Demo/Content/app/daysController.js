mainApp.controller("daysController",
    function($scope,
        daysService) {

        var loading = {
            FirstName: "Loading",
            LastName: "...",
            Id: -1
        };

        $scope.setSelectedSlot = function(day) {
            if ($scope.days === null) {
                return false;
            }

            $scope.day = day;
            $scope.dayId = day.Id;

            return true;
        };

        /* BEGIN: Handlers */

        var handleQueryResult = function(data) {
            $scope.days = data;
        };

        var handleGetResult = function(data) {
            $scope.day = data;
        };

        var handleSaveResult = function() {
            alert("Created new day!");
        };

        var handleUpdateResult = function() {
            alert("Updated day!");;
        };

        var handleDeleteResult = function() {
            alert("Updated day!");;
        };

        var handleSearchResult = function(data) {
            $scope.searchResult = angular.fromJson(data);
            alert("Searched!");;
        };

        /* END: Handlers */

        /* BEGIN: CRUD */

        $scope.query = function() {
            daysService.query(handleQueryResult);
        };

        $scope.get = function() {
            handleGetResult(daysService.get($scope.dayId));
        };

        $scope.save = function() {
            daysService.save($scope.toCreate, handleSaveResult);
        };

        $scope.update = function() {
            daysService.update($scope.toUpdate, handleUpdateResult);
        };

        $scope.delete = function() {
            daysService.delete($scope.toDelete, handleDeleteResult);
        };

        $scope.getByDoctorId = function() {
            daysService.getByDoctorId($scope.searchByDoctorId, handleSearchResult);
        };

        /* END: CRUD */

        $scope.days = [loading];
        $scope.day = loading;
        $scope.dayId = 1;
        $scope.toCreate = {};
        $scope.toUpdate = {};
        $scope.toDelete = {};
        $scope.searchResult = [loading];
        $scope.searchByDoctorId = 1;
    });