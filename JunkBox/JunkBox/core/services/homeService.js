angular
.module('junkBox.services.homeService',[])
.factory('Home', function($q, $http) {
  return{
    getRecentPurchases: function(address) {
      var deferred = $q.defer();
      console.log(address);
      $http.post('recentPurchases.cs', address)
         .success(function(data, status, headers, config) {
          deferred.resolve(data);
         }).error(function(error) {
          deferred.reject(error);
         });
      return deferred.promise;
    }
  };
});