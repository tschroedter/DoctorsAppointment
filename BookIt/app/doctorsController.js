mainApp.controller("doctorsController", function ($scope, doctors) {
    doctors.query(function (data) {
        $scope.doctors = JSON.parse(data);
    });
});

/*
app.controller("PostsCtrl", function ($scope, $http) {
    $http.get('data/posts.json').
      success(function (data, status, headers, config) {
          $scope.posts = data;
      }).
      error(function (data, status, headers, config) {
          // log error
      });
});
*/