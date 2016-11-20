angular
.module('junkBox.services.preferencesService',[])
.factory('Preferences', function($q, $http) {
  return{
    validateAddress: function(address) {
      var deferred = $q.defer();
      console.log(address);
      $http.post('validateAddress.cs', address)
         .success(function(data, status, headers, config) {
          deferred.resolve(data);
         }).error(function(error) {
          deferred.reject(error);
         });
      return deferred.promise;
    },
    haltPurchases: function(haltPurchases) {
      var deferred = $q.defer();
      $http.post('haltPurchases.cs', haltPurchases)
         .success(function(data, status, headers, config) {
          deferred.resolve(data);
         }).error(function(error) {
          deferred.reject(error);
         });
      return deferred.promise;
    },
    changePassword: function(newPassword){
      var deferred = $q.defer();
      $http.post('changePassword.cs', newPassword)
         .success(function(data, status, headers, config) {
          deferred.resolve(data);
         }).error(function(error) {
          deferred.reject(error);
         });
      return deferred.promise;
    }
  };
});