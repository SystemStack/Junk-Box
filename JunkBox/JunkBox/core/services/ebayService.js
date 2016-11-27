angular
.module('junkBox.services.ebayService',[])
.factory('Ebay', function ($q, $http) {
    return {
        getTest: function (info) {
            var deferred = $q.defer();
            $http.post('/Ebay/GetTest/')
                  .success(function (data, status, headers, config) {
                      deferred.resolve(data);
                  }).error(function (error) {
                      deferred.reject(error);
                  });
            return deferred.promise;
        },
        getSomething: function (info) {
            var deferred = $q.defer();
            $http.post('/Ebay/GetSomething/', { data: info })
                  .success(function (data, status, headers, config) {
                      deferred.resolve(data);
                  }).error(function (error) {
                      deferred.reject(error);
                  });
            return deferred.promise;
        },
        getAllCategories: function (info) {
            var deferred = $q.defer();
            $http.post('/Ebay/GetAllCategories/')
                  .success(function (data, status, headers, config) {
                      deferred.resolve(data);
                  }).error(function (error) {
                      deferred.reject(error);
                  });
            return deferred.promise;
        },
        getViablePurchases: function (info) {
            var deferred = $q.defer();
            console.log(info);
            $http.post('/Ebay/GetViablePurchases/', { data: info })
                  .success(function (data, status, headers, config) {
                      deferred.resolve(data);
                  }).error(function (error) {
                      deferred.reject(error);
                  });
            return deferred.promise;
        },
        ebayBrowseApiTest: function (info) {
            var deferred = $q.defer();
            console.log(info);
            $http.post('/Ebay/BrowseAPITest/', {data: info})
                  .success(function (data, status, headers, config) {
                      deferred.resolve(data);
                  }).error(function (error) {
                      deferred.reject(error);
                  });
            return deferred.promise;
        }
    };
});