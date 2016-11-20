angular
.module('junkBox.services.queriesService',[])
.factory('Queries', function($q, $http) {
  return{
    send: function(query) {
      var deferred = $q.defer();
      $http
      .post('urlToSendTo.cs',query)
      .success(function(data, status, headers, config) {
        deferred.resolve(data);
      }).error(function(error) {
        deferred.reject(error);
      });
      return deferred.promise;
    }
  };
});