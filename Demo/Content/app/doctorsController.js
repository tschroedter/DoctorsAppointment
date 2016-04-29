mainApp.controller('doctorsController',
    function ($scope,
              doctorsService,
              notificationService) {

        var createController =
            function (scope,
                      doctorsService,
                      notificationService) {
                var controller = {
                    scope: scope,
                    doctorsService: doctorsService,
                    notificationService: notificationService,
                    doctors: [],
                    doctor: {},
                    doctorId: {},
                    toCreate: {},
                    toUpdate: {},
                    toDelete: {},
                    searchResult: [],
                    searchByLastName: {},

                    init: function () {

                        var loading = {
                            FirstName: 'Loading',
                            LastName: '...',
                            Id: -1
                        };

                        this.doctors = [loading];
                        this.doctor = loading;
                        this.doctorId = 1;
                        this.searchResult = [loading];
                        this.searchByLastName = '';
                    },

                    setSelectedSlot: function (controller, doctor) {
                        controller.doctor = doctor;
                        controller.doctorId = doctor.Id;
                    },

                    refresh: function () {
                        this.doctorsService.query();
                    },

                    /* BEGIN: Handlers */

                    handleQueryResult: function (controller, data) {
                        controller.doctors = angular.fromJson(data);
                    },

                    handleGetResult: function (controller, data) {
                        controller.doctor = data;
                    },

                    handleSaveResult: function (controller) {
                        controller.notificationService.alert('Created new doctor!');
                    },

                    handleUpdateResult: function (controller) {
                        controller.notificationService.alert('Updated doctor!');
                    },

                    handleDeleteResult: function (controller) {
                        controller.notificationService.alert('Updated doctor!');
                    },

                    handleSearchResult: function (controller, data) {
                        controller.searchResult = angular.fromJson(data);
                        controller.notificationService.alert('Searched!');
                    },

                    /* END: Handlers */

                    /* BEGIN: CRUD */
                    query: function () {
                        doctorsService.query(
                            this.scope.doctorsController,
                            this.handleQueryResult);
                    },

                    get: function () {
                        doctorsService.get(
                            this.doctorId,
                            this.scope.doctorsController,
                            this.handleGetResult);
                    },

                    save: function () {
                        doctorsService.save(
                            this.toCreate,
                            this.scope.doctorsController,
                            this.handleSaveResult);
                    },

                    update: function () {
                        doctorsService.update(
                            this.toUpdate,
                            this.scope.doctorsController,
                            this.handleUpdateResult);
                    },

                    delete: function () {
                        doctorsService.delete(
                            this.toDelete,
                            this.scope.doctorsController,
                            this.handleDeleteResult);
                    },

                    search: function () {
                        doctorsService.search(
                            this.searchByLastName,
                            this.scope.doctorsController,
                            this.handleSearchResult);
                    }

                    /* END: CRUD */
                };

                controller.init();

                return controller;
            };

        $scope.doctorsController =
            createController(
                $scope,
                doctorsService,
                notificationService);
    });