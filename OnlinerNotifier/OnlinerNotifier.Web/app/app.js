var app = angular.module('onlinerNotifier',
[
    'ngRoute',
    'ngCookies',
    'ui.bootstrap',
    'onlinerNotifier.auth',
    'onlinerNotifier.account',
    'onlinerNotifier.home',
    notificationModule
]);

app.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider.when('/auth', {
            templateUrl: '/app/views/auth.html',
            controller: 'authController'
        })
        .when('/account', {
            templateUrl: '/app/views/account.html',
            controller: 'accountController'
        })
        .when('/home', {
            templateUrl: '/app/views/home.html',
            controller: 'homeController'
        })
        .otherwise({
            redirectTo: '/auth'    
        });
}] )