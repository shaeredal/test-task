'use strict';
var account = angular.module('onlinerNotifier.account', ['ngRoute']);

account.controller('accountController',
    function($scope, $http, $cookies, $filter) {
        var userId = $cookies.get('User');
        $http.get('api/Account/' + userId)
            .then(function(response) {
                var user = response.data;
                $scope.name = user.FirstName + ' ' + user.LastName;
                $scope.avatarUri = user.AvatarUri;
                $scope.userProducts = user.UserProducts;
                $scope.email = user.Email;
            });

        $scope.currencies = [{ name: "BYB", rate: 1 }];
        $http.get('https://www.nbrb.by/API/ExRates/Rates/145?onDate=' +
                $filter('date')(Date.now(), 'EEE%2C+dd+MMM+yyyy+21%3A00%3A00') +
                '&Periodicity=0&Cur_ID=145')
            .then(function(response) {
                $scope.currencies.push({ name: "USD", rate: response.data.Cur_OfficialRate });
            });

        $scope.delProduct = function(id, index) {
            $http.delete('api/Account/' + id)
                .then(function(response) {
                    $scope.userProducts.splice(index, 1);
                });
        };

        $scope.setTime = function() {
            if ($scope.NotificationForm.$valid) {
                $http.post('api/Notification/', { 'Time': $scope.time, Email: $scope.email });
            }
        }

        $scope.changeTrackingStatus = function(id, index) {
            var isTracked = $scope.userProducts[index].IsTracked;
            $http.post('api/Tracking', { 'id': id, 'status': !isTracked })
                .then(function(response) {
                    if (response.status == 200) {
                        $scope.userProducts[index].IsTracked = !isTracked;
                    }
                });
        }
    });



