angular
.module('junkBox.controllers.queriesCtrl', [])
.controller('queriesCtrl', function($scope, Queries) {
  $scope.query = {
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
      Queries.send($scope.query);
      console.log($scope.query);
      $scope.submittedRecord = true;
    }
  };

});