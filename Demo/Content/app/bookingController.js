mainApp.controller("bookingController",
    function($scope,
        doctorsService,
        daysService,
        slotsService) {

        var month = new Array();
        month[0] = "January";
        month[1] = "February";
        month[2] = "March";
        month[3] = "April";
        month[4] = "May";
        month[5] = "June";
        month[6] = "July";
        month[7] = "August";
        month[8] = "September";
        month[9] = "October";
        month[10] = "November";
        month[11] = "December";

        var loading = {
            FirstName: "Loading",
            LastName: "...",
            Id: -1
        };

        var dayToDate = function(day) {
            var dateTime = new Date(day.Date);
            var dayOfMonth = dateTime.getDay();
            var monthOfYear = month[dateTime.getMonth()];
            var year = dateTime.getYear();

            return dayOfMonth + " " + monthOfYear + " " + year;
        };
        var dayToHoursMinutes = function(day) {
            var dateTime = new Date(day.Date);
            var hours = dateTime.getHours();
            var minutes = dateTime.getMinutes();

            if (minutes < 10) {
                minutes = "0" + minutes;
            }

            return hours + ":" + minutes;
        };
        var convertDaysToDateAndTimeArray = function(days) {
            var array = [];

            angular.forEach(days, function(day) {
                var daysDate = dayToDate(day);
                var daysTime = dayToHoursMinutes(day);

                var myDateTime = {};
                myDateTime.Id = day.Id;
                myDateTime.Date = daysDate;
                myDateTime.Time = daysTime;

                array.push(myDateTime);
            });


            return array;
        };
        var handleQueryResult = function(data) {
            $scope.doctors = angular.fromJson(data);
        };

        var handleGetByDoctorIdResult = function(data) {
            $scope.days = angular.fromJson(data);

            $scope.availableDateTimes = convertDaysToDateAndTimeArray($scope.days);

        };

        var handleGetByDayIdResult = function(data) {
            alert("handleGetByDayIdResult - todo");
        }

        $scope.query = function() {
            doctorsService.query(handleQueryResult);
        };

        $scope.updateDays = function() {
            daysService.getByDoctorId($scope.doctorId, handleGetByDoctorIdResult);
        };

        $scope.updateSlots = function () {
            slotsService.getByDayId($scope.dayId, handleGetByDayIdResult);
        };

        $scope.isBookDisabled = function() {
            return $scope.dayId < 0;
        }

        $scope.book = function() {
            alert("todo");
        }

        $scope.init = function() {
            doctorsService.query(handleQueryResult);
        };

        $scope.doctors = [loading];
        $scope.doctor = loading;
        $scope.doctorId = -1;

        $scope.days = [loading];
        $scope.day = loading;
        $scope.dayId = -1; // todo rename to selectedDayId ???

        $scope.init();
    });