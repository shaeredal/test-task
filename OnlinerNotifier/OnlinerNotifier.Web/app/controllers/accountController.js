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

        $scope.delProduct = function (id, index) {
            $http.delete('api/Account/' + id)
                .then(function(response) {
                    $scope.products.splice(index, 1);
                });
        };

        $scope.setTime = function () {
            if ($scope.time) {
                $http.post('api/Notification/', $scope.time);
            }
        }
    });