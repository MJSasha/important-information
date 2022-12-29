﻿window.scrollToElementId = (elementId) => {
    console.info('scrolling to element', elementId);
    var element = document.getElementById(elementId);
    if (!element) {
        console.warn('element was not found', elementId);
        return false;
    }
    element.scrollIntoView({ block: "center", behavior: "smooth" });
    return true;
}

window.WriteCookie = {

    WriteCookie: function (name, value, days) {

        var expires;
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        }
        else {
            expires = "";
        }
        document.cookie = name + "=" + value + expires + "; path=/";
    }
}
window.ReadCookie = {
    ReadCookie: function (cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }
}
$("body").on("click", '[data-toggle="collapse"]', function (e) {
    if ($($(this).data('target')).hasClass('collapsing'))
        return false;
});
window.expanderMethods = {
    toggle: function (id) {
        $(`#${id}`).collapse('toggle');
    }
};
