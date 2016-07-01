'use strict';
var home = angular.module('onlinerNotifier.home', ['ngRoute', 'infinite-scroll']);

home.controller('homeController', function ($scope, $http, $cookies, $filter) {
        toastr.options = {
            "closeButton": true,
            "positionClass": "toast-bottom-right"
        }

        $scope.toastHub = $.connection.toastHub;
        $scope.toastHub.client.showAddToast = function (text) {
            console.log(text);
            toastr.info(text);
        };
        $scope.toastHub.client.showDeleteToast = function (text) {
            console.log(text);
            toastr.info(text);
            
        };
        $.connection.hub.start().done(function () {

        });

        $scope.updateInfo = function() {
            var userId = $cookies.get('User');
            $http.get('api/Account/' + userId)
                .then(function (response) {
                    var user = response.data;
                    $scope.name = user.FirstName + ' ' + user.LastName;
                    $scope.avatarUri = user.AvatarUri.replace("http://", "https://");
                    $scope.userProducts = user.UserProducts;
                    $scope.email = user.Email;

                    $scope.trackedIds = function (){
                        var idList = [];
                        for (let up of $scope.userProducts) {
                            idList.push(up.Product.OnlinerId);
                        }
                        return idList;
                    }();
                });
        }
        $scope.updateInfo();
        

        $scope.search = function() {
            $http.get('https://catalog.api.onliner.by/search/products?query=' + $scope.searchQuery)
                .then(function(response) {
                    $scope.products = response.data.products;
                    $scope.lastPage = response.data.page.current;
                });
        }

        $scope.currencies = [{ name: "BYB", rate: 1 }];
        $scope.currencies.push({ name: "BYN", rate: 10000 });
        $http.get('https://www.nbrb.by/API/ExRates/Rates/145?onDate=' +
                $filter('date')(Date.now(), 'EEE%2C+dd+MMM+yyyy+21%3A00%3A00') +
                '&Periodicity=0&Cur_ID=145')
            .then(function (response) {
                $scope.currencies.push({ name: "USD", rate: response.data.Cur_OfficialRate * 10000 });
            });
        $scope.currentRate = $scope.currencies[0].rate;

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
            $scope.toastHub.server.getAddToast("productname"); //temporary
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
                    $scope.updateInfo();
                }, function(response) {
                    alert('"'+product.full_name+'"'+" is not added.");
                });
        }

        $scope.delProduct = function (onlinerId) {
            $scope.toastHub.server.getDeleteToast("productname"); //temporary
            var id;
            for (let up of $scope.userProducts) {
                if (up.Product.OnlinerId == onlinerId) {
                    id = up.Product.Id;
                }
            }
            $http.delete('api/Account/' + id)
                .then(function (response) {
                    $scope.updateInfo();
                });
        };
    });