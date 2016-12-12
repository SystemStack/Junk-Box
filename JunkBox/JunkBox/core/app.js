angular.module("junkBox", [
    "ngAnimate",
    "ngMaterial",
    "ui.router",
    "junkBox.controllers",
    "junkBox.filters",
    "junkBox.services",
    "junkBox.directives",
    "ngResource",
    "ngSanitize"
])
    .config(function ($stateProvider, $urlRouterProvider, $mdThemingProvider) {
        $mdThemingProvider.theme('default')
            .primaryPalette('cyan')
            .accentPalette('green');

        $stateProvider
            .state("login", {
                url: "/login",
                templateUrl: "/features/login/login.html",
                controller: "loginCtrl"
            })
            .state("home", {
                url: "/home",
                templateUrl: "/features/home/home.html",
                controller: "homeCtrl"
            })
            .state("queries", {
                url: "/queries",
                templateUrl: "/features/queries/queries.html",
                controller: "queriesCtrl"
            })
            .state("preferences", {
                url: "/preferences",
                templateUrl: "/features/preferences/preferences.html",
                controller: "preferencesCtrl"
            })
            .state("ebay", {
                url: "/ebay",
                templateUrl: "/features/ebay/ebay.html",
                controller: "ebayCtrl"
            })
            .state("orderHistory", {
                url: "/orderHistory",
                templateUrl: "/features/orderHistory/orderHistory.html",
                controller: "orderHistoryCtrl"
            });
        $urlRouterProvider.otherwise("/home");
    }).run(function ($rootScope, $http) {
        if (!sessionStorage.email) {
            delete sessionStorage;
            window.location.assign("features/login/login.html");
        }    
    }).controller("MainCtrl", function ($scope, $mdToast, $rootScope, $interval, Ebay) {
        $scope.time = function () {
            return $interval(function () {
                console.log("Processing 'Daily' Ebay items");
                Ebay.ebayDailyPurchases().then(function (resolve) {
                    console.log(resolve);
                }).catch(function (reject) {
                    console.log(reject);
                });
            }, 300000);
        }();

        $scope.checkSessionStorage = function () {
            if (sessionStorage.email) {
                $rootScope.email = sessionStorage.email;
            } else {
                $scope.displayToUser("Please log in");
                delete sessionStorage;
                window.location.assign("features/login/login.html");
            }
        }();

        $scope.goHome = function () {
            $urlRouterProvider.otherwise("/login");
        };

        $scope.displayToUser = function (text, milliseconds) {
            $mdToast.show(
                $mdToast.simple()
                .textContent(text)
                .hideDelay(milliseconds || 3000)
            );
        };

        $scope.logout = function () {
            delete sessionStorage;
            window.location.assign("features/login/login.html");
        };

    });