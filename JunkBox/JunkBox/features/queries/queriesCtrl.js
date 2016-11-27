angular
.module('junkBox.controllers.queriesCtrl', [])
.controller('queriesCtrl', function($scope, Queries, $rootScope, Ebay) {
  $scope.query = {
    email: $rootScope.email,
    category: "All Categories",
    categoryId: "-1",
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

  $scope.categories = {
      "All Categories" : "-1",
      "Antiques" : "-1", 
      "Art" : "-1",  
      "Baby" : "-1", 
      "Books" : "-1",  
      "Business and Industrial": "-1", 
      "Cameras and Photo": "-1", 
      "Cell Phones and Accessories": "-1", 
      "Clothing, Shoes and Accessories": "-1",  
      "Coins and Paper Money": "-1", 
      "Collectibles": "-1", 
      "Computers/Tablets and Networking": "-1", 
      "Consumer Electronics": "-1",  
      "Crafts": "-1", 
      "Dolls and Bears": "-1",  
      "DVDs and Movies": "-1", 
      "eBay Motors": "-1",  
      "Entertainment Memorabilia": "-1", 
      "Gift Cards and Coupons": "-1",  
      "Health and Beauty": "-1", 
      "Home and Garden": "-1", 
      "Jewelry and Watches": "-1", 
      "Music": "-1", 
      "Musical Instruments and Gear": "-1", 
      "Pet Supplies": "-1", 
      "Pottery and Glass": "-1", 
      "Real Estate": "-1",  
      "Specialty Services": "-1", 
      "Sporting Goods": "-1", 
      "Sports Mem, Cards and Fan Shop": "-1", 
      "Stamps": "-1", 
      "Tickets and Experiences": "-1", 
      "Toys and Hobbies": "-1", 
      "Travel": "-1", 
      "Video Games and Consoles": "-1", 
      "Everything Else": "-1"
  };

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
          $scope.frequencyOptions.forEach(function (element) {
              //console.log(userSettings["Frequency"] + " *** " + element["label"]);
              if(userSettings["Frequency"].toUpperCase() === element["label"].toUpperCase()){
                  $scope.query.frequencyOptions.label = element.label;
                  $scope.query.frequencyOptions.value = element.value;
              }
          });
          //console.log($scope.query.frequencyOptions);
      });

      Ebay.getAllCategories().then(function (success) {
          console.log(success);
          var newList = {};

          success.CategoryArray.Category.forEach(function (element) {
              newList[element["CategoryName"]] = element["CategoryID"];
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

  $scope.updateCategory = function () {
      console.log($scope.query.category);
      $scope.query.categoryId = $scope.categories[$scope.query.category];
      console.log($scope.query.categoryId);
  };

});