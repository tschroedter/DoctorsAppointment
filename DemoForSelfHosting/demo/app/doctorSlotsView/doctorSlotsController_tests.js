describe('doctorsController', function () {
    beforeEach(module('mainApp'));

    var mockScope = {};
    var mockDoctorSlotsService = {
        search: jasmine.createSpy('search')
    };
    var mockNotificationService = {
        alert: jasmine.createSpy('alert')
    };

    function createSut($controller) {
        $controller('doctorSlotsController', {
            $scope: mockScope,
            doctorSlotsService: mockDoctorSlotsService,
            notificationService: mockNotificationService
        });
    }

    describe('doctorSlotsController should', function () {

        it('be defined', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorSlotsController;

            expect(actual).toBeDefined();
        }));

        it('set scope', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorSlotsController.scope;

            expect(actual).toBeDefined();
        }));

        it('set doctorSlotsService', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorSlotsController.doctorSlotsService;

            expect(actual).toBeDefined();
        }));

        it('set notificationService', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorSlotsController.notificationService;

            expect(actual).toBeDefined();
        }));
    });

    describe('handler tests', function () {

        var data =
            '[' +
            '{\"LastName\":\"Miller\",\"FirstName\":\"Mary\",\"Id\":1},' +
            '{\"LastName\":\"Smith\",\"FirstName\":\"Will\",\"Id\":2},' +
            '{\"LastName\":\"Smith\",\"FirstName\":\"Jane\",\"Id\":3}' +
            ']';

        it('handleSearchResult should set searchResult', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorSlotsController
                .handleSearchResult(mockScope.doctorSlotsController,
                    data);

            // Assert
            var actual = mockScope.doctorSlotsController.searchResult.length;

            expect(actual).toEqual(3);
        }));

        it('handleSearchResult should call alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorSlotsController
                .handleSearchResult(mockScope.doctorSlotsController,
                    data);

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));
    });

    describe('searchResult should', function () {

        it('return default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorSlotsController.searchResult[0];

            expect(actual.FirstName).toEqual('Loading');
            expect(actual.LastName).toEqual('...');
            expect(actual.Id).toEqual(-1);
        }));

        it('doctors should return only one doctor by default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorSlotsController.searchResult;

            expect(actual.length).toEqual(1);
        }));
    });

    describe('searchByDoctorId should', function () {

        it('return default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorSlotsController.searchByDoctorId;

            expect(actual).toEqual(1);
        }));
    });

    describe('searchByDate should', function () {

        it('return default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorSlotsController.searchByDate;

            expect(actual).toEqual('2015-06-30');
        }));
    });

    describe('searchByStatus should', function () {

        it('return default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorSlotsController.searchByStatus;

            expect(actual).toEqual('open');
        }));
    });

    describe('search should', function () {

        var data =
            '[' +
            '{\"LastName\":\"Miller\",\"FirstName\":\"Mary\",\"Id\":1},' +
            '{\"LastName\":\"Smith\",\"FirstName\":\"Will\",\"Id\":2},' +
            '{\"LastName\":\"Smith\",\"FirstName\":\"Jane\",\"Id\":3}' +
            ']';

        it('call doctorSlotsService search', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorSlotsController.search();

            // Assert
            expect(mockDoctorSlotsService.search).toHaveBeenCalled();
        }));

        it('call doctorSlotsService search', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockDoctorSlotsService.search =
                function (searchByDoctorId,
                          searchByDate,
                          searchByStatus,
                          controller,
                          handler) {
                    handler(controller, data);
                };

            // Act
            mockScope.doctorSlotsController.search();

            // Assert
            var actual = mockScope.doctorSlotsController.searchResult;
            expect(actual.length).toEqual(3);
        }));
    });
});

