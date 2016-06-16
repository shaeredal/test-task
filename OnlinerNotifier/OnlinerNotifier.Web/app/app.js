var app = angular.module('onlinerNotifier',
[
    'ngRoute',
    'onlinerNotifier.auth'
]);

app.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider.when('/auth', {
            templateUrl: '/app/views/auth.html',
            controller: 'authCtrl'
        });
}] )