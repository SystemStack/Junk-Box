angular
.module('junkBox.controllers.queriesCtrl', [])
.controller('queriesCtrl', function($scope, Queries, $rootScope, Ebay) {
  $scope.query = {
    email: $rootScope.email,
    category: "All Categories",
    price : 2.51,
    frequencyOptions: {
      value: -1,
      label: "Never"
    }
  };
  $scope.frequencyOptions = [{
    value: 1,
    label: "Daily"
  },{
     value: 7,
    label: "Weekly"
  },{
    value: 30,
    label: "Monthly"
  }];

  $scope.categories = ["All Categories", "Antiques",
            "Art", "Baby",
            "Books", "Business and Industrial",
            "Cameras and Photo", "Cell Phones and Accessories",
            "Clothing, Shoes and Accessories", "Coins and Paper Money",
            "Collectibles", "Computers/Tablets and Networking",
            "Consumer Electronics", "Crafts",
            "Dolls and Bears", "DVDs and Movies",
            "eBay Motors", "Entertainment Memorabilia",
            "Gift Cards and Coupons", "Health and Beauty",
            "Home and Garden", "Jewelry and Watches",
            "Music", "Musical Instruments and Gear",
            "Pet Supplies", "Pottery and Glass",
            "Real Estate", "Specialty Services",
            "Sporting Goods", "Sports Mem, Cards and Fan Shop",
            "Stamps", "Tickets and Experiences",
            "Toys and Hobbies", "Travel",
            "Video Games and Consoles", "Everything Else"].sort();

  $scope.getQuerySettings = function () {
      var userData = {
          email: $rootScope.email
      }

      Queries.getSettings(userData).then(function (data) {
          console.log(data);
          var userSettings = data["result"];
          $scope.query.category = userSettings["Category"];
          $scope.query.price = userSettings["PriceLimit"];
          //$scope.query.frequencyOptions = ??? Not sure how to handle this one.
      });

      Ebay.getAllCategories().then(function (success) {
          console.log(success);
          var newList = [];

          //console.log(success.CategoryArray.Category);
          success.CategoryArray.Category.forEach(function (element) {
              newList.push(element["CategoryName"]);
          });

          $scope.categories = newList;
      }, function (failure) {
          console.log(failure);
      });
  }();

  $scope.send = function() {
    var verifyValidData = function(e) {
      console.log(e);
      if(!e) {
        $scope.displayToUser("Data is invalid");
      } else if(e.price < 1.00 || e.price > 5.00) {
        $scope.displayToUser("Price is invalid");
      } else {
        $scope.displayToUser("Data is valid");
        return true;
      }
      return false;
    };
    if(verifyValidData($scope.query)) {
        Queries.send($scope.query).then(function (data) {
            console.log(data);
        });
      console.log($scope.query);
      $scope.submittedRecord = true;
    }
  };

});