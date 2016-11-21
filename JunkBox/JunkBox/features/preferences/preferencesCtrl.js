angular
.module('junkBox.controllers.preferencesCtrl', [])
.controller('preferencesCtrl',
  function($rootScope, $scope, Preferences) {
      $scope.address = {
      email: $rootScope.Email,
      streetName : "918 Wisconsin Street",
      streetName2: "",
      city: "Oshkosh",
      state: "WI",
      postalCode: 54901
    };
    $scope.haltPurchases = false;
    $scope.states = ["AL","AK","AZ","AR","CA","CO","CT","DE","FL","GA",
                     "HI","ID","IL","IN","IA","KS","KY","LA","ME","MD",
                     "MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ",
                     "NM","NY","NC","ND","OH","OK","OR","PA","RI","SC",
                     "SD","TN","TX","UT","VT","VA","WA","WV","WI","WY"];

    $scope.validateAddress = function() {
      if (!Queries.validateAddress(e.address)) {
        $scope.displayToUser("We could not validate your address");
      }
    };
    $scope.updateUserInformation = function () {
        Preferences.updateAddress($scope.address).then(function (data) {
            console.log(data);
        });
    };
    $scope.haltAccount = function(e) {
        if (e === false) {
            var haltData = {
                action: false
            };
        Preferences.haltPurchases(haltData).then(function(data){
            $scope.displayToUser('Purchases are continuing');
            console.log(data);
        },function(){
          $scope.displayToUser("Update failed, try again");
        });
        } else if (e === true) {
            var haltData = {
                action: true
            };
        Preferences.haltPurchases(haltData).then(function(data){
            $scope.displayToUser('Purchases have been halted');
            console.log(data);
        },function(){
          $scope.displayToUser("Update failed, try again");
        });
      } else {
        $scope.displayToUser("Update failed, try again");
      }
    };

    $scope.changePassword = function () {
        var passwordData = {
            email: $rootScope.Email,
            oldPassword: $scope.currentPassword,
            newPassword: $scope.newPassword
        };
      if(!$scope.newPassword || !$scope.newPassword2 || !$scope.currentPassword ){
        $scope.displayToUser("Please fill out all fields");
      } else if($scope.newPassword !== $scope.newPassword2){
        $scope.displayToUser("Passwords must be the same");
      } else if ($scope.currentPassword === $scope.newPassword){
        $scope.displayToUser("You cannot change your password to your current password");
      } else {
        Preferences.changePassword(passwordData).then(function(data){
            $scope.displayToUser("Password successfully changed");
            console.log(data);
        },function(){
          $scope.displayToUser("Password change failed");
        });
      }
    }

});