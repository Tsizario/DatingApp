window.addEventListener("load", function () {

    function openWindow(event) {
        event = event || window.event;

        // find the url and title to set
        var href = this.getAttribute("href");
        var newTitle = this.getAttribute("data-title");
        // or if you work the title out some other way...
        // var newTitle = "Some constant string";

        // open the window
        var newWin = window.open(href, "_blank");

        // add a load listener to the window so that the title gets changed on page load
        setTimeout(function () {
            newWin.document.title = newTitle;
        }, 100);

        // stop the default `a` link or you will get 2 new windows!
        event.returnValue = false;
    }

    // find all a tags opening in a new window
    var links = document.querySelectorAll("a[target=_blank][data-title]");
    // or this if you don't want to store custom titles with each link
    //var links = document.querySelectorAll("a[target=_blank]");

    // add a click event for each so we can do our own thing
    for (var i = 0; i < links.length; i++) {
        links[i].addEventListener("click", openWindow.bind(links[i]));
    }
});