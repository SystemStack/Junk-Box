angular
.module('junkBox.services.loginService', [])
.factory('Login', function ($q, $http) {
    return {
        login: function (user) {
            var deferred = $q.defer();
            $http.post('api/login/login'+ user.email)
               .success(function (data, status, headers, config) {
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


