mainApp.controller('doctorsController',
    function ($scope,
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

        doctors.query(function (data) {
            $scope.doctors.list = angular.fromJson(data);
            $scope.doctors.list.sort(compareByLastName);

            if ($scope.doctors.length > 0) {
                $scope.doctors.selected = $scope.doctors.list[0];
            }
        });

        $scope.doctors = {
            list: [loading],
            selected: loading
        };

        $scope.isAvailable = function () {
            if ($scope.doctors.length === 0) {
                return false;
            }

            return true;
        };

        $scope.loadDoctor = function () {
            $scope.doctor = doctors.get({ id: $scope.doctorId });
        };

        $scope.doctor = loading;
        $scope.doctorId = loading.Id;
    });
