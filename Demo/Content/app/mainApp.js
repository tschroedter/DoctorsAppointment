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

mainApp.factory("slotsSearchByDayId", function ($resource) {
    return $resource("/slots/dayId/:query", {
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

    instance.get = function(id, doSomething) {
        doSomething(instance.days.get({ id: id }));
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

    instance.get = function(id, doSomething) {
        doSomething(instance.doctors.get({ id: id }));
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

// todo check, all services look simillar
mainApp.factory("slotsService", function(slots, slotsSearchByDayId) {
    var instance = {};

    instance.slots = slots;
    instance.slotsSearchByDayId = slotsSearchByDayId;

    /* BEGIN: CRUD */

    instance.query = function(doSomething) {
        instance.slots.query(doSomething);
    };

    instance.get = function(id, doSomething) {
        doSomething(instance.slots.get({ id: id }));
    };

    instance.save = function(slot, doSomething) {
        instance.slots.save(slot, doSomething);
    };

    instance.update = function(slot, doSomething) {
        instance.slots.update(slot, doSomething);
    };

    instance.delete = function(slot, doSomething) {
        instance.slots.delete(slot, doSomething);
    };

    instance.search = function (searchByDayId, doSomething) {
        instance.slotsSearchByDayId.search({
            query: searchByDayId
        }, doSomething);
    };

    /* END: CRUD */

    return instance;
});

mainApp.filter("byDate", function() {

    return function(input, byDate) {

        var stringStartsWidth = function(string, prefix) {
            if (typeof string === "string" &&
                typeof prefix === "string") {
                return string.slice(0, prefix.length) === prefix;
            }

            return false;
        };
        var out = [];

        if (input === "-1" ||
            byDate === "") {
            return out;
        }

        angular.forEach(input, function(dateTime) {

            if (stringStartsWidth(dateTime.Date, byDate)) {
                out.push(dateTime);
            }
        });

        return out;
    };
});