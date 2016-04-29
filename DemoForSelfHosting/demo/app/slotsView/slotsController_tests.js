describe('slotsController', function () {
    beforeEach(module('mainApp'));

    var mockScope = {};
    var mockSlotsService = {
        query: jasmine.createSpy('query'),
        get: jasmine.createSpy('get'),
        save: jasmine.createSpy('save'),
        update: jasmine.createSpy('update'),
        delete: jasmine.createSpy('delete'),
        search: jasmine.createSpy('search')
    };
    var mockNotificationService = {
        alert: jasmine.createSpy('alert')
    };

    function createSut($controller) {
        $controller('slotsController', {
            $scope: mockScope,
            slotsService: mockSlotsService,
            notificationService: mockNotificationService
        });
    }

    describe('slotsController should', function () {

        it('be defined', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            // Assert
            var actual = mockScope.slotsController;

            expect(actual).toBeDefined();
        }));

        it('set scope', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.slotsController.scope;

            expect(actual).toBeDefined();
        }));

        it('set slotsService', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.slotsController.slotsService;

            expect(actual).toBeDefined();
        }));

        it('set notificationService', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.slotsController.notificationService;

            expect(actual).toBeDefined();
        }));

        it('set slots to default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.slotsController.slots;

            expect(actual.length).toEqual(1);
        }));

        it('set slot to default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.slotsController.slot;

            expect(actual.Id).toEqual(-1);
            expect(actual.DayId).toEqual(-1);
            expect(actual.DayId).toEqual(-1);
            expect(actual.EndDateTime).toEqual(new Date('2000-01-01T00:00:00'));
            expect(actual.StartDateTime).toEqual(new Date('2000-01-01T00:00:00'));
            expect(actual.Status).toEqual(0);
        }));

        it('set slotId to default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.slotsController.slotId;

            expect(actual).toEqual(1);
        }));

        it('set toCreate to default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.slotsController.toCreate;

            expect(actual).toEqual({});
        }));

        it('set toUpdate to default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.slotsController.toUpdate;

            expect(actual).toEqual({});
        }));

        it('set toDelete to default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.slotsController.toDelete;

            expect(actual).toEqual({});
        }));

        it('set searchByDayId to default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.slotsController.searchByDayId;

            expect(actual).toEqual(1);
        }));

        it('set searchResult to default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.slotsController.searchResult;

            expect(actual.length).toEqual(1);
        }));
    });

    describe('setSelectedSlot should', function () {

        var slot = {
            Id: 1
        };

        it('return false for slots is null', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            var actual = mockScope.slotsController.setSelectedSlot({});

            // Assert
            expect(actual).toBeFalsy();
        }));

        it('return true for slot is not null', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            var actual = mockScope.slotsController.setSelectedSlot(slot);

            // Assert
            expect(actual).toBeTruthy();
        }));

        it('sets slot', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.slotsController.setSelectedSlot(slot);

            // Assert
            var actual = mockScope.slotsController.slot;

            expect(actual).toEqual(slot);
        }));

        it('sets slotId', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.slotsController.setSelectedSlot(slot);

            // Assert
            var actual = mockScope.slotsController.slotId;

            expect(actual).toEqual(1);
        }));
    });

    describe('handler tests', function () {

        var jsonDataSlots =
            '[' +
            '{\"Id\":1,\"DayId\":1,\"EndDateTime\":\"2015-06-30T09:15:00\",\"StartDateTime\":\"2015-06-30T09:00:00\",\"Status\":1},' +
            '{\"Id\":2,\"DayId\":2,\"EndDateTime\":\"2015-07-01T09:15:00\",\"StartDateTime\":\"2015-07-01T09:00:00\",\"Status\":1},' +
            '{\"Id\":3,\"DayId\":3,\"EndDateTime\":\"2015-07-30T14:15:00\",\"StartDateTime\":\"2015-07-30T14:00:00\",\"Status\":0}' +
            ']';

        var slot = {
            Id: 1
        };

        it('handleQueryResult should set for slots', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.slotsController
                .handleQueryResult(
                    mockScope.slotsController,
                    jsonDataSlots);

            // Assert
            var actual = mockScope.slotsController.slots.length;

            expect(actual).toEqual(3);
        }));

        it('handleGetResult should slot', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.slotsController
                .handleGetResult(
                    mockScope.slotsController,
                    slot);

            // Assert
            var actual = mockScope.slotsController.slot;

            expect(actual.Id).toEqual(1);
        }));

        it('handleSaveResult calls notificationService alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.slotsController.handleSaveResult(
                mockScope.slotsController,
                slot);

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));

        it('handleUpdateResult calls notificationService alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.slotsController.handleUpdateResult(
                mockScope.slotsController,
                slot);

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));

        it('handleUpdateResult calls notificationService alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.slotsController.handleUpdateResult(
                mockScope.slotsController,
                slot);

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));

        it('handleDeleteResult calls notificationService alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.slotsController.handleDeleteResult(
                mockScope.slotsController,
                slot);

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));

        it('handleSearchResult sets searchResult', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.slotsController.handleSearchResult(
                mockScope.slotsController,
                jsonDataSlots);

            // Assert
            var actual = mockScope.slotsController.searchResult;

            expect(actual.length).toEqual(3);
        }));

        it('handleSearchResult calls notificationService alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.slotsController.handleSearchResult(
                mockScope.slotsController,
                slot);

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));
    });

    describe('query should', function () {

        var jsonDataSlots =
            '[' +
            '{\"Id\":1,\"DayId\":1,\"EndDateTime\":\"2015-06-30T09:15:00\",\"StartDateTime\":\"2015-06-30T09:00:00\",\"Status\":1},' +
            '{\"Id\":2,\"DayId\":2,\"EndDateTime\":\"2015-07-01T09:15:00\",\"StartDateTime\":\"2015-07-01T09:00:00\",\"Status\":1},' +
            '{\"Id\":3,\"DayId\":3,\"EndDateTime\":\"2015-07-30T14:15:00\",\"StartDateTime\":\"2015-07-30T14:00:00\",\"Status\":0}' +
            ']';

        it('calls slots service query', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.slotsController.query();

            // Assert
            expect(mockSlotsService.query).toHaveBeenCalled();
        }));

        it('set slots', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockSlotsService.query = function (controller,
                                               handler) {
                handler(controller, jsonDataSlots);
            };

            // Act
            mockScope.slotsController.query();

            // Assert
            var actual = mockScope.slotsController.slots;

            expect(actual.length).toEqual(3);
        }));
    });
    describe('query should', function () {

        var jsonDataSlots =
            '[' +
            '{\"Id\":1,\"DayId\":1,\"EndDateTime\":\"2015-06-30T09:15:00\",\"StartDateTime\":\"2015-06-30T09:00:00\",\"Status\":1},' +
            '{\"Id\":2,\"DayId\":2,\"EndDateTime\":\"2015-07-01T09:15:00\",\"StartDateTime\":\"2015-07-01T09:00:00\",\"Status\":1},' +
            '{\"Id\":3,\"DayId\":3,\"EndDateTime\":\"2015-07-30T14:15:00\",\"StartDateTime\":\"2015-07-30T14:00:00\",\"Status\":0}' +
            ']';

        // todo fix this test
        it('calls slots service query', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.slotsController.query();

            // Assert
            //expect(mockSlotsService.query).toHaveBeenCalled();
        }));

        it('set slots', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockSlotsService.query = function (controller,
                                               handler) {
                handler(controller, jsonDataSlots);
            };

            // Act
            mockScope.slotsController.query();

            // Assert
            var actual = mockScope.slotsController.slots;

            expect(actual.length).toEqual(3);
        }));
    });

    describe('get should', function () {

        var jsonDataSlot = {
            Id: 3
        };

        it('calls slots service get', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.slotsController.get();

            // Assert
            expect(mockSlotsService.get).toHaveBeenCalled();
        }));

        it('set slot', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockSlotsService.get = function (slotId,
                                             controller,
                                             handler) {
                handler(controller, jsonDataSlot);
            };

            // Act
            mockScope.slotsController.get();

            // Assert
            var actual = mockScope.slotsController.slot;

            expect(actual.Id).toEqual(3);
        }));
    });

    describe('save should', function () {

        it('calls slots service save', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.slotsController.save();

            // Assert
            expect(mockSlotsService.save).toHaveBeenCalled();
        }));

        it('calls slot', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockSlotsService.save = function (toCreate,
                                              controller,
                                              handler) {
                handler(controller, '');
            };

            // Act
            mockScope.slotsController.save();

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));
    });

    describe('update should', function () {

        it('calls slots service update', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.slotsController.update();

            // Assert
            expect(mockSlotsService.update).toHaveBeenCalled();
        }));

        it('calls notification service alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockSlotsService.update = function (toUpdate,
                                                controller,
                                                handler) {
                handler(controller, '');
            };

            // Act
            mockScope.slotsController.update();

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));
    });

    describe('delete should', function () {

        it('calls slots service delete', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.slotsController.delete();

            // Assert
            expect(mockSlotsService.delete).toHaveBeenCalled();
        }));

        it('calls notification service alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockSlotsService.delete = function (toDelete,
                                                controller,
                                                handler) {
                handler(controller, '');
            };

            // Act
            mockScope.slotsController.delete();

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));
    });

    describe('search should', function () {

        var jsonDataSlots =
            '[' +
            '{\"Id\":1,\"DayId\":1,\"EndDateTime\":\"2015-06-30T09:15:00\",\"StartDateTime\":\"2015-06-30T09:00:00\",\"Status\":1},' +
            '{\"Id\":2,\"DayId\":2,\"EndDateTime\":\"2015-07-01T09:15:00\",\"StartDateTime\":\"2015-07-01T09:00:00\",\"Status\":1},' +
            '{\"Id\":3,\"DayId\":3,\"EndDateTime\":\"2015-07-30T14:15:00\",\"StartDateTime\":\"2015-07-30T14:00:00\",\"Status\":0}' +
            ']';

        it('calls slots service search', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.slotsController.search();

            // Assert
            expect(mockSlotsService.search).toHaveBeenCalled();
        }));

        it('calls notification service alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            //noinspection JSUnusedLocalSymbols
            function mockSearch(searchByDayId,
                                controller,
                                handler) {
                handler(controller, jsonDataSlots);
            }

            mockSlotsService.search = mockSearch;


            // Act
            mockScope.slotsController.search();

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled(); // todo check parameters for all usages of toHaveBeenCalled
        }));
    });
});

