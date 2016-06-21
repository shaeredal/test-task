'use strict';
angular.module('onlinerNotifier.account', ['ngRoute'])
    .controller('accountController', function ($scope, $http, $cookies) {
        var userId = $cookies.get('User');
        $http.get('api/Account/' + userId)
            .then(function (response) {
                var user = response.data;
                $scope.name = user.FirstName + ' ' + user.LastName;
                $scope.avatarUri = user.AvatarUri;
                $scope.products = user.Products;
            });

        $scope.delProduct = function (id) {
            //temporary
            console.log("id: " + id);
        };

        $scope.setTime = function () {
            //temporary
            console.log("time: " + $scope.time);
        }
    });