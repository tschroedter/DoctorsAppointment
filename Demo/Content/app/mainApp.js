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

mainApp.factory("daysSearchByDoctorId", function($resource) {
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

mainApp.factory("doctorsService", function(doctors, doctorsSearchByLastName) {
    var instance = {};

    instance.doctorsResource = doctors;
    instance.doctorsSearchByLastNameResource = doctorsSearchByLastName;

    instance.compareByLastName = function(a, b) {
        if (a.LastName < b.LastName)
            return -1;
        if (a.LastName > b.LastName)
            return 1;
        return 0;
    };

    /* BEGIN: CRUD */

    instance.query = function(doSomething) {
        instance.doctorsResource.query(doSomething);
    };

    instance.get = function(doctorId) {
        return instance.doctorsResource.get({ id: doctorId });
    };

    instance.save = function(doctor, doSomething) {
        instance.doctorsResource.save(doctor, function() {
            doSomething();
        });
    };

    instance.update = function(doctor, doSomething) {
        instance.doctorsResource.update(doctor, doSomething);
    };

    instance.delete = function(doctor, doSomething) {
        instance.doctorsResource.delete(doctor, doSomething);
    };

    instance.search = function(searchByLastName, doSomething) {
        instance.doctorsSearchByLastNameResource.search({
            query: searchByLastName
        }, doSomething);
    };

    /* END: CRUD */

    return instance;
});