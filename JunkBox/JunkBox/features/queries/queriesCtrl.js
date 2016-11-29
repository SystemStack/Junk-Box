angular
.module('junkBox.controllers.queriesCtrl', [])
.controller('queriesCtrl', function ($scope, Queries, $rootScope, Ebay) {
    $scope.query = {
        email: $rootScope.email,
        frequencyOptions: {}
    };
    $scope.frequencyOptions = [{
        value: 0,
        label: "Never"
    }, {
        value: 1,
        label: "Daily"
    }, {
        value: 7,
        label: "Weekly"
    }, {
        value: 30,
        label: "Monthly"
    }];
    $scope.sliderUpdate = function () {

    }
    $scope.categories = {
        "All Categories": "-1",
        "Antiques": "-1",
        "Art": "-1",
        "Baby": "-1",
        "Books": "-1",
        "Business and Industrial": "-1",
        "Cameras and Photo": "-1",
        "Cell Phones and Accessories": "-1",
        "Clothing, Shoes and Accessories": "-1",
        "Coins and Paper Money": "-1",
        "Collectibles": "-1",
        "Computers/Tablets and Networking": "-1",
        "Consumer Electronics": "-1",
        "Crafts": "-1",
        "Dolls and Bears": "-1",
        "DVDs and Movies": "-1",
        "eBay Motors": "-1",
        "Entertainment Memorabilia": "-1",
        "Gift Cards and Coupons": "-1",
        "Health and Beauty": "-1",
        "Home and Garden": "-1",
        "Jewelry and Watches": "-1",
        "Music": "-1",
        "Musical Instruments and Gear": "-1",
        "Pet Supplies": "-1",
        "Pottery and Glass": "-1",
        "Real Estate": "-1",
        "Specialty Services": "-1",
        "Sporting Goods": "-1",
        "Sports Mem, Cards and Fan Shop": "-1",
        "Stamps": "-1",
        "Tickets and Experiences": "-1",
        "Toys and Hobbies": "-1",
        "Travel": "-1",
        "Video Games and Consoles": "-1",
        "Everything Else": "-1"
    };

    $scope.getQuerySettings = function () {
        var userData = {
            email: $rootScope.email
        }

        Ebay.getAllCategories().then(function (success) {
            var newList = {};
            success.CategoryArray.Category.forEach(function (element) {
                newList[element.CategoryName] = element.CategoryID;
            });
            $scope.categories = newList;
        }, function (error) {
            $scope.showResponse("There was an error getting your query preferences");
            console.log(error);
        }).finally(function () {
            Queries.getSettings(userData).then(function (data) {
                console.log(data);
                var userSettings        = data.result;
                $scope.query.category   = userSettings.Category;
                $scope.query.categoryId = $scope.categories[userSettings.Category];
                $scope.query.price      = userSettings.PriceLimit;
                $scope.query.frequencyOptions = $scope.frequencyOptions.find(function (obj) {
                    return obj.label.toUpperCase() === userSettings.Frequency.toUpperCase();
                });
            });
        });
    }();

    function queriesGetSettings() {

    }

    function QueriesGetSettingsSuccess() {

    }

    $scope.send = function () {
        var verifyValidData = function (e) {
            console.log(e);
            if (!e) {
                $scope.displayToUser("Data is invalid");
            } else if (e.price < 1.00 || e.price > 5.00) {
                $scope.displayToUser("Price is invalid");
            } else {
                var haltData = {
                    action: (e.frequencyOptions.label === "Never")
                };
                Queries.haltPurchases(haltData).then(function(data) {
                    $scope.displayToUser((haltData.action)
                        ?"We have stopped all purchases on your account"
                        :"Your query preferences have changed successfully");
                }, function(error){
                    $scope.displayToUser("Something went wrong, try again or contact support",5000);
                });
                return true;
            }
            return false;
        };
        if (verifyValidData($scope.query)) {
            Queries.send($scope.query).then(function (data) {
                console.log(data);
            });
            console.log($scope.query);
            $scope.submittedRecord = true;
        }
    };

    $scope.updateCategory = function () {
        console.log($scope.query.category);
        $scope.query.categoryId = $scope.categories[$scope.query.category];
        console.log($scope.query.categoryId);
    };

});