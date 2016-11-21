angular
.module("junkBoxLogin")
.controller('loginCtrl', function ($scope, $rootScope, $window, Login, $mdToast) {
    $scope.switchToNewAccountView = false;
    $rootScope.loggedIn = "false";

    $scope.credentials = {
        "email": "broadl21@uwosh.edu",
        "password": "password"
    };
    $scope.account = {
        "email": "",
        "password": "password",
        "password2": "password",
        "address": "918 Wisconsin St.",
        "address2": "",
        "city": "Oshkosh",
        "state": "Wisconsin",
        "phone": "6088511199",
        "postalCode": "54901",
        "firstName": "Levi",
        "lastName": "Broadnax"
    };

    $scope.states = ["Alabama", "Alaska", "American Samoa", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "District Of Columbia", "Federated States Of Micronesia", "Florida", "Georgia", "Guam", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Marshall Islands", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Northern Mariana Islands", "Ohio", "Oklahoma", "Oregon", "Palau", "Pennsylvania", "Puerto Rico", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virgin Islands", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming"];


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
            } else {
                return true;
            }
        };

        if (validate()) {
            Login.register($scope.account).then(function (data) {
                console.log(data);
            });
        }
    };
    $scope.logIn = function () {
        $scope.isClicked = true;
        Login.login($scope.credentials).then(function (data) {
            if (data.result === "Success") {
                sessionStorage.email = $scope.credentials.email;
                window.location.assign("../../index.html");
            } else {
                $mdToast.show($mdToast.simple()
                                      .textContent("Wrong username or password")
                                      .hideDelay(3000)
                              );
               $scope.isClicked = false;
            }
        }).error(function (data) {
            console.log(data);
            delete sessionStorage.userName;
        });
    };
});
