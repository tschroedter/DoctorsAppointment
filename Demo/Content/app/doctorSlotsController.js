mainApp.controller("doctorSlotsController",
    function($scope,
        doctorSlotsService) {

        var loading = {
            FirstName: "Loading",
            LastName: "...",
            Id: -1
        };

        $scope.init = function() {
        };

        /* BEGIN: Handlers */

        var handleSearchResult = function(data) {
            $scope.searchResult = angular.fromJson(data);
            alert("Searched!");;
        };

        /* END: Handlers */

        /* BEGIN: CRUD */

        $scope.search = function() {
            doctorSlotsService.search(
                $scope.searchByDoctorId,
                $scope.searchByDate,
                $scope.searchByStatus,
                handleSearchResult); // todo use object instead of list
        };

        /* END: CRUD */

        $scope.searchResult = [loading];
        $scope.searchByDoctorId = 1;
        $scope.searchByDate = "2015-06-30";
        $scope.searchByStatus = "open";

        $scope.init();
    });