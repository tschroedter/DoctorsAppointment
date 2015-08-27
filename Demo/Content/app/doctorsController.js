﻿mainApp.controller('doctorsController',
    function($scope,
        doctors) {

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

        $scope.setSelectedDoctor = function (doctor) {
            if ($scope.doctors === null) {
                return false;
            }

            $scope.doctor = doctor;
            $scope.doctorId = doctor.Id;

            return true;
        };

        /* BEGIN: CRUD */ // TODO: move all this into service
        
        $scope.returnAll = function() {
            doctors.query(function(data) {
                $scope.doctors = angular.fromJson(data);
                $scope.doctors.sort(compareByLastName);
            });
        };

        $scope.returnSingle = function () {
            $scope.doctor = doctors.get({ id: $scope.doctorId });
        };

        $scope.save = function() {
            doctors.save($scope.create, function () {
                alert("Created new doctor!");
            });
        };

        /* END: CRUD */

        $scope.doctors = [loading];
        $scope.doctor = loading;
        $scope.doctorId = loading.Id;
        $scope.create = new doctors();
    });
