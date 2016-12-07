angular
.module('junkBox.controllers.orderHistoryCtrl', [])
.controller('orderHistoryCtrl', function ($scope, $rootScope, OrderHistory) {
    var customerData = {
        email: $rootScope.email
    };

    $scope.orders = {
        orderList: {}
    };

    $scope.onPageLoad = function () {
        OrderHistory.getCustomerOrders(customerData).then(function (data) {
            console.log(data);
            $scope.orders.orderList = data;
        });
    }();

    $scope.getSessionData = function(checkoutSessionId) {
        var payload = {
            checkoutSessionId: checkoutSessionId
        };
        OrderHistory.getGuestCheckoutSession(payload).then(function (result) {
            console.log(result);
            for (var i = 0; i < $scope.orders.orderList.length; i++) {
                if($scope.orders.orderList[i].CheckoutSessionID === checkoutSessionId) {
                    $scope.orders.orderList[i].checkoutSessionDetails = result;
                }
            }
        });
    }
});