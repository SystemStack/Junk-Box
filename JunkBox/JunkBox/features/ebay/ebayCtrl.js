angular
.module('junkBox.controllers.ebayCtrl', [])
.controller('ebayCtrl', function ($scope, Ebay, $rootScope) {
    $scope.stuff = {
        email: $rootScope.email,
        html: "",
        timestamp: "",
        orderId: "",
        imageUrl: ""
    };

    $scope.ebayResponse = {};

    $scope.browseApiFindItems = function () {

        Ebay.ebayBrowseApiFindViableItems($scope.stuff).then(function (success) {
            console.log(success);
            $scope.ebayResponse = success;
        }, function (failure) {
            console.log(failure);
        });
    }();

    $scope.forceBuy = function (itemId, imgUrl) {
        console.log(itemId);
        $scope.stuff.orderId = itemId;
        $scope.stuff.imageUrl = imgUrl;
        Ebay.ebayOrderApiInitiateGuestCheckoutSession($scope.stuff).then(function (success) {
            console.log(success);
        }, function (failure) {
            console.log(failure);
        });
    };


});