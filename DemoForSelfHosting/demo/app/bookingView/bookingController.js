mainApp.controller('bookingController',
    function ($scope,
              doctorsService,
              doctorSlotsService,
              daysService,
              notificationService) {

        var createController =
            function (parameters) {
                var controller = {
                    scope: parameters.scope,
                    doctorsService: parameters.doctorsService,
                    doctorSlotsService: parameters.doctorSlotsService,
                    daysService: daysService,
                    notificationService: parameters.notificationService,
                    doctors: [],
                    doctor: {},
                    doctorId: {},
                    days: [],
                    day: {},
                    dayId: {},
                    availableSlots: [],
                    slotId: {},
                    doctorsLoading: {},
                    daysLoading: {},
                    slotsLoading: {},

                    init: function () {
                        this.daysLoading = {
                            Date: 'Loading...',
                            Id: -1
                        };

                        this.slotsLoading = {
                            StartTime: 'Loading...',
                            EndTime: '...',
                            Id: -1
                        };


                        this.doctorsLoading = {
                            FirstName: "Loading",
                            LastName: "...",
                            Id: -1
                        };

                        this.doctors = [this.doctorsLoading];
                        this.doctor = this.doctorsLoading;
                        this.doctorId = -1;

                        this.days = [this.doctorsLoading];
                        this.day = this.doctorsLoading;
                        this.dayId = -1;

                        this.availableSlots = [this.doctorsLoading];
                        this.slotId = -1;
                    },

                    /* BEGIN: Handlers */

                    handleQueryResult: function (controller,
                                                 data) {
                        controller.doctors = angular.fromJson(data);
                    },

                    handleSearchForSlotsResult: function (controller,
                                                          data) {
                        var slots = [];

                        var slotsNew = angular.fromJson(data);

                        for (var i = 0; i < slotsNew.length; i++) {
                            var slot = slotsNew[i];

                            var availableSlot = {};

                            availableSlot.Id = slot.Id;
                            availableSlot.StartTime = controller.dateStringToHoursMinutes(slot.StartDateTime);
                            availableSlot.EndTime = controller.dateStringToHoursMinutes(slot.EndDateTime);
                            availableSlot.Status = slot.Status;

                            slots.push(availableSlot);
                        }

                        controller.availableSlots = slots;
                    },

                    handleGetByDoctorIdResult: function (controller,
                                                         data) {
                        var days = angular.fromJson(data);

                        if (typeof days == 'undefined' ||
                            days === null ||
                            days.length === 0) {
                            return;
                        }

                        controller.days = angular.fromJson(data);
                        controller.availableDates = controller.convertDaysToAvailableDates(controller.days);
                        controller.availableSlots = [controller.slotsLoading];
                        controller.slotId = -1;
                    },

                    /* END: Handlers */

                    /* BEGIN: CRUD */

                    query: function () {
                        doctorsService.query(
                            this.scope.bookingController,
                            this.handleQueryResult);
                    },

                    /* END: CRUD */

                    dayToDate: function (day) {
                        var dateTime = new Date(day.Date);

                        return dateTime.toDateString();
                    },

                    convertDaysToAvailableDates: function (days) {
                        var array = [];

                        for (var i = 0; i < days.length; i++) {
                            var day = days[i];

                            var daysDate = this.dayToDate(day);

                            var myDateTime = {};
                            myDateTime.Id = day.Id;
                            myDateTime.Date = daysDate;

                            array.push(myDateTime);
                        }

                        return array;
                    },

                    dateStringToHoursMinutes: function (dateTime) {
                        var date = new Date(dateTime);

                        var hours = date.getHours();
                        var minutes = date.getMinutes();

                        if (minutes < 10) {
                            minutes = "0" + minutes;
                        }

                        return hours + ":" + minutes;
                    },

                    lookupDay: function (days,
                                         dayId) {
                        for (var i = 0; i < days.length; i++) {
                            var current = days[i];

                            if (current.Id === dayId) {
                                return current;
                            }
                        }

                        return {
                            Id: -1
                        };
                    },

                    getDate: function (days, dayId) {
                        if (dayId < 0) {
                            return '';
                        }

                        var day = this.lookupDay(days, dayId);

                        if (typeof day === 'undefined' ||
                            day == null ||
                            day.Id < 0) {
                            return '';
                        }

                        return day.Date.substring(0, 10);
                    },

                    updateDays: function () {
                        this.availableSlots = [this.slotsLoading];
                        this.slotId = -1;

                        daysService.getByDoctorId(
                            this.doctorId,
                            this.scope.bookingController,
                            this.handleGetByDoctorIdResult);
                    },

                    updateSlots: function () {
                        this.slotId = -1;

                        var date = this.getDate(this.days, parseInt(this.dayId));

                        if (date.length === 0) {
                            return;
                        }

                        this.doctorSlotsService.search(
                            this.doctorId,
                            date,
                            "open",
                            this.scope.bookingController,
                            this.handleSearchForSlotsResult);
                    },

                    isBookDisabled: function () {
                        return this.slotId < 0;
                    },

                    book: function () {
                        this.notificationService.alert("todo");
                    },

                    refresh: function () {
                        doctorsService.query(
                            this.scope.bookingController,
                            this.handleQueryResult);
                    }
                };

                controller.init();

                return controller;
            };

        $scope.bookingController =
            createController(
                {
                    scope: $scope,
                    doctorsService: doctorsService,
                    doctorSlotsService: doctorSlotsService,
                    notificationService: notificationService
                });

        $scope.bookingController.refresh();
    });
