account.filter('currencyFilter', function () {
    return function (x, currentRate) {
        return Math.round(x / currentRate);
    }
});