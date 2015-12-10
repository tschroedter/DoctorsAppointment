mainApp.controller("bookingController",
    function($scope,
        doctorsController,
        daysController) {

        $scope.init = function () {
            $scope.query();
        };

        $scope.query = function () {
            doctorsController.query();
        };

        $scope.updateDays = function () {
            alert("doctor.Id: " + $scope.doctorId);
            // todo get days for doctor.Id
            daysController.searchByDoctorId = $scope.doctorId;
            daysController.getByDoctorId();
        }

        $scope.doctors = doctorsController.doctors;
        $scope.doctor = doctorsController.doctor;
        $scope.doctorId = doctorsController.doctors;

        $scope.init();
   });