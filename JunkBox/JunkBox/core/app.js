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
  }).run(function ($rootScope, $http) {
    if (!sessionStorage.email) {
        delete sessionStorage;
        window.location.assign("features/login/login.html");
    }
}).controller("MainCtrl", function ($scope, $mdToast, $rootScope) {
      $scope.checkSessionStorage = function (){
          if(sessionStorage.email){
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


  });