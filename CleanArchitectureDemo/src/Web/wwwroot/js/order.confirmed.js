"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/orderConfirmedHub").build();

connection.start().then(() => console.log("order confirmed hub connected...")).catch(err => console.error(err.toString()));

connection.on("OrderConfirmed", function (message) {
    showNotificaton();
    var li = document.createElement("li");
    document.getElementById("orderNotification").appendChild(li);
    li.textContent = `${message}`;
});

