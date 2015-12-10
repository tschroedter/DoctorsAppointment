mainApp.controller("daysController",
    function($scope,
        days,
        daysSearchByDoctorId) {

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

        /* BEGIN: CRUD */

        $scope.query = function() {
            days.query(function(data) {
                $scope.days = angular.fromJson(data);
            });
        };

        $scope.get = function() {
            $scope.day = days.get({ id: $scope.dayId });
        };

        $scope.save = function() {
            days.save($scope.toCreate, function() {
                alert("Created new day!");
            });
        };

        $scope.update = function() {
            $scope.toUpdate.$update(function() {
                alert("Updated day!");
            });
        };

        $scope.delete = function() {
            days.delete($scope.toDelete, function() {
                alert("Deleted day!");
            });
        };

        $scope.getByDoctorId = function () {
            daysSearchByDoctorId.search({
                query: $scope.searchByDoctorId
            }, function (data) {
                $scope.searchResult = angular.fromJson(data);
                alert("Searched!");
            });
        };

        /* END: CRUD */

        $scope.days = [loading];
        $scope.day = loading;
        $scope.dayId = 1;
        $scope.toCreate = new days();
        $scope.toUpdate = days.get({ id: 1 });
        $scope.toDelete = new days();
        $scope.searchResult = [loading];
        $scope.searchByDoctorId = "1";
    });