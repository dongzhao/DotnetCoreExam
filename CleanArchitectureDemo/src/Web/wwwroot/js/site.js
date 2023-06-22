// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var viewNotificaton = function () {
    $(this).text("0");
    $(this).data("notification",0);
};

var showNotificaton = function () {
    const counter = $(this).data("notification");
    $(this).data("notification", counter+1);
    $(this).text(""+ (counter + 1));
};

$("#notifications").on("click", viewNotificaton);

