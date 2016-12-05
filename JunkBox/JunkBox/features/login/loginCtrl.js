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
        "email": "ABC"+Math.random()+"@uwosh.edu",
        "password": "ValidPassword1!",
        "password2": "ValidPassword1!",
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
        $scope.waitServerResult = true;
        var validate = function () {
            if (!$scope.account.email) {
                $scope.displayToUser("You must have a valid email");
            } else if ($scope.account.password !== $scope.account.password2) {
                $scope.displayToUser("Your passwords must match");
            } else if ($scope.account.password.length < 8) {
                $scope.displayToUser("Your password must be at least 8 characters");
            } else if (!/[A-Z]/.test($scope.account.password)) {
                $scope.displayToUser("Your password must have a capital letter");
            } else if (!/[a-z]/.test($scope.account.password)) {
                $scope.displayToUser("Your password must have a lowercase letter");
            } else if (!/[0-9]/.test($scope.account.password)) {
                $scope.displayToUser("Your password must have a number");
            } else {
                return true;
            }
            $scope.waitServerResult = false;
        };

        if (validate()) {
            Login.register($scope.account).then(function (data) {
                $scope.credentials = {
                    "email": $scope.account.email,
                    "password": $scope.account.password
                };
                Login.login($scope.credentials).then(function (data) {
                    if (data.result === "Success") {
                        $scope.displayToUser("Account successfully created");
                        sessionStorage.email = $scope.account.email;
                        window.location.assign("../../index.html");
                    } else {
                        $scope.displayToUser("Account could not be created", 5000);
                        $scope.isClicked = false;
                    }
                }).error(function (data) {
                    console.log(data);
                    delete sessionStorage.userName;
                });
            });
        }
    };
    $scope.logIn = function () {
        $scope.isClicked = true;
        $scope.waitServerResult = true;
        Login.login($scope.credentials).then(function (data) {
            if (data.result === "Success") {
                sessionStorage.email = $scope.credentials.email;
                window.location.assign("../../index.html");
            } else {
                $scope.displayToUser("Wrong username or password");                                    
                $scope.waitServerResult = false;
                $scope.isClicked = false;
            }
        }).error(function (data) {
            console.log(data);
            delete sessionStorage.userName;
        });
    };
    $scope.displayToUser = function (text, milliseconds) {
        $mdToast.show(
            $mdToast.simple()
            .textContent(text)
            .hideDelay(milliseconds || 3000)
        );
    };
});
