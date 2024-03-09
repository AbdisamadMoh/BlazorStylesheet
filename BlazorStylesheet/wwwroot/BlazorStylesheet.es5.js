"use strict";

"use srtict";
window.updateStylesheet = function (stylesheet) {
    removeStylesheet();
    addStylesheet(stylesheet);
    console.log("loaded");
};

function removeStylesheet() {
    var styles = document.querySelectorAll('[BlazorStylesheet="true"]');
    if (styles.length > 0) {
        styles.forEach(function (style) {
            document.head.removeChild(style);
        });
    }
}
function addStylesheet(stylesheet) {
    if (stylesheet) {
        var head = document.head;
        var style = document.createElement("style");
        style.setAttribute("BlazorStylesheet", "true");
        style.innerHTML = stylesheet;
        head.appendChild(style);
    }
    removeLoader();
    //console.log(stylesheet);
}

function removeLoader() {
    document.documentElement.removeAttribute("loading");
}

document.addEventListener("DOMContentLoaded", function (event) {
    //removeLoader();
});

