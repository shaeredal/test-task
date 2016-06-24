'use strict';
angular.module('onlinerNotifier.account', ['ngRoute'])
    .controller('accountController', function ($scope, $http, $cookies) {
        var userId = $cookies.get('User');
        $http.get('api/Account/' + userId)
            .then(function (response) {
                var user = response.data;
                $scope.name = user.FirstName + ' ' + user.LastName;
                $scope.avatarUri = user.AvatarUri;
                $scope.userProducts = user.UserProducts;
                $scope.email = user.Email;
            });

        $scope.delProduct = function (id, index) {
            $http.delete('api/Account/' + id)
                .then(function(response) {
                    $scope.userProducts.splice(index, 1);
                });
        };

        $scope.setTime = function () {
            if ($scope.NotificationForm.$valid) {
                $http.post('api/Notification/', { 'Time': $scope.time, Email: $scope.email });
            }
        }

        $scope.changeTrackingStatus = function (id, index) {
            var isTracked = $scope.userProducts[index].IsTracked;
            $http.post('api/Tracking', { 'id': id, 'status': !isTracked })
                .then(function(response) {
                    //TODO: change status
                    console.log(response);
                    if (response.status == 200) {
                        $scope.userProducts[index].IsTracked = !isTracked;
                    }
                });
        }
    });