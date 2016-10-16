angular
.module('junkBox.controllers.homeCtrl', [])
.controller('homeCtrl', function($scope, Home) {
    $scope.getRecentPurchases = function() {
        Home.getRecentPurchases().then(function(data){
            $scope.PreviousPurchases = data;
        },function(){
            $scope.displayToUser("Failed to grab most recent purchases");
        });
    }();
});