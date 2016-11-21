angular
.module('junkBox.services.queriesService',[])
.factory('Queries', function($q, $http) {
  return{
    send: function(query) {
      var deferred = $q.defer();
      $http
      .post('/Query/SetSettings/', {data: query})
      .success(function(data, status, headers, config) {
        deferred.resolve(data);
      }).error(function(error) {
        deferred.reject(error);
      });
      return deferred.promise;
    },
    getSettings: function (user) {
        var deferred = $q.defer();
        $http
        .post('/Query/GetSettings/', {data: user})
        .success(function (data) {
            deferred.resolve(data);
        }).error(function (error) {
            deferred.reject(error);
        });
        return deferred.promise;
    }
  };
});