angular
.module('junkBox.services.ebayService',[])
.factory('Ebay', function ($q, $http) {
    return {
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
        ebayBrowseApiFindViableItems: function (info) {
            var deferred = $q.defer();
            console.log(info);
            $http.post('/Ebay/BrowseApiFindViableItems/', {data: info})
                  .success(function (data, status, headers, config) {
                      deferred.resolve(data);
                  }).error(function (error) {
                      deferred.reject(error);
                  });
            return deferred.promise;
        },
        ebayOrderApiInitiateGuestCheckoutSession: function (info) {
            var deferred = $q.defer();
            console.log(info);
            $http.post('/Ebay/OrderApiInitiateGuestCheckoutSession/', { data: info })
                  .success(function (data, status, headers, config) {
                      deferred.resolve(data);
                  }).error(function (error) {
                      deferred.reject(error);
                  });
            return deferred.promise;
        }
    };
});