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

mainApp.factory("daysService", function(days, daysSearchByDoctorId) {
    var instance = {};

    instance.days = days;
    instance.daysSearchByDoctorId = daysSearchByDoctorId;

    /* BEGIN: CRUD */

    instance.query = function(doSomething) {
        instance.days.query(doSomething);
    };

    instance.get = function(dayId) {
        return instance.days.get({ id: dayId });
    };

    instance.save = function(day, doSomething) {
        instance.days.save(day, doSomething);
    };

    instance.update = function(day, doSomething) {
        instance.days.update(day, doSomething);
    };

    instance.delete = function(day, doSomething) {
        instance.days.delete(day, doSomething);
    };

    instance.getByDoctorId = function(doctorId, doSomething) {
        instance.daysSearchByDoctorId.search({
            query: doctorId
        }, doSomething);
    };

    /* END: CRUD */

    return instance;
});

mainApp.factory("doctorsService", function(doctors, doctorsSearchByLastName) {
    var instance = {};

    instance.doctors = doctors;
    instance.doctorsSearchByLastName = doctorsSearchByLastName;

    instance.compareByLastName = function(a, b) {
        if (a.LastName < b.LastName)
            return -1;
        if (a.LastName > b.LastName)
            return 1;
        return 0;
    };

    /* BEGIN: CRUD */

    instance.query = function(doSomething) {
        instance.doctors.query(doSomething);
    };

    instance.get = function(doctorId) {
        return instance.doctors.get({ id: doctorId });
    };

    instance.save = function(doctor, doSomething) {
        instance.doctors.save(doctor, doSomething);
    };

    instance.update = function(doctor, doSomething) {
        instance.doctors.update(doctor, doSomething);
    };

    instance.delete = function(doctor, doSomething) {
        instance.doctors.delete(doctor, doSomething);
    };

    instance.search = function(searchByLastName, doSomething) {
        instance.doctorsSearchByLastName.search({
            query: searchByLastName
        }, doSomething);
    };

    /* END: CRUD */

    return instance;
});