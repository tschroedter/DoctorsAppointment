describe('daysController', function () {
    beforeEach(module('mainApp'));

    var mockScope = {};
    var mockDaysService = {
        query: jasmine.createSpy('query'),
        get: jasmine.createSpy('get'),
        save: jasmine.createSpy('save'),
        update: jasmine.createSpy('update'),
        delete: jasmine.createSpy('delete'),
        search: jasmine.createSpy('search'),
        getByDoctorId: jasmine.createSpy('getByDoctorId')
    };
    var mockNotificationService = {
        alert: jasmine.createSpy('alert')
    };

    function createSut($controller) {
        $controller('daysController', {
            $scope: mockScope,
            daysService: mockDaysService,
            notificationService: mockNotificationService
        });
    }

    describe('daysController should', function () {

        it('be defined', inject(function ($controller) {
            // Arrange
            // Arrange
            createSut($controller);

            // Act
            // Assert
            var actual = mockScope.daysController;

            expect(actual).toBeDefined();
        }));

        it('set scope', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.daysController.scope;

            expect(actual).toBeDefined();
        }));

        it('set days service', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.daysController.daysService;

            expect(actual).toBeDefined();
        }));

        it('set notificationService', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.daysController.notificationService;

            expect(actual).toBeDefined();
        }));

        it('set days to default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.daysController.days;

            expect(actual.length).toEqual(1);
        }));

        it('set day to default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.daysController.day;

            expect(actual.Id).toEqual(-1);
            expect(actual.FirstName).toEqual('Loading');
            expect(actual.LastName).toEqual('...');
        }));

        it('set searchResult to default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.daysController.searchResult;

            expect(actual.length).toEqual(1);
        }));

        it('set searchByDoctorId to default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.daysController.searchByDoctorId;

            expect(actual).toEqual(1);
        }));
    });

    describe('setSelectedDay should', function () {

        var day = {
            Id: 1
        };

        it('return false for days is null', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            var actual = mockScope.daysController.setSelectedDay({});

            // Assert
            expect(actual).toBeFalsy();
        }));

        it('return true for day is not null', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            var actual = mockScope.daysController.setSelectedDay(day);

            // Assert
            expect(actual).toBeTruthy();
        }));

        it('sets day', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.daysController.setSelectedDay(day);

            // Assert
            var actual = mockScope.daysController.day;

            expect(actual).toEqual(day);
        }));

        it('sets dayId', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.daysController.setSelectedDay(day);

            // Assert
            var actual = mockScope.daysController.dayId;

            expect(actual).toEqual(1);
        }));
    });

    describe('handler tests', function () {

        var jsonDataDays =
            '[' +
            '{\"Date\":\"2015-06-30T00:00:00\",\"DoctorId\":1,\"Id\":1},' +
            '{\"Date\":\"2015-07-01T00:00:00\",\"DoctorId\":1,\"Id\":2},' +
            '{\"Date\":\"2015-07-30T00:00:00\",\"DoctorId\":2,\"Id\":3}' +
            ']';

        var day = {
            Id: 1
        };

        it('handleQueryResult should set for days', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.daysController
                .handleQueryResult(
                    mockScope.daysController,
                    jsonDataDays);

            // Assert
            var actual = mockScope.daysController.days.length;

            expect(actual).toEqual(3);
        }));

        it('handleGetResult should day', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.daysController
                .handleGetResult(
                    mockScope.daysController,
                    day);

            // Assert
            var actual = mockScope.daysController.day;

            expect(actual.Id).toEqual(1);
        }));

        it('handleSaveResult calls notificationService alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.daysController.handleSaveResult(
                mockScope.daysController,
                {});

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));

        it('handleUpdateResult calls notificationService alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.daysController.handleUpdateResult(
                mockScope.daysController,
                {});

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));

        it('handleDeleteResult calls notificationService alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.daysController.handleDeleteResult(
                mockScope.daysController,
                {});

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));

        it('handleSearchResult sets searchResult', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.daysController.handleSearchResult(
                mockScope.daysController,
                jsonDataDays);

            // Assert
            var actual = mockScope.daysController.searchResult;

            expect(actual.length).toEqual(3);
        }));

        it('handleSearchResult calls notificationService alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.daysController.handleSearchResult(
                mockScope.daysController,
                jsonDataDays);

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));
    });

    describe('query should', function () {

        var jsonDataDays =
            '[' +
            '{\"Date\":\"2015-06-30T00:00:00\",\"DoctorId\":1,\"Id\":1},' +
            '{\"Date\":\"2015-07-01T00:00:00\",\"DoctorId\":1,\"Id\":2},' +
            '{\"Date\":\"2015-07-30T00:00:00\",\"DoctorId\":2,\"Id\":3}' +
            ']';

        it('calls slots service query', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.daysController.query();

            // Assert
            expect(mockDaysService.query).toHaveBeenCalled();
        }));

        it('set slots', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockDaysService.query = function (controller,
                                              handler) {
                handler(controller, jsonDataDays);
            };

            // Act
            mockScope.daysController.query();

            // Assert
            var actual = mockScope.daysController.days;

            expect(actual.length).toEqual(3);
        }));
    });

    describe('get should', function () {

        var jsonDataDay = {
            Id: 3
        };

        it('calls slots service get', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.daysController.get();

            // Assert
            expect(mockDaysService.get).toHaveBeenCalled();
        }));

        it('set slot', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockDaysService.get = function (dayId,
                                            controller,
                                            handler) {
                handler(controller, jsonDataDay);
            };

            // Act
            mockScope.daysController.get();

            // Assert
            var actual = mockScope.daysController.day;

            expect(actual.Id).toEqual(3);
        }));
    });

    describe('save should', function () {

        it('calls days service save', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.daysController.save();

            // Assert
            expect(mockDaysService.save).toHaveBeenCalled();
        }));

        it('calls notification service', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockDaysService.save = function (toCreate,
                                             controller,
                                             handler) {
                handler(controller, '');
            };

            // Act
            mockScope.daysController.save();

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));
    });

    describe('update should', function () {

        it('calls slots service update', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.daysController.update();

            // Assert
            expect(mockDaysService.update).toHaveBeenCalled();
        }));

        it('calls notification service alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockDaysService.update = function (toUpdate,
                                               controller,
                                               handler) {
                handler(controller, '');
            };

            // Act
            mockScope.daysController.update();

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));
    });

    describe('delete should', function () {

        it('calls days service delete', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.daysController.delete();

            // Assert
            expect(mockDaysService.delete).toHaveBeenCalled();
        }));

        it('calls notification service alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockDaysService.delete = function (toDelete,
                                               controller,
                                               handler) {
                handler(controller, '');
            };

            // Act
            mockScope.daysController.delete();

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));
    });

    describe('getByDoctorId should', function () {

        var jsonDataDays =
            '[' +
            '{\"Date\":\"2015-06-30T00:00:00\",\"DoctorId\":1,\"Id\":1},' +
            '{\"Date\":\"2015-07-01T00:00:00\",\"DoctorId\":1,\"Id\":2},' +
            '{\"Date\":\"2015-07-30T00:00:00\",\"DoctorId\":2,\"Id\":3}' +
            ']';

        it('calls days service get', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockScope.daysController.searchByDoctorId = 3;

            // Act
            mockScope.daysController.getByDoctorId();

            // Assert
            expect(mockDaysService.getByDoctorId).toHaveBeenCalled();
        }));

        it('sets searchResult', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockScope.daysController.searchByDoctorId = 3;

            mockDaysService.getByDoctorId = function (searchByDoctorId,
                                                      controller,
                                                      handler) {
                handler(controller, jsonDataDays);
            };

            // Act
            mockScope.daysController.getByDoctorId();

            // Assert
            var actual = mockScope.daysController.searchResult;

            expect(actual.length).toEqual(3);
        }));
    });
});


