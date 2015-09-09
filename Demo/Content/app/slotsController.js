mainApp.controller("slotsController",
    function($scope,
        slots) {

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

        /* BEGIN: CRUD */

        $scope.query = function() {
            slots.query(function(data) {
                $scope.slots = angular.fromJson(data);
            });
        };

        $scope.get = function() {
            $scope.slot = slots.get({ id: $scope.slotId });
        };

        $scope.save = function() {
            slots.save($scope.toCreate, function() {
                alert("Created new slot!");
            });
        };

        $scope.update = function() {
            $scope.toUpdate.$update(function() {
                alert("Updated slot!");
            });
        };

        $scope.delete = function() {
            slots.delete($scope.toDelete, function() {
                alert("Deleted slot!");
            });
        };

        /* END: CRUD */

        $scope.slots = [loading];
        $scope.slot = loading;
        $scope.slotId = 1;
        $scope.toCreate = new slots();
        $scope.toUpdate = slots.get({ id: 1 });
        $scope.toDelete = new slots();
    });