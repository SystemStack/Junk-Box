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

    $scope.executeGetTest = function () {
        console.log("EXECUTE GET TEST RESULT!");
        Ebay.getTest().then(function (success) {
            console.log(success);
            $scope._cb_findItemsByKeywords(success);
        }, function (failure) {
            console.log(failure);
        });
    };

    $scope.getCategoriesTest = function () {
        console.log("Attemtping to get categories from Ebay:");
        Ebay.getAllCategories().then(function (success) {
            console.log(success);
        }, function (failure) {
            console.log(failure);
        });
    };

    /*
    $scope.getTimestamp = function () {
        Ebay.getSomething().then(function (data) {
            console.log(data);
            $scope.stuff.timestamp = data["result"];
            console.log($scope.stuff.timestamp);
        }, function (reject) {
            console.log(reject);
        });
    }();*/

    /*
    $scope.getViablePurchases = function () {

        Ebay.getViablePurchases($scope.stuff).then(function (success) {
            console.log(success);
            $scope._cb_findItemsByKeywords(success);
        }, function (failure) {
            console.log(failure);
        });
    }();*/

    $scope.browseApiTest = function () {

        Ebay.ebayBrowseApiTest($scope.stuff).then(function (success) {
            console.log(success);
            //$scope._cb_findItemsBrowseApi(success);
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

    //Fucking hell, they had this whole script on one fucking line.
    $scope._cb_findItemsByKeywords = function (root) {
        var items = root &&
                    root.findItemsByCategoryResponse &&
                    root.findItemsByCategoryResponse[0] &&
                    root.findItemsByCategoryResponse[0].searchResult &&
                    root.findItemsByCategoryResponse[0].searchResult[0] &&
                    root.findItemsByCategoryResponse[0].searchResult[0].item ||
                    [];
        var html = []; html.push("<table width='100%' border='0' cellspacing='0' cellpadding='3'><tbody>");
        for (var i = 0; i < items.length; ++i) {
            var item = items[i];

            var shippingInfo = item.shippingInfo &&
                item.shippingInfo[0] ||
                {};

            var sellingStatus = item.sellingStatus &&
                item.sellingStatus[0] ||
                {};

            var listingInfo = item.listingInfo &&
                item.listingInfo[0] ||
                {};

            var title = item.title;

            var subtitle = item.subtitle ||
                '';

            var pic = item.galleryURL;

            var viewitem = item.viewItemURL;

            var currentPrice = sellingStatus.currentPrice &&
                sellingStatus.currentPrice[0] ||
                {};

            var displayPrice = currentPrice['@currencyId'] + ' ' + currentPrice['__value__'];

            var buyItNowAvailable = listingInfo.buyItNowAvailable &&
                listingInfo.buyItNowAvailable[0] === 'true';

            var freeShipping = shippingInfo.shippingType &&
                shippingInfo.shippingType[0] === 'Free';

            if (null !== title && null !== viewitem) {
                html.push("<tr><td class='image-container'><img src='" + pic + "'border = '0'></td>");
                html.push('<td class="data-container"><a class="item-link" href="' + viewitem + '"target="_blank">');
                html.push('<p class="title">' + title + '</p>');
                html.push('<p class="subtitle">' + subtitle + '</p>');
                html.push('<p class="price">' + displayPrice + '</p>');
                if (buyItNowAvailable) {
                    html.push('<p class="bin">Buy It Now</p>');
                }
                if (freeShipping) {
                    html.push('<p class="fs">Free shipping</p>');
                }
                html.push('</a></td></tr>');
            }
        }
        html.push("</tbody></table>");
        $scope.stuff.html = html.join("");
        console.log(html);
    }

    $scope._cb_findItemsBrowseApi = function (root) {
        var items = root &&
                    root.itemSummaries;

        var html = []; html.push("<table width='100%' border='0' cellspacing='0' cellpadding='3'><tbody>");
        for (var i = 0; i < items.length; ++i) {
            var item = items[i];

            var shippingInfo = item.shippingOptions &&
                item.shippingOptions[0] ||
                {};

            var sellingStatus = item.buyingOptions &&
                item.buyingOptions[0] ||
                {};

            var listingInfo = item.listingInfo &&
                item.listingInfo[0] ||
                {};

            var title = item.title;

            var subtitle = item.subtitle ||
                '';

            var pic = item.image.imageUrl;

            var viewitem = item.itemGroupHref;

            var currentPrice = item.price.value;

            var displayPrice = item.price.currency + ' ' + currentPrice;

            var buyItNowAvailable = listingInfo.buyItNowAvailable &&
                listingInfo.buyItNowAvailable[0] === 'true';

            var freeShipping = shippingInfo.shippingType &&
                shippingInfo.shippingType[0] === 'Free';

            if (null !== title && null !== viewitem) {
                html.push("<tr><td class='image-container'><img src='" + pic + "'border = '0'></td>");
                html.push('<td class="data-container"><a class="item-link" href="' + viewitem + '"target="_blank">');
                html.push('<p class="title">' + title + '</p>');
                html.push('<p class="subtitle">' + subtitle + '</p>');
                html.push('<p class="price">' + displayPrice + '</p>');
                if (buyItNowAvailable) {
                    html.push('<p class="bin">Buy It Now</p>');
                }
                if (freeShipping) {
                    html.push('<p class="fs">Free shipping</p>');
                }
                html.push('</a>');
                html.push('<p><md-button ng-click="forceBuy("'+ item.itemId +'")">Force Buy Item</md-button></p>');
                html.push('</td></tr>');
            }
        }
        html.push("</tbody></table>");
        $scope.stuff.html = html.join("");
        console.log(html);
    }
});