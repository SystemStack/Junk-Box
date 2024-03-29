angular
.module('junkBox.services.preferencesService',[])
.factory('Preferences', function($q, $http) {
  return{
    updateAddress: function(address) {
      var deferred = $q.defer();
      console.log(address);
        $http.post('/Preferences/UpdateAddress/', { data: address} )
         .success(function(data, status, headers, config) {
          deferred.resolve(data);
         }).error(function(error) {
          deferred.reject(error);
         });
      return deferred.promise;
    },
    getAddress: function (user) {
        var deferred = $q.defer();
        console.log(user);
        $http.post('/Preferences/GetAddress/', { data: user })
         .success(function (data, status, headers, config) {
             deferred.resolve(data);
         }).error(function (error) {
             deferred.reject(error);
         });
        return deferred.promise;
    },
    changePassword: function(newPassword){
      var deferred = $q.defer();
        $http.post('/Preferences/ChangePassword/', {data: newPassword})
         .success(function(data, status, headers, config) {
          deferred.resolve(data);
         }).error(function(error) {
          deferred.reject(error);
         });
      return deferred.promise;
    }
  };
});