angular
.module('junkBox.services.orderHistoryService', [])
.factory('OrderHistory', function ($q, $http) {
    return {
        
        getCustomerOrders: function (data) {
            var deferred = $q.defer();
            $http.post('/OrderHistory/GetCustomerOrders/', { data : data })
                  .success(function (data, status, headers, config) {
                      deferred.resolve(data);
                  }).error(function (error) {
                      deferred.reject(error);
                  });
            return deferred.promise;
        },
        getGuestCheckoutSession: function (data) {
            var deferred = $q.defer();
            $http.post('/OrderHistory/GetGuestCheckoutSession/', { data: data })
                  .success(function (data, status, headers, config) {
                      deferred.resolve(data);
                  }).error(function (error) {
                      deferred.reject(error);
                  });
            return deferred.promise;
        }
        
    };
});