﻿'use strict';
angular.module('onlinerNotifier.auth', ['ngRoute'])
    .controller('authController', function ($scope, $http) {
        $http.get('Authorization/GetAuthUrl?providerName=Twitter')
            .then(function(response) {
                $scope.twitterLink = response.data.Url;
            });
    });
