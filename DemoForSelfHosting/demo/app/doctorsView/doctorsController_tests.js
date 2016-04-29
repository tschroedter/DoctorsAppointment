describe('doctorsController', function () {
    beforeEach(module('mainApp'));

    var mockScope = {};
    var mockDoctorsService = {
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
        return $controller('doctorsController', {
            $scope: mockScope,
            doctorsService: mockDoctorsService,
            notificationService: mockNotificationService
        });
    }

    describe('doctorsController should', function () {

        it('be defined', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorsController;

            expect(actual).toBeDefined();
        }));

        it('set scope', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorsController.scope;

            expect(actual).toBeDefined();
        }));

        it('set doctorsService', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorsController.doctorsService;

            expect(actual).toBeDefined();
        }));

        it('set notificationService', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorsController.notificationService;

            expect(actual).toBeDefined();
        }));

        it('set doctor', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorsController.doctor.FirstName;

            expect(actual).toBeDefined();
        }));

        it('set doctors', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorsController.doctors[0];

            expect(actual.FirstName).toEqual('Loading');
            expect(actual.LastName).toEqual('...');
            expect(actual.Id).toEqual(-1);
        }));

        it('doctors should return only one doctor by default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorsController.doctors;

            expect(actual.length).toEqual(1);
        }));

        it('set toCreate', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorsController.toCreate;

            expect(actual).toBeDefined();
        }));

        it('set toUpdate', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorsController.toUpdate;

            expect(actual).toBeDefined();
        }));

        it('set toDelete', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorsController.toDelete;

            expect(actual).toBeDefined();
        }));

        it('set searchResult', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorsController.searchResult[0];

            expect(actual.FirstName).toEqual('Loading');
            expect(actual.LastName).toEqual('...');
            expect(actual.Id).toEqual(-1);
        }));

        it('searchResult should return only one doctor by default', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorsController.searchResult;

            expect(actual.length).toEqual(1);
        }));

        it('set searchByLastName', inject(function ($controller) {
            // Arrange
            // Act
            createSut($controller);

            // Assert
            var actual = mockScope.doctorsController.searchByLastName;

            expect(actual).toEqual('');
        }));
    });

    describe('refresh should', function () {

        it('calls doctorsService query', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.refresh();

            // Assert
            expect(mockDoctorsService.query).toHaveBeenCalled();
        }));
    });

    describe('setSelectedSlot should', function () {

        it('set doctor in scope', inject(function ($controller) {
            // Arrange
            createSut($controller);

            var doctor = {
                Id: 1
            };

            // Act
            mockScope.doctorsController.setSelectedSlot(
                mockScope.doctorsController,
                doctor);

            // Assert
            var actual = mockScope.doctorsController.doctor;

            expect(doctor.Id).toEqual(actual.Id);
        }));

        it('set doctorId in scope', inject(function ($controller) {
            // Arrange
            createSut($controller);

            var expectedId = 1;
            var doctor = {
                Id: expectedId
            };

            // Act
            mockScope.doctorsController.setSelectedSlot(
                mockScope.doctorsController,
                doctor);

            // Assert
            var actual = mockScope.doctorsController.doctorId;

            expect(actual).toEqual(expectedId);
        }));
    });

    describe('handler tests', function () {

        var data =
            '[' +
            '{\"LastName\":\"Miller\",\"FirstName\":\"Mary\",\"Id\":1},' +
            '{\"LastName\":\"Smith\",\"FirstName\":\"Will\",\"Id\":2},' +
            '{\"LastName\":\"Smith\",\"FirstName\":\"Jane\",\"Id\":3}' +
            ']';

        it('handleQueryResult should return correct number of doctors', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.handleQueryResult(
                mockScope.doctorsController,
                data);

            // Assert
            var actual = mockScope.doctorsController.doctors.length;

            expect(actual).toEqual(3);
        }));

        it('handleQueryResult should return first doctor', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.handleQueryResult(
                mockScope.doctorsController,
                data);

            // Assert
            var actual = mockScope.doctorsController.doctors[0];

            expect(actual.FirstName).toEqual('Mary');
            expect(actual.LastName).toEqual('Miller');
            expect(actual.Id).toEqual(1);
        }));

        it('handleQueryResult should return second doctor', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.handleQueryResult(
                mockScope.doctorsController,
                data);

            // Assert
            var actual = mockScope.doctorsController.doctors[1];

            expect('Will').toEqual(actual.FirstName);
            expect('Smith').toEqual(actual.LastName);
            expect(2).toEqual(actual.Id);
        }));

        it('handleQueryResult should return third doctor', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.handleQueryResult(
                mockScope.doctorsController,
                data);

            // Assert
            var actual = mockScope.doctorsController.doctors[2];

            expect('Jane').toEqual(actual.FirstName);
            expect('Smith').toEqual(actual.LastName);
            expect(3).toEqual(actual.Id);
        }));

        it('handleSaveResult should call alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.handleSaveResult(
                mockScope.doctorsController,
                {});

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));

        it('handleUpdateResult should call alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.handleUpdateResult(
                mockScope.doctorsController,
                {});

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));

        it('handleDeleteResult should call alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.handleDeleteResult(
                mockScope.doctorsController,
                {});

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));

        it('handleSearchResult should call alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.handleSearchResult(
                mockScope.doctorsController,
                data);

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));

        it('handleSearchResult should set searchResult', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.handleSearchResult(
                mockScope.doctorsController,
                data);

            // Assert
            var actual = mockScope.doctorsController.searchResult;

            expect(actual.length).toEqual(3);
        }));
    });

    describe('query function', function () {

        var data =
            '[' +
            '{\"LastName\":\"Miller\",\"FirstName\":\"Mary\",\"Id\":1},' +
            '{\"LastName\":\"Smith\",\"FirstName\":\"Will\",\"Id\":2},' +
            '{\"LastName\":\"Smith\",\"FirstName\":\"Jane\",\"Id\":3}' +
            ']';

        it('calls doctorsService query method', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.query();

            // Assert
            expect(mockDoctorsService.query).toHaveBeenCalled();
        }));

        it('sets doctors', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockDoctorsService.query = function (controller, handler) {
                handler(controller, data);
            };

            // Act
            mockScope.doctorsController.query();

            // Assert
            var actual = mockScope.doctorsController.doctors;

            expect(actual.length).toEqual(3);
        }));
    });

    describe('get function should', function () {

        var data = {
            Id: 1
        };

        it('calls doctorsService get', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.get();

            // Assert
            expect(mockDoctorsService.get).toHaveBeenCalled();
        }));

        it('sets doctor', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockDoctorsService.get = function (docotorId,
                                               controller,
                                               handler) {
                handler(controller, data);
            };

            // Act
            mockScope.doctorsController.get();

            // Assert
            var actual = mockScope.doctorsController.doctor;

            expect(actual.Id).toEqual(1);
        }));

        it('call alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.get();

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));
    });

    describe('save function should', function () {

        it('calls doctorsService save', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.save();

            // Assert
            expect(mockDoctorsService.save).toHaveBeenCalled();
        }));

        it('call alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.save();

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));
    });

    describe('update function should', function () {

        it('calls doctorsService update', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.update();

            // Assert
            expect(mockDoctorsService.update).toHaveBeenCalled();
        }));

        it('call alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.update();

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));
    });

    describe('delete function should', function () {

        it('calls doctorsService delete', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.delete();

            // Assert
            expect(mockDoctorsService.delete).toHaveBeenCalled();
        }));

        it('call alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.delete();

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));
    });

    describe('search function should', function () {

        var data =
            '[' +
            '{\"LastName\":\"Miller\",\"FirstName\":\"Mary\",\"Id\":1},' +
            '{\"LastName\":\"Smith\",\"FirstName\":\"Will\",\"Id\":2},' +
            '{\"LastName\":\"Smith\",\"FirstName\":\"Jane\",\"Id\":3}' +
            ']';

        it('calls doctorsService search', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.search();

            // Assert
            expect(mockDoctorsService.search).toHaveBeenCalled();
        }));

        it('set searchResult', inject(function ($controller) {
            // Arrange
            createSut($controller);

            mockDoctorsService.search = function (searchByLastName,
                                                  controller,
                                                  handler) {
                handler(controller, data);
            };

            // Act
            mockScope.doctorsController.search();

            // Assert
            var actual = mockScope.doctorsController.searchResult;

            expect(actual.length).toEqual(3);
        }));

        it('call alert', inject(function ($controller) {
            // Arrange
            createSut($controller);

            // Act
            mockScope.doctorsController.search();

            // Assert
            expect(mockNotificationService.alert).toHaveBeenCalled();
        }));
    });
});

