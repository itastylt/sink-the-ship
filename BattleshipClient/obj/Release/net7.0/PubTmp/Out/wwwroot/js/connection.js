"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/shiphub").build();

document.getElementById("startButton").disabled = true;

connection.on("EnemyBoard", function (user, message) {
    console.log(user, message);
    printEnemyBoard(user, message);
});

connection.start().then(function () {
    document.getElementById("startButton").disabled = false;
    console.log("Connection established");
}).catch(function (err) {
    return console.error(err.toString());
});


document.getElementById("startButton").addEventListener("click", function (event) {
    var user = document.getElementById("nameText").value;
    $('#name').val(user);

    var message = `ready;${placedShipsAsString()}`;

    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});
