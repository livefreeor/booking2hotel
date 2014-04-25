(function() {
  var display;

  display = function() {
    return alert('I am running from CoffeeScript!');
  };

  window.onload = display();

}).call(this);
