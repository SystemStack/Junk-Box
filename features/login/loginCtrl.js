angular
.module('junkBox.controllers.loginCtrl', [])
.controller('loginCtrl', function($scope, Login) {
    $scope.switchToNewAccountView = false;
    $scope.newAccount = function() {
        $scope.switchToNewAccountView = !$scope.switchToNewAccountView;
    };

    $scope.register = function(){

    };


});