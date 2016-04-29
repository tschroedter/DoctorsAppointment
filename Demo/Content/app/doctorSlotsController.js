mainApp.controller('doctorSlotsController',
    function ($scope,
              doctorSlotsService,
              notificationService) {

        var createController =
            function (scope,
                      doctorSlotsService,
                      notificationService) {
                var controller = {
                    scope: scope,
                    doctorSlotsService: doctorSlotsService,
                    notificationService: notificationService,
                    searchResult: [],
                    searchByDoctorId: {},
                    searchByDate: {},
                    searchByStatus: {},

                    init: function () {
                        var loading = {
                            FirstName: 'Loading',
                            LastName: '...',
                            Id: -1
                        };

                        this.searchResult = [loading];
                        this.searchByDoctorId = 1;
                        this.searchByDate = '2015-06-30';
                        this.searchByStatus = 'open';
                    },

                    /* BEGIN: Handlers */

                    handleSearchResult: function (controller, data) {
                        controller.searchResult = angular.fromJson(data);
                        controller.notificationService.alert('Searched!');
                    },

                    /* END: Handlers */

                    /* BEGIN: CRUD */

                    search: function () {
                        doctorSlotsService.search(
                            this.searchByDoctorId,
                            this.searchByDate,
                            this.searchByStatus,
                            this.scope.doctorSlotsController,
                            this.handleSearchResult); // todo use object instead of list
                    }

                    /* END: CRUD */
                };

                controller.init();

                return controller;
            };

        $scope.doctorSlotsController =
            createController(
                $scope,
                doctorSlotsService,
                notificationService);
    });