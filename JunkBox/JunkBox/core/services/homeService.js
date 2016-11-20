angular
.module('junkBox.services.homeService',[])
.factory('Home', function($q, $http) {
    return{
      getRecentPurchases: function(user) {
          var deferred = $q.defer();
            email = encodeURIComponent(user.email).replace(/\./g, "PERIODHERE");
            $http.post('/Home/GetRecentPurchases/' + email)
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