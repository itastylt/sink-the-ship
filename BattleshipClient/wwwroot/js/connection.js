"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/shiphub").build();

document.getElementById("startButton").disabled = true;

connection.on("StartGame", function (user, message) {
    console.log(user, message);
    let player = message.split(';')[0];
    let board = message.split(';')[1];
    printEnemyBoard(player, board);
});

connection.on("FireShot", function (user, message) {
    console.log(user, message);
    let player = message.split(';')[0];
    let board = message.split(';')[1];
    if (board != null) {
        printEnemyBoard(player, board);
    } else {
        console.log("Nice try :)");
    }

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

function selectShipCannon(cannon) {
    var user = document.getElementById("nameText").value;
    selectedCannon = cannon;


    console.log(`Selected cannon ${cannon}`);
    connection.invoke("SendMessage", user, `selectWeapon;${cannon}`).catch(function (err) {
        return console.error(err.toString());
    });
}

function sinkShip(x, y) {
    var user = document.getElementById("nameText").value;
    let x_cord = x;
    let y_cord = y;

    console.log(`Selected cordinate ${x_cord} and ${y_cord}`);
    connection.invoke("SendMessage", user, `fireWeapon;${x_cord};${y_cord}`).catch(function (err) {
        return console.error(err.toString());
    });
}
