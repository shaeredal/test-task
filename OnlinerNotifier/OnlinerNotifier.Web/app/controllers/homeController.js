'use strict';
angular.module('onlinerNotifier.home', ['ngRoute'])
    .controller('homeController', function ($scope, $http, $cookies) {
        var userId = $cookies.get('User');
        $http.get('api/User?' + userId)
            .then(function(response) {
                var user = response.data;
                $scope.name = user.FirstName + ' ' + user.LastName;
                $scope.avatarUri = user.AvatarUri;
            });
    });