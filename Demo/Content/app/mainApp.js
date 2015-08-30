﻿var mainApp = angular.module('mainApp', ['ngResource']);

mainApp.factory('doctors', function ($resource) {
    return $resource('/doctors/:id', { id: '@_id' }, {
        update: {
            method: 'PUT'
        }
    });
});

mainApp.factory('doctorsSearch', function ($resource) {
    return $resource('/doctors/:query', {
        query: '@query'
    }, {
        search: {
            method: 'GET',
            isArray: true,
            params: {
                query: '@query'
            }
        }
    });
});