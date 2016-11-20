angular
.module('junkBox.controllers.homeCtrl', [])
.controller('homeCtrl', function($scope, Home) {
    var user = {
        email: "test@test.com"
    };

    $scope.getRecentPurchases = function() {
        Home.getRecentPurchases(user).then(function(data){
            $scope.PreviousPurchases = data;
        },function(){
            $scope.displayToUser("Failed to grab most recent purchases");
        });
    }();


});