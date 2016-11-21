angular
.module('junkBox.controllers.homeCtrl', [])
.controller('homeCtrl', function($scope, Home, $rootScope) {
    var user = {
        email: $rootScope.email
    };
    $scope.getRecentPurchases = function() {
        Home.getRecentPurchases(user).then(function(data){
            $scope.PreviousPurchases = data;
        },function(){
            $scope.displayToUser("Failed to grab most recent purchases");
        });
    }();


});