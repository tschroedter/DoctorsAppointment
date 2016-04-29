'use strict';

var nancyBaseUri = "/";
var mainApp = angular.module("mainApp", ["ngResource"]);

mainApp.factory("doctors", function ($resource) {
    return $resource(nancyBaseUri + "doctors/:id", { id: "@_id" }, {
        update: {
            method: "PUT"
        }
    });
});

mainApp.factory("doctorsSearchByLastName", function ($resource) {
    return $resource(nancyBaseUri + "doctors/byLastName/:query", {
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

mainApp.factory("slots", function ($resource) {
    return $resource(nancyBaseUri + "slots/:id", { id: "@_id" }, {
        update: {
            method: "PUT"
        }
    });
});

mainApp.factory("days", function ($resource) {
    return $resource(nancyBaseUri + "days/:id", { id: "@_id" }, {
        update: {
            method: "PUT"
        }
    });
});

mainApp.factory("daysSearchByDoctorId", function ($resource) {
    return $resource(nancyBaseUri + "days/doctorId/:query", {
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
    return $resource(nancyBaseUri + "slots/dayId/:query", {
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

mainApp.factory("doctorSlotsSearch", function ($resource) {
    return $resource(nancyBaseUri + "doctors/:id/slots", {
        query: "@query"
    }, {
        search: {
            method: "GET",
            isArray: true,
            params: {
                id: "@id",
                date: "@date",
                status: "@status"
            }
        }
    });
});

mainApp.factory("daysService", function (days, daysSearchByDoctorId) {
    var instance = {};

    instance.days = days;
    instance.daysSearchByDoctorId = daysSearchByDoctorId;

    /* BEGIN: CRUD */

    instance.query = function (controller, doSomething) {
        instance.days.query(
            function (data) {
                doSomething(controller, data);
            });
    };

    instance.get = function (id, controller, doSomething) {
        instance.days.get(
            {
                id: id
            },
            function (data) {
                doSomething(controller, data);
            });
    };

    instance.save = function (day, controller, doSomething) {
        instance.days.save(
            day,
            function (data) {
                doSomething(controller, data);
            });
    };

    instance.update = function (day, controller, doSomething) {
        instance.days.update(
            day,
            function (data) {
                doSomething(controller, data);
            });
    };

    instance.delete = function (day, controller, doSomething) {
        instance.days.delete(
            day,
            function (data) {
                doSomething(controller, data);
            });
    };

    instance.getByDoctorId = function (doctorId, controller, doSomething) {
        instance.daysSearchByDoctorId.search({
            query: doctorId
        },
            function (data) {
                doSomething(controller, data);
            });
    };

    /* END: CRUD */

    return instance;
});

mainApp.factory("doctorsService", function (doctors, doctorsSearchByLastName) {
    var instance = {};

    instance.doctors = doctors;
    instance.doctorsSearchByLastName = doctorsSearchByLastName;

    instance.compareByLastName = function (a, b) {
        if (a.LastName < b.LastName)
            return -1;
        if (a.LastName > b.LastName)
            return 1;
        return 0;
    };

    /* BEGIN: CRUD */

    instance.query = function (controller,
                               doSomething) {
        instance.doctors.query(
            function (data) {
                doSomething(controller, data);
            });
    };

    instance.get = function (id,
                             controller,
                             doSomething) {
        instance.doctors.get(
            {
                id: id
            },
            function (data) {
                doSomething(controller, data);
            });
    };

    instance.save = function (doctor,
                              controller,
                              doSomething) {
        instance.doctors.save(
            doctor,
            function (data) {
                doSomething(controller, data);
            });
    };

    instance.update = function (doctor,
                                controller,
                                doSomething) {
        instance.doctors.update(
            doctor,
            function (data) {
                doSomething(controller, data);
            });
    };

    instance.delete = function (doctor,
                                controller,
                                doSomething) {
        instance.doctors.delete(
            doctor,
            function (data) {
                doSomething(controller, data);
            });
    };

    instance.search = function (searchByLastName,
                                controller,
                                doSomething) {
        instance.doctorsSearchByLastName.search(
            {
                query: searchByLastName
            },
            function (data) {
                doSomething(controller, data);
            });
    };

    /* END: CRUD */

    return instance;
});

mainApp.factory("slotsService", function (slots, slotsSearchByDayId) {
    var instance = {};

    instance.slots = slots;
    instance.slotsSearchByDayId = slotsSearchByDayId;

    /* BEGIN: CRUD */

    instance.query = function (controller,
                               doSomething) {
        instance.slots.query(
            function (data) {
                doSomething(controller, data);
            });
    };

    instance.get = function (id,
                             controller,
                             doSomething) {

        instance.slots.get(
            {
                id: id
            },
            function (data) {
                doSomething(controller, data);
            });
    };

    instance.save = function (slot,
                              controller,
                              doSomething) {
        instance.slots.save(
            slot,
            function (data) {
                doSomething(controller, data);
            });
    };

    instance.update = function (slot,
                                controller,
                                doSomething) {
        instance.slots.update(
            slot,
            function (data) {
                doSomething(controller, data);
            });
    };

    instance.delete = function (slot,
                                controller,
                                doSomething) {
        instance.slots.delete(
            slot,
            function (data) {
                doSomething(controller, data);
            });
    };

    instance.search = function (searchByDayId,
                                controller,
                                doSomething) {
        instance.slotsSearchByDayId.search(
            {
                query: searchByDayId
            },
            function (data) {
                doSomething(controller, data);
            });
    };

    /* END: CRUD */

    return instance;
});

mainApp.factory("doctorSlotsService", function (doctorSlotsSearch) {
    var instance = {};

    instance.doctorSlotsSearch = doctorSlotsSearch;

    /* BEGIN: CRUD */

    // todo how to add searchByDate
    instance.search = function (searchByDoctorId,
                                searchByDate,
                                searchStatus,
                                controller,
                                doSomething) {

        instance.doctorSlotsSearch.search(
            {
                id: searchByDoctorId,
                date: searchByDate,
                status: searchStatus
            },
            function (data) {
                doSomething(controller, data);
            });
    };

    /* END: CRUD */

    return instance;
});

mainApp.filter("byDate", function () {

    return function (input, byDate) {

        var stringStartsWidth = function (string, prefix) {
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

        angular.forEach(input, function (dateTime) {

            if (stringStartsWidth(dateTime.Date, byDate)) {
                out.push(dateTime);
            }
        });

        return out;
    };
});

mainApp.factory("notificationService", function () {
    return {
        alert: function (message) {
            alert(message);
        }
    };
});
