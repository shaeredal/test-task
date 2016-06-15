var app = angular.module('OnlinerNotifier',
[
    'ngRoute',
    'OnlinerNotifier.Auth'
]);

app.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider.when('/auth', {
            templateUrl: '/app/views/auth.html',
            controller: 'AuthCtrl'
        });
}] )