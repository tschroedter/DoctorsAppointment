var mainApp = angular.module("mainApp", ["ngResource"]);

mainApp.factory("doctors", function($resource) {
    return $resource("/doctors/:id", { id: "@_id" }, {
        update: {
            method: "PUT"
        }
    });
});

mainApp.factory("doctorsSearchByLastName", function($resource) {
    return $resource("/doctors/byLastName/:query", {
        query: "@query"
    }, {
        search: {
            method: "GET",
            isArray: true,
            params: {
                query: "@query"
            }
        }
    });
});

mainApp.factory("slots", function($resource) {
    return $resource("/slots/:id", { id: "@_id" }, {
        update: {
            method: "PUT"
        }
    });
});

mainApp.factory("days", function($resource) {
    return $resource("/days/:id", { id: "@_id" }, {
        update: {
            method: "PUT"
        }
    });
});

mainApp.factory("daysSearchByDoctorId", function ($resource) {
    return $resource("/days/doctorId/:query", {
        query: "@query"
    }, {
        search: {
            method: "GET",
            isArray: true,
            params: {
                query: "@query"
            }
        }
    });
});
