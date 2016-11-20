angular
.module('junkBox.controllers.loginCtrl', [])
.controller('loginCtrl', function ($scope, Login) {
    $scope.switchToNewAccountView = false;
    $scope.login = {
        "email": "levi.broadnax@gmail.com",
        "password": "Text",
        "hash": "dummyHash"
    };
    $scope.account = {
        "email": "",
        "password": "",
        "password2": "",
        "address": "",
        "address2": "",
        "city": "",
        "state": "",
        "phone": "",
        "postalCode": "",
        "firstName": "",
        "lastName": "",
        "hash": "dummyHash",
        "salt": "dummySalt"
        
    };
    $scope.newAccount = function () {
        $scope.switchToNewAccountView = !$scope.switchToNewAccountView;
    };

    $scope.register = function () {
        console.log($scope.account);
        var validate = function () {
            if ($scope.account.password !== $scope.account.password2) {
                $scope.displayToUser("Your passwords must match");
            } else if ($scope.account.password.length < 8) {
                $scope.displayToUser("Your password must be at least 8 characters");
            //} else if (x === 5) {//regex for special characters and ish

            } else {
                return true;
            }
        };

        if (validate()) {
            //I think we want to generate the hash/salt and send those through
            //instead of sending the plain-text passwords
            $scope.account.password = "";
            $scope.account.password2 = "";
            Login.register($scope.account).then(function (data) {
                console.log(data);
            });
        }
    };



    $scope.logIn = function () {
        console.log("LOGIN FUNCTION: " + $scope.login);
        Login.login($scope.login).then(function (data) {
            console.log(data);
        });
    };

    $scope.states = ["Alabama", "Alaska", "American Samoa", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "District Of Columbia", "Federated States Of Micronesia", "Florida", "Georgia", "Guam", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Marshall Islands", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Northern Mariana Islands", "Ohio", "Oklahoma", "Oregon", "Palau", "Pennsylvania", "Puerto Rico", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virgin Islands", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming"];

});