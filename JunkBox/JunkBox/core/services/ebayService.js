angular
.module('junkBox.services.ebayService',[])
.factory('Ebay', function ($q, $http) {
    return {
        getTest: function (info) {
            var deferred = $q.defer();
            $http.post('/Ebay/GetTest/')
                  .success(function (data, status, headers, config) {
                      console.log(data);
                      deferred.resolve(data);
                  }).error(function (error) {
                      console.log(error);
                      deferred.reject(error);
                  });
            return deferred.promise;
        },
        getSomething: function (info) {
            var deferred = $q.defer();
            $http.post('/Ebay/GetSomething/', { data: info })
                  .success(function (data, status, headers, config) {
                      console.log(data);
                      deferred.resolve(data);
                  }).error(function (error) {
                      console.log(error);
                      deferred.reject(error);
                  });
            return deferred.promise;
        },
        getEbayDemo: function (info) {
            //PRODUCTION
            //AppID: WalterWo-JunkBox-PRD-e45f6444c-265c3eca
            //devID: ad5f5bd0-374e-442a-a57f-ea202679bf77
            //certID: PRD-45f6444c2fd3-e10e-4f99-8e66-a1ec

            //SANDBOX
            //App ID (Client ID)	WalterWo-JunkBox-SBX-645ed6013-c241386a
            //Dev ID	ad5f5bd0-374e-442a-a57f-ea202679bf77
            //Cert ID (Client Secret)	SBX-45ed60138e48-1d64-4760-a734-2aaa

            //CAUTION!!! Chrome must be started with web security disabled to not get the Xlns bullshit error
            //chromium-browser --disable-web-security --user-data-dir
            //Otherwise this fucking thing actually pulled info from ebay! However, my controller didnt parse it well enough to display
            //on the page... oh well. I just used Ebay's stupid example javascript function.
            var url = "http://svcs.ebay.com/services/search/FindingService/v1";
                url += "?OPERATION-NAME=findItemsByKeywords";
                url += "&SERVICE-VERSION=1.0.0";
                url += "&SECURITY-APPNAME=WalterWo-JunkBox-PRD-e45f6444c-265c3eca";
                url += "&GLOBAL-ID=EBAY-US";
                url += "&RESPONSE-DATA-FORMAT=JSON";
                url += "&callback=_cb_findItemsByKeywords";
                url += "&REST-PAYLOAD";
                url += "&keywords=harry%20potter";
                url += "&paginationInput.entriesPerPage=3";

                $http.defaults.useXDomain = true;
                delete $http.defaults.headers.common['X-Requested-With'];

                var url2 = "http://svcs.ebay.com/services/search/FindingService/v1?" +
                "OPERATION-NAME=findItemsAdvanced&" +
                "SERVICE-VERSION=1.9.0&"
                "SECURITY-APPNAME=WalterWo-JunkBox-PRD-e45f6444c-265c3eca&" +
                "RESPONSE-DATA-FORMAT=JSON&" +
                "REST-PAYLOAD&" +
                "keywords=nikon+d5000+digital+slr+camera&" +
                "itemFilter(0).name=Condition&" +
                "itemFilter(0).value=New&" +
                "itemFilter(1).name=MaxPrice&" +
                "itemFilter(1).value=750.00&" +
                "itemFilter(1).paramName=Currency&" +
                "itemFilter(1).paramValue=USD&" +
                "itemFilter(2).name=TopRatedSellerOnly&" +
                "itemFilter(2).value=true&" +
                "itemFilter(3).name=ReturnsAcceptedOnly&" +
                "itemFilter(3).value=true&" +
                "itemFilter(4).name=ExpeditedShippingType&" +
                "itemFilter(4).value=Expedited&" +
                "itemFilter(5).name=MaxHandlingTime&" +
                "itemFilter(5).value=1";

        var deferred = $q.defer();
            $http.get(url, { headers: { "Access-Control-Allow-Origin" : "localhost" } })//With web security disabled... do I even need this header?
              .success(function (data, status, headers, config) {
                  console.log("SUCCESS!!!");
                  console.log(url2);

                  console.log("data");
                  console.log(data);

                  console.log("status");
                  console.log(status);

                  console.log("headers");
                  console.log(headers);

                  console.log("config");
                  console.log(config);

                  deferred.resolve(data);
              }).error(function (error) {
                  console.log("You suck again.");
                  console.log(error);
                  deferred.reject(error);
              });
        return deferred.promise;
    }
    };
});