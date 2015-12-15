mainApp.controller("bookingController",
    function($scope,
        doctorsService,
        daysService,
        slotsService, // todo maybe can be removed
        doctorSlotsService) {

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

        var lookupDay = function(days, dayId) {
            for (var i = 0; i < days.length; i++) {
                var current = days[i];

                if (current.Id === dayId) {
                    return current;
                }
            }

            return {};
        };

        var dayToDate = function(day) {
            var dateTime = new Date(day.Date);
            var dayOfMonth = dateTime.getDay();
            var monthOfYear = month[dateTime.getMonth()];
            var year = dateTime.getFullYear();

            return dayOfMonth + " " + monthOfYear + " " + year;
        };

        var dateStringToHoursMinutes = function(dateTime) {
            var date = new Date(dateTime);

            var hours = date.getHours();
            var minutes = date.getMinutes();

            if (minutes < 10) {
                minutes = "0" + minutes;
            }

            return hours + ":" + minutes;
        };

        var convertDaysToAvailableDates = function(days) {
            var array = [];

            angular.forEach(days, function(day) {
                var daysDate = dayToDate(day);

                var myDateTime = {};
                myDateTime.Id = day.Id;
                myDateTime.Date = daysDate;

                array.push(myDateTime);
            });

            return array;
        };
        var handleQueryResult = function(data) {
            $scope.doctors = angular.fromJson(data);
        };

        var handleGetByDoctorIdResult = function(data) {
            $scope.days = angular.fromJson(data);

            $scope.availableDates = convertDaysToAvailableDates($scope.days);
            $scope.availableSlots = [loading];
            $scope.slotId = -1;
        };

        var handleSearchForSlotsResult = function(data) {
            var out = [];

            angular.forEach(data, function(slot) {
                var availabeSlot = {};

                availabeSlot.Id = slot.Id;
                availabeSlot.StartTime = dateStringToHoursMinutes(slot.StartDateTime);
                availabeSlot.EndTime = dateStringToHoursMinutes(slot.EndDateTime);
                availabeSlot.Status = slot.Status;

                out.push(availabeSlot);
            });

            $scope.availableSlots = out;
        };
        $scope.query = function() {
            doctorsService.query(handleQueryResult);
        };

        $scope.updateDays = function() {
            $scope.availableSlots = [loading];
            $scope.slotId = -1;

            daysService.getByDoctorId($scope.doctorId, handleGetByDoctorIdResult);
        };

        $scope.updateSlots = function() {
            $scope.slotId = -1;

            var dayId = parseInt($scope.dayId);

            if (dayId < 0) {
                return;
            }

            var day = lookupDay($scope.days, dayId);
            var date = day.Date.substring(0, 10);

            doctorSlotsService.search(
                $scope.doctorId,
                date,
                "open",
                handleSearchForSlotsResult);
        };

        $scope.isBookDisabled = function() {
            return $scope.slotId < 0;
        };
        $scope.book = function() {
            alert("todo");
        };
        $scope.init = function() {
            doctorsService.query(handleQueryResult);
        };

        $scope.doctors = [loading];
        $scope.doctor = loading;
        $scope.doctorId = -1;

        $scope.days = [loading];
        $scope.day = loading;
        $scope.dayId = -1;

        $scope.availableSlots = [loading];
        $scope.slotId = -1;

        $scope.init();
    });