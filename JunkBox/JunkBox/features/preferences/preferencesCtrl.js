angular
.module('junkBox.controllers.preferencesCtrl', [])
.controller('preferencesCtrl',
  function($rootScope, $scope, Preferences) {
      $scope.address = {
      email: $rootScope.email,
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

    $scope.getAddressData = function () {
        var userData = {
            email: $rootScope.email
        }
        Preferences.getAddress(userData).then(function (data) {
            console.log(data);
            //console.log("WILL THIS WORK???" + data["result"]["AddressID"]); <-- answer is yes it will!
            $scope.address.streetName = data["result"]["BillingAddress"];
            $scope.address.streetName2 = data["result"]["BillingAddress2"];
            $scope.address.city = data["result"]["BillingCity"];
            $scope.address.state = data["result"]["BillingState"];
            $scope.address.postalCode = data["result"]["BillingZip"];
        });
    }();

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
    
    $scope.changePassword = function () {
        var passwordData = {
            email: $rootScope.email,
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