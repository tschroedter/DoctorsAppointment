describe('bookingController', function () {
    beforeEach(module('mainApp'));

    var mockScope = {};
    var mockDoctorsService = {
        query: jasmine.createSpy('query')
    };
    var mockDoctorSlotsService = {
        search: jasmine.createSpy('search')
    };
    var mockDaysService = {
        getByDoctorId: jasmine.createSpy('getByDoctorId')
    };
    var mockNotificationService = {
        alert: jasmine.createSpy('alert')
    };

    function createSut($controller) {
        $controller('bookingController', {
            $scope: mockScope,
            doctorsService: mockDoctorsService,
            doctorSlotsService: mockDoctorSlotsService,
            daysService: mockDaysService,
            notificationService: mockNotificationService
        });
    }

    describe('bookingController should', function () {

        it('be defined', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            // Assert
            var actual = mockScope.bookingController;

            expect(actual).toBeDefined();
        }));

        it('set scope', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.bookingController.scope;

            expect(actual).toBeDefined();
        }));

        it('set doctorsService', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.bookingController.doctorsService;

            expect(actual).toBeDefined();
        }));

        it('set doctorSlotsService', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.bookingController.doctorSlotsService;

            expect(actual).toBeDefined();
        }));

        it('set daysService', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.bookingController.daysService;

            expect(actual).toBeDefined();
        }));

        it('set notificationService', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.bookingController.notificationService;

            expect(actual).toBeDefined();
        }));

        it('set doctors', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.bookingController.doctors[0];

            expect(actual.FirstName).toEqual('Loading');
            expect(actual.LastName).toEqual('...');
            expect(actual.Id).toEqual(-1);
        }));

        it('set doctors and returns only one doctor by default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.bookingController.doctors;

            expect(actual.length).toEqual(1);
        }));

        it('set doctor', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.bookingController.doctor.FirstName;

            expect(actual).toBeDefined();
        }));

        it('set doctorId', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.bookingController.doctorId;

            expect(actual).toEqual(-1);
        }));

        it('set days', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.bookingController.days[0];

            expect(actual.FirstName).toEqual('Loading');
            expect(actual.LastName).toEqual('...');
            expect(actual.Id).toEqual(-1);
        }));

        it('set days and returns only one day by default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.bookingController.days;

            expect(actual.length).toEqual(1);
        }));

        it('set day', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.bookingController.day.FirstName; // todo not nice day doesn't have a firstname

            expect(actual).toBeDefined();
        }));

        it('set dayId', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.bookingController.dayId;

            expect(actual).toEqual(-1);
        }));

        it('set doctorsLoading', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.bookingController.doctorsLoading;

            expect(actual.Id).toEqual(-1);
        }));

        it('set daysLoading', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.bookingController.daysLoading;

            expect(actual.Id).toEqual(-1);
        }));

        it('set slotsLoading', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.bookingController.slotsLoading;

            expect(actual.Id).toEqual(-1);
        }));
    });

    describe('refresh should', function () {

        it('calls doctorsService query', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.bookingController.refresh();

            // Assert
            expect(mockDoctorsService.query).toHaveBeenCalled();
        }));
    });

    describe('book should', function () {

        it('calls notificationService alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.bookingController.book();

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));
    });

    describe('isBookDisabled should', function () {

        it('return true for slotId is less than zero', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockScope.bookingController.slotId = -1;

            // Act
            var actual = mockScope.bookingController.isBookDisabled();

            // Assert
            expect(actual).toBeTruthy();
        }));

        it('return false for slotId is greater or equal zero', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockScope.bookingController.slotId = 0;

            // Act
            var actual = mockScope.bookingController.isBookDisabled(); // todo change name of function

            // Assert
            expect(actual).toBeFalsy();
        }));
    });

    describe('handler tests', function () {

        var jsonDataDoctors =
            '[' +
            '{\"LastName\":\"Miller\",\"FirstName\":\"Mary\",\"Id\":1},' +
            '{\"LastName\":\"Smith\",\"FirstName\":\"Will\",\"Id\":2},' +
            '{\"LastName\":\"Smith\",\"FirstName\":\"Jane\",\"Id\":3}' +
            ']';

        var jsonDataSlots =
            '[' +
            '{\"Id\":1,\"DayId\":1,\"EndDateTime\":\"2015-06-30T09:15:00\",\"StartDateTime\":\"2015-06-30T09:00:00\",\"Status\":1},' +
            '{\"Id\":2,\"DayId\":2,\"EndDateTime\":\"2015-07-01T09:15:00\",\"StartDateTime\":\"2015-07-01T09:00:00\",\"Status\":1},' +
            '{\"Id\":3,\"DayId\":3,\"EndDateTime\":\"2015-07-30T14:15:00\",\"StartDateTime\":\"2015-07-30T14:00:00\",\"Status\":0}' +
            ']';

        var jsonDataDays =
            '[' +
            '{\"Date\":\"2015-06-30T00:00:00\",\"DoctorId\":1,\"Id\":1},' +
            '{\"Date\":\"2015-07-01T00:00:00\",\"DoctorId\":1,\"Id\":2}' +
            ']';

        it('handleQueryResult should return correct number of doctors', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.bookingController.handleQueryResult(
                mockScope.bookingController,
                jsonDataDoctors);

            // Assert
            var actual = mockScope.bookingController.doctors.length;

            expect(actual).toEqual(3);
        }));

        it('handleSearchForSlotsResult should return correct number of available slots', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.bookingController.handleSearchForSlotsResult(
                mockScope.bookingController,
                jsonDataSlots);

            // Assert
            var actual = mockScope.bookingController.availableSlots.length;

            expect(actual).toEqual(3);
        }));

        it('handleSearchForSlotsResult should return empty available slots for no slots found', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.bookingController.handleSearchForSlotsResult(
                mockScope.bookingController,
                '[]');

            // Assert
            var actual = mockScope.bookingController.availableSlots.length;

            expect(actual).toEqual(0);
        }));

        it('handleGetByDoctorIdResult should set days', inject(function($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.bookingController.handleGetByDoctorIdResult(
                mockScope.bookingController,
                jsonDataDays);

            // Assert
            var actual = mockScope.bookingController.days.length;

            expect(actual).toEqual(2);
        }));

        it('handleGetByDoctorIdResult should set availableDates', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.bookingController.handleGetByDoctorIdResult(
                mockScope.bookingController,
                jsonDataDays);

            // Assert
            var actual = mockScope.bookingController.availableDates.length;

            expect(actual).toEqual(2);
        }));

        it('handleGetByDoctorIdResult should reset availableSlots', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.bookingController.handleGetByDoctorIdResult(
                mockScope.bookingController,
                jsonDataDays);

            // Assert
            var actual = mockScope.bookingController.availableSlots;

            expect(actual.length).toEqual(1);
            expect(actual[0].Id).toEqual(-1);
        }));

        it('handleGetByDoctorIdResult should reset slotId', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.bookingController.handleGetByDoctorIdResult(
                mockScope.bookingController,
                jsonDataDays);

            // Assert
            var actual = mockScope.bookingController.slotId;

            expect(actual).toEqual(-1);
        }));
    });

    describe('updateSlots should', function () {

        var jsonDataDays =
            '[' +
            '{\"Date\":\"2015-06-30T00:00:00\",\"DoctorId\":1,\"Id\":1},' +
            '{\"Date\":\"2015-07-01T00:00:00\",\"DoctorId\":1,\"Id\":2},' +
            '{\"Date\":\"2015-07-30T00:00:00\",\"DoctorId\":2,\"Id\":3}' +
            ']';

        it('call doctorSlotsService for date', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockScope.bookingController.days = angular.fromJson(jsonDataDays);
            mockScope.bookingController.dayId = "1";

            // Act
            mockScope.bookingController.updateSlots();

            // Assert
            expect(mockDoctorSlotsService.search).toHaveBeenCalled(); // todo not working
        }));

        it('not call doctorSlotsService for date is empty', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockScope.bookingController.days = angular.fromJson(jsonDataDays);
            mockScope.bookingController.dayId = "-1";

            // Act
            mockScope.bookingController.updateSlots();

            // Assert
            expect(mockDoctorSlotsService.search).not.toHaveBeenCalled(); // todo not working
        }));
    });

    describe('dateStringToHoursMinutes should', function () {

        it('return 10:00 for 2015-06-30T00:00:00', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            var actual = mockScope.bookingController.dateStringToHoursMinutes('2015-06-30T00:00:00');

            // Assert
            expect(actual).toEqual('10:00');
        }));

        it('return 10:15 for 2015-06-30T00:05:00', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            var actual = mockScope.bookingController.dateStringToHoursMinutes('2015-06-30T00:15:00');

            // Assert
            expect(actual).toEqual('10:15');
        }));
    });

    describe('lookupDay should', function () {

        var jsonDataDays =
            '[' +
            '{\"Date\":\"2015-06-30T00:00:00\",\"DoctorId\":1,\"Id\":1},' +
            '{\"Date\":\"2015-07-01T00:00:00\",\"DoctorId\":1,\"Id\":2},' +
            '{\"Date\":\"2015-07-30T00:00:00\",\"DoctorId\":2,\"Id\":3}' +
            ']';

        var days = angular.fromJson(jsonDataDays);

        it('return day for existing Id', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            var actual = mockScope.bookingController.lookupDay(days, 1);

            // Assert
            expect(actual.Id).toEqual(1);
            expect(actual.Date).toEqual('2015-06-30T00:00:00');
        }));

        it('return {} for not existing Id', inject(function ($controller) {
            // Arrange
            createSut($controller);

            var days = angular.fromJson(jsonDataDays);

            // Act
            var actual = mockScope.bookingController.lookupDay(days, -1);

            // Assert
            expect(actual.Id).toEqual(-1);
        }));
    });

    describe('getDate should', function () {

        var jsonDataDays =
            '[' +
            '{\"Date\":\"2015-06-30T00:00:00\",\"DoctorId\":1,\"Id\":1},' +
            '{\"Date\":\"2015-07-01T00:00:00\",\"DoctorId\":1,\"Id\":2},' +
            '{\"Date\":\"2015-07-30T00:00:00\",\"DoctorId\":2,\"Id\":3}' +
            ']';

        var days = angular.fromJson(jsonDataDays);

        it('return date for existing Id', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            var actual = mockScope.bookingController.getDate(days, 1);

            // Assert
            expect(actual).toEqual('2015-06-30');
        }));

        it('return empty string for not existing Id', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            var actual = mockScope.bookingController.getDate(days, 123);

            // Assert
            expect(actual).toEqual('');
        }));

        it('return empty string for negative Id', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            var actual = mockScope.bookingController.getDate(days, -1);

            // Assert
            expect(actual).toEqual('');
        }));
    });

    describe('dayToDate should', function () {

        var day =
        {
            Date: '2001-02-03'
        };

        it('return string', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            var actual = mockScope.bookingController.dayToDate(day);

            // Assert
            expect(actual).toEqual('Sat Feb 03 2001');
        }));
    });

    describe('convertDaysToAvailableDates should', function () {

        var jsonDataDays =
            '[' +
            '{\"Date\":\"2015-06-30T00:00:00\",\"DoctorId\":1,\"Id\":1},' +
            '{\"Date\":\"2015-07-01T00:00:00\",\"DoctorId\":1,\"Id\":2},' +
            '{\"Date\":\"2015-07-30T00:00:00\",\"DoctorId\":2,\"Id\":3}' +
            ']';

        var days = angular.fromJson(jsonDataDays);

        it('return array', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            var actual = mockScope.bookingController.convertDaysToAvailableDates(days);

            // Assert
            expect(actual.length).toEqual(3);
        }));

        it('return array for empty days', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            var actual = mockScope.bookingController.convertDaysToAvailableDates([]);

            // Assert
            expect(actual.length).toEqual(0);
        }));
    });

    describe('query should', function () {

        it('calls doctorsService query', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.bookingController.query();

            // Assert
            expect(mockDoctorsService.query).toHaveBeenCalled(); // todo not working
        }));
    });

    describe('updateDays should', function () {

        it('calls daysService getByDoctorId', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.bookingController.updateDays();

            // Assert
            expect(mockDaysService.getByDoctorId).toHaveBeenCalled();
        }));

        it('set availableSlots to default (length test)', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.bookingController.updateDays();

            // Assert
            var actual = mockScope.bookingController.availableSlots;

            expect(actual.length).toEqual(1);
        }));

        it('set availableSlots to loading (text test)', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.bookingController.updateDays();

            // Assert
            var actual = mockScope.bookingController.availableSlots[0];

            expect(actual.StartTime).toEqual('Loading...');
        }));

        it('set slotId to -1', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.bookingController.updateDays();

            // Assert
            var actual = mockScope.bookingController.slotId;

            expect(actual).toEqual(-1);
        }));
    });
});


