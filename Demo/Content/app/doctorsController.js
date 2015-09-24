﻿mainApp.controller("doctorsController",
    function($scope,
        doctors,
        doctorsSearchByLastName) {

        var loading = {
            FirstName: "Loading",
            LastName: "...",
            Id: -1
        };

        function compareByLastName(a, b) {
            if (a.LastName < b.LastName)
                return -1;
            if (a.LastName > b.LastName)
                return 1;
            return 0;
        }

        $scope.init = function () {
            $scope.query();
        };

        $scope.setSelectedSlot = function(doctor) {
            if ($scope.doctors === null) {
                return false;
            }

            $scope.doctor = doctor;
            $scope.doctorId = doctor.Id;

            return true;
        };

        /* BEGIN: CRUD */

        $scope.query = function() {
            doctors.query(function(data) {
                $scope.doctors = angular.fromJson(data);
                $scope.doctors.sort(compareByLastName);
            });
        };

        $scope.get = function() {
            $scope.doctor = doctors.get({ id: $scope.doctorId });
        };

        $scope.save = function() {
            doctors.save($scope.toCreate, function() {
                alert("Created new doctor!");
            });
        };

        $scope.update = function() {
            $scope.toUpdate.$update(function() {
                alert("Updated doctor!");
            });
        };

        $scope.delete = function() {
            doctors.delete($scope.toDelete, function() {
                alert("Deleted doctor!");
            });
        };

        $scope.search = function() {
            doctorsSearchByLastName.search({
                query: $scope.searchByLastName
            }, function(data) {
                $scope.searchResult = angular.fromJson(data);
                alert("Searched!");
            });
        };

        /* END: CRUD */

        $scope.doctors = [loading];
        $scope.doctor = loading;
        $scope.doctorId = 1;
        $scope.toCreate = new doctors();
        $scope.toUpdate = doctors.get({ id: 1 });
        $scope.toDelete = new doctors();
        $scope.searchResult = [loading];
        $scope.searchByLastName = "";

        $scope.init();

        // stuff below should be in separate controller
        $scope.updateDays = function () {
            alert("doctor.Id: " + $scope.doctorId);
        }

    });