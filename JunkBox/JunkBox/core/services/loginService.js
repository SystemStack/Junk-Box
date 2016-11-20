angular
.module('junkBox.services.loginService', [])
.factory('Login', function ($q, $http) {
    return {
        login: function (user) {
            var deferred = $q.defer();
            email = encodeURIComponent(user.email).replace(/\./g, "PERIODHERE");
            $http.post('/Login/Login/' + email + "," + user.password)
                .success(function (data, status, headers, config) {
                   console.log(data);
                   deferred.resolve(data);
               }).error(function (error) {
                   console.log(error);
                   deferred.reject(error);
               });
            return deferred.promise;
        },
        register: function (user) {
            var deferred = $q.defer();
            $http.post('/Login/Register/' , { id: user })
                 .success(function (data) {
                     console.log(data);
                     deferred.resolve(data);
                 }).error(function (error) {
                     console.log(error);
                     deferred.reject(error);
                 });
            return deferred.promise;
        }
    };
});


