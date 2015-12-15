mainApp.controller("bookingController",
    function($scope,
        doctorsService,
        daysService) {

        var loading = {
            FirstName: "Loading",
            LastName: "...",
            Id: -1
        };

        var dateTimeToAvailableDay = function(dateTime) {
            var day = {};
            day.Id = dateTime.Id;
            day.Date = dateTime.toDateString();

            return day;
        }

        var dayToHoursMinutes = function(day) {
            var dateTime = new Date(day.Date);
            var hours = dateTime.getHours();
            var minutes = dateTime.getMinutes();

            if (minutes < 10) {
                minutes = "0" + minutes;
            }

            return hours + ":" + minutes;
        }

        var dateTimeToAvailableTime = function (day) {
            var time = {}
            time.Id = day.Id;
            time.Time = dayToHoursMinutes(day);

            return time;
        }

        var addDateTimeToAvailableDays = function(dateTime) {
            var day = dateTimeToAvailableDay(dateTime);

            $scope.availableDays.push(day);        }

        var addDateTimeToAvailableTimes = function (day) {
            var time = dateTimeToAvailableTime(day);

            $scope.availableTimes.push(time);
        }

        var handleQueryResult = function (data) {
            $scope.doctors = angular.fromJson(data);
        };

        var handleGetByDoctorIdResult = function(data) {
            $scope.days = angular.fromJson(data);

            $scope.availableDays = [];
            $scope.availableTimes = [];

            for (var i = 0; i < $scope.days.length; i++) {
                var dateTime = $scope.days[i].Date; // todo maybe rename days to DateTime
                var currentDateTime = new Date(dateTime);

                addDateTimeToAvailableDays(currentDateTime);
            }
        };

        $scope.query = function () {
            doctorsService.query(handleQueryResult);
        };

        $scope.updateDays = function () {
            daysService.getByDoctorId($scope.doctorId, handleGetByDoctorIdResult);
        };

        var stringStartsWidth = function(string, prefix) {
            return string.slice(0, prefix.length) === prefix;
        }

        $scope.updateTimes = function () {
            // todo replace by filter
            //var out = [];

            $scope.availableTimes = [];

            angular.forEach($scope.days, function (day) {
                var daysDateTime = new Date(day.Date);
                var daysDate = daysDateTime.toDateString();

                if (stringStartsWidth(daysDate, $scope.selectedDayDate)) {
                    addDateTimeToAvailableTimes(day);
                    //out.push(day);
                }

            });

            //$scope.availableTimes = out;
        };

        $scope.init = function () {
            doctorsService.query(handleQueryResult);
        };

        $scope.doctors = [loading];
        $scope.doctor = loading;
        $scope.doctorId = -1;

        $scope.days = [loading];
        $scope.day = loading;
        $scope.dayId = -1;
        $scope.searchResult = [loading];

        $scope.availableDays = [];
        $scope.availableTimes = [];

        $scope.selectedDayId = -1;
        $scope.selectedDayDate = "";
        $scope.selectedDayTimeId = -1;

        $scope.init();
   });