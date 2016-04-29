mainApp.controller('slotsController',
    function ($scope,
              slotsService,
              notificationService) {

        var createController =
            function (scope,
                      slotsService,
                      notificationService) {

                var controller = {
                    scope: scope,
                    slotsService: slotsService,
                    notificationService: notificationService,
                    slots: [],
                    slot: {},
                    slotId: {},
                    toCreate: {},
                    toUpdate: {},
                    toDelete: {},
                    searchByDayId: {},
                    searchResult: [],

                    init: function () {
                        var loading = {
                            Id: -1,
                            DayId: -1,
                            EndDateTime: new Date('2000-01-01T00:00:00'),
                            StartDateTime: new Date('2000-01-01T00:00:00'),
                            Status: 0
                        };

                        this.slots = [loading];
                        this.slot = loading;
                        this.slotId = 1;
                        this.toCreate = {};
                        this.toUpdate = {};
                        this.toDelete = {};
                        this.searchByDayId = 1;
                        this.searchResult = [loading];
                    },

                    /* BEGIN: Handlers */

                    handleQueryResult: function (controller, data) {
                        controller.slots = angular.fromJson(data);
                    },

                    handleGetResult: function (controller, data) {
                        controller.slot = data;
                    },

                    handleSaveResult: function (controller) {
                        controller.notificationService.alert('Created new slot!');
                    },

                    handleUpdateResult: function (controller) {
                        controller.notificationService.alert('Updated slot!');
                    },

                    handleDeleteResult: function (controller) {
                        controller.notificationService.alert('Updated slot!');
                    },

                    handleSearchResult: function (controller, data) {
                        controller.searchResult = angular.fromJson(data);
                        controller.notificationService.alert('Searched!');
                    },

                    /* END: Handlers */

                    /* BEGIN: CRUD */

                    query: function () {
                        slotsService.query(
                            this.scope.slotsController,
                            this.handleQueryResult);
                    },

                    get: function () {
                        slotsService.get(
                            this.slotId,
                            this.scope.slotsController,
                            this.handleGetResult);
                    },

                    save: function () {
                        slotsService.save(
                            this.toCreate,
                            this.scope.slotsController,
                            this.handleSaveResult);
                    },

                    update: function () {
                        slotsService.update(
                            this.toUpdate,
                            this.scope.slotsController,
                            this.handleUpdateResult);
                    },
                    delete: function () {
                        slotsService.delete(
                            this.toDelete,
                            this.scope.slotsController,
                            this.handleDeleteResult);
                    },

                    search: function () {
                        slotsService.search(
                            this.searchByDayId,
                            this.scope.slotsController,
                            this.handleSearchResult);
                    },

                    /* END: CRUD */

                    setSelectedSlot: function (slot) {
                        if (typeof slot === 'undefined' ||
                            undefined == slot.Id) {
                            return false;
                        }

                        this.slot = slot;
                        this.slotId = slot.Id;

                        return true;
                    }
                };

                controller.init();

                return controller;
            };

        $scope.slotsController =
            createController(
                $scope,
                slotsService,
                notificationService);
    });