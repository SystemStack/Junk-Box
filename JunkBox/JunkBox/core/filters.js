angular
.module('junkBox.filters', [])
.filter('reverse', function() {//simple filter, reverses input
  return function(e) {
    return e.slice().reverse();
  };
});