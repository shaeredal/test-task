'use strict';
angular.module('onlinerNotifier.home', ['ngRoute', 'infinite-scroll'])
    .controller('homeController', function ($scope, $http, $cookies) {
        var userId = $cookies.get('User');
        $http.get('api/User/' + userId)
            .then(function(response) {
                var user = response.data;
                $scope.name = user.FirstName + ' ' + user.LastName;
                $scope.avatarUri = user.AvatarUri;
            });

        $scope.search = function() {
            $http.get('https://catalog.api.onliner.by/search/products?query=' + $scope.searchQuery)
                .then(function(response) {
                    $scope.products = response.data.products;
                    $scope.lastPage = response.data.page.current;
                });
        }

        $scope.more = function() {
            $scope.lastPage += 1;
            $http.get('https://catalog.api.onliner.by/search/products?query=' +
                    $scope.searchQuery +
                    "&page=" +
                    $scope.lastPage)
                .then(function (response) {
                    $scope.products = $scope.products.concat(response.data.products);
                });
        }

        $scope.addProduct = function (id) {

            var productMatch = $scope.products.filter(function(prod) {
                return prod.id == id;
            });
            if (productMatch.length != 1) {
                return;
            }
            var product = productMatch[0];
            var productData = {
                "OnlinerId": product.id,
                "Name": product.full_name,           
            };
            if (product.prices) {
                productData["MaxPrice"] = product.prices.max;
                productData["MinPrice"] = product.prices.min;
            }
            $http.post("api/Product", productData)
                .then(function(response) {
                    alert('"' + product.full_name + '"' + " is added in the track list.");
                }, function(response) {
                    alert('"'+product.full_name+'"'+" is alredy in the track list.");
                });
        }
    });