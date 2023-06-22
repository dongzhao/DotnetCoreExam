"use strict";

const { Alert } = require("../lib/bootstrap/dist/js/bootstrap.esm");

var connection = new signalR.HubConnectionBuilder().withUrl("/domainEventHub").build();

connection.on("EntityUpdated", function (name, message) {
    //var li = document.createElement("li");
    Alert(message);
    showNotificaton();
});

connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});