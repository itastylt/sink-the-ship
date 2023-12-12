"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/shiphub").build();

document.getElementById("startButton").disabled = true;

connection.on("UnScope", function (user, message) {
    console.log(user, message);

    var player = document.getElementById("nameText").value;
    if (user == player) {
        $('#cannon-1').trigger();
    }
});

connection.on("WinnerGame", function (user, message) {
    console.log("GameEnd", user);

    var player = document.getElementById("nameText").value;
    handleWinningScreen(user);

});
connection.on("UnClonedBoard", function (user, message) {
    console.log(user, message);

    let player = message.split(';')[0];
    let board = message.split(';')[1];
    let turn = message.split(';')[2];
    printBoards(player, board);
    handleTurnScreen(turn);
    handleUnClonePowerUp(player);
});

connection.on("ClonedBoard", function (user, message) {
    console.log(user, message);


    let player = message.split(';')[0];
    let board = message.split(';')[1];
    let turn = message.split(';')[2];
    printBoards(player, board);
    handleTurnScreen(turn);
    handleClonePowerUp(player);
});

connection.on("StartGame", function (user, message) {

    console.log(user, message);
    let currUser = document.getElementById("nameText").value;
    let player = message.split(';')[0];
    let board = message.split(';')[1];
    let turn = message.split(';')[2];
    printBoards(player, board);
    handleTurnScreen(turn);
    handleShowOnStart();

    if (currUser == player) {
        handleSplashScreen(true);
    }

});
connection.on("FireShot", function (user, message) {
    console.log(user, message);
    let player = message.split(';')[0];
    let board = message.split(';')[1];
    let turn = message.split(';')[2];
    handleTurnScreen(turn);
    if (board != null) {
        printBoards(player, board);
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

function randomizeShips() {
    var user = document.getElementById("nameText").value;

    $('#name').val(user);
    handleSplashScreen(true);

    connection.invoke("SendMessage", user , `randomize;`).catch(function (err) {
        return console.error(err.toString());
    });
}

document.getElementById("cancelButton").addEventListener("click", function (event) {
    event.preventDefault();
    var user = document.getElementById("nameText").value;
    connection.invoke("SendMessage", user, `undoReady; `).catch(function (err) {
        return console.error(err.toString());
    });
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
    handleCannonBoard();
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



function cloneShip() {
    var user = document.getElementById("nameText").value;

    $('#powerUpClone').addClass("d-none");
    $('#powerUpUnclone').removeClass("d-none");
    connection.invoke("SendMessage", user, 'cloneShip; ').catch (function (err) {
        return console.error(err.toString());
    });
}

function unCloneShip() {
    var user = document.getElementById("nameText").value;
    $('.power-up-unclone').addClass("disabled");

    connection.invoke("SendMessage", user, 'unCloneShip; ').catch(function (err) {
        return console.error(err.toString());
    });
}

function unFire() {
    var user = document.getElementById("nameText").value;
    $('.power-up-fire').addClass("disabled");
    connection.invoke("SendMessage", user, 'unFireWeapon; ').catch(function (err) {
        return console.error(err.toString());
    });
}

function unScope() {
    var user = document.getElementById("nameText").value;
    $('.power-up-scope').addClass("disabled");
    $('#cannon-1').trigger("click");
    connection.invoke("SendMessage", user, 'unselectWeapon; ').catch(function (err) {
        return console.error(err.toString());
    });
}
function fireGroup() {
    var user = document.getElementById("nameText").value;
    let x_cord = x;
    let y_cord = y;
    console.log(`Selected cordinate ${x_cord} and ${y_cord}`);
    connection.invoke("SendMessage", user, `fireGroup;${x_cord};${y_cord}`).catch(function (err) {
        return console.error(err.toString());
    });
}