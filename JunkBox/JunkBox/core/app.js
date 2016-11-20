angular.module("junkBox",
  [
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
        });
      $urlRouterProvider.otherwise("/home");
  })
  .controller("MainCtrl", function ($scope, $mdToast) {
      $scope.goHome = function () {
          $urlRouterProvider.otherwise("/home");
      };

      $scope.displayToUser = function (text, milliseconds) {
          $mdToast.show(
          $mdToast.simple()
            .textContent(text)
            .hideDelay(milliseconds || 3000)
          );
      };


  });