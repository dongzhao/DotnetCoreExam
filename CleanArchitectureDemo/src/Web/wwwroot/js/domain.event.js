"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/domainEventHub").build();

connection.on("EntityUpdated", function (name, message) {
    //var li = document.createElement("li");
});
