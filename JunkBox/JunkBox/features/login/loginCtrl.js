angular
.module('junkBox.controllers.loginCtrl', [])
.controller('loginCtrl', function ($scope, Login) {
    $scope.switchToNewAccountView = false;
    $scope.login = {
        email: "broadl21@uwosh.edu",
        password:"Text"
    };

    $scope.newAccount = function () {
        $scope.switchToNewAccountView = !$scope.switchToNewAccountView;
    };

    $scope.register = function () {

    };

    $scope.logIn = function () {
        Login.login($scope.login).then(function (data) {
            console.log(data);
        });
    };


});