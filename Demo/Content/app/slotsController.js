mainApp.controller("slotsController",
    function($scope,
        slotsService) {

        var loading = {
            Id: -1,
            DayId: -1,
            EndDateTime: new Date("2000-01-01T00:00:00"),
            StartDateTime: new Date("2000-01-01T00:00:00"),
            Status: 0
        };

        $scope.setSelectedSlot = function(slot) {
            if ($scope.slots === null) {
                return false;
            }

            $scope.slot = slot;
            $scope.slotId = slot.Id;

            return true;
        };

        /* BEGIN: Handlers */

        var handleQueryResult = function(data) {
            $scope.slots = angular.fromJson(data);
        };

        var handleGetResult = function(data) {
            $scope.slot = data;
        };

        var handleSaveResult = function() {
            alert("Created new slot!");
        };

        var handleUpdateResult = function() {
            alert("Updated slot!");;
        };

        var handleDeleteResult = function() {
            alert("Updated slot!");;
        };

        /* END: Handlers */

        /* BEGIN: CRUD */

        $scope.query = function() {
            slotsService.query(handleQueryResult);
        };

        $scope.get = function() {
            slotsService.get($scope.slotId, handleGetResult);
        };

        $scope.save = function() {
            slotsService.save($scope.toCreate, handleSaveResult);
        };

        $scope.update = function() {
            slotsService.update($scope.toUpdate, handleUpdateResult);
        };

        $scope.delete = function() {
            slotsService.delete($scope.toDelete, handleDeleteResult);
        };

        /* END: CRUD */

        $scope.slots = [loading];
        $scope.slot = loading;
        $scope.slotId = 1;
        $scope.toCreate = {};
        $scope.toUpdate = {};
        $scope.toDelete = {};
    });