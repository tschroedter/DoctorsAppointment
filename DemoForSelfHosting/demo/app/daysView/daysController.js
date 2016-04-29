mainApp.controller('daysController',
    function ($scope,
              daysService,
              notificationService) {

        var createController =
            function (parameters) {
                var controller = {
                    scope: parameters.scope,
                    daysService: parameters.daysService,
                    notificationService: parameters.notificationService,
                    days: [],
                    day: {},
                    dayId: {},
                    toCreate: {},
                    toUpdate: {},
                    toDelete: {},
                    searchResult: [],
                    searchByDoctorId: {},

                    init: function () {
                        var loading = {
                            FirstName: 'Loading',
                            LastName: '...',
                            Id: -1
                        };

                        this.days = [loading];
                        this.day = loading;
                        this.dayId = 1;
                        this.searchResult = [loading];
                        this.searchByDoctorId = 1;
                    },

                    /* BEGIN: Handlers */

                    handleQueryResult: function (controller, data) {
                        controller.days = angular.fromJson(data);
                    },

                    handleGetResult: function (controller, data) {
                        controller.day = data;
                    },

                    handleSaveResult: function (controller) {
                        controller.notificationService.alert('Created new day!');
                    },

                    handleUpdateResult: function (controller) {
                        controller.notificationService.alert('Updated day!');
                    },

                    handleDeleteResult: function (controller) {
                        controller.notificationService.alert('Updated day!');
                    },

                    handleSearchResult: function (controller, data) {
                        controller.searchResult = angular.fromJson(data);
                        controller.notificationService.alert('Searched!');
                    },

                    /* END: Handlers */

                    /* BEGIN: CRUD */

                    query: function () {
                        daysService.query(
                            this.scope.daysController,
                            this.handleQueryResult);
                    },

                    get: function () {
                        daysService.get(
                            this.dayId,
                            this.scope.daysController,
                            this.handleGetResult);
                    },

                    save: function () {
                        daysService.save(
                            this.toCreate,
                            this.scope.daysController,
                            this.handleSaveResult);
                    },

                    update: function () {
                        daysService.update(
                            this.toUpdate,
                            this.scope.daysController,
                            this.handleUpdateResult);
                    },

                    delete: function () {
                        daysService.delete(
                            this.toDelete,
                            this.scope.daysController,
                            this.handleDeleteResult);
                    },

                    /* END: CRUD */

                    setSelectedDay: function (day) {
                        if (typeof day === 'undefined' ||
                            undefined == day.Id) {
                            return false;
                        }

                        this.day = day;
                        this.dayId = day.Id;

                        return true;
                    },

                    getByDoctorId: function () {
                        daysService.getByDoctorId(
                            this.searchByDoctorId,
                            this.scope.daysController,
                            this.handleSearchResult);
                    }
                };

                controller.init();

                return controller;
            };

        $scope.daysController =
            createController(
                {
                    scope: $scope,
                    daysService: daysService,
                    notificationService: notificationService
                });
    });
