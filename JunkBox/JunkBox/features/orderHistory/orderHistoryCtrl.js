angular
.module('junkBox.controllers.orderHistoryCtrl', [])
.controller('orderHistoryCtrl', function ($scope, $rootScope, OrderHistory) {
    var customerData = {
        email: $rootScope.email
    };

    $scope.orders = {
        orderList: {}
    };

    $scope.getOrders = function () {
        OrderHistory.getCustomerOrders(customerData).then(function (data) {
            console.log(data);
            $scope.orders.orderList = data;
        });
    }();

});