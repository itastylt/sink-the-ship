"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/shiphub").build();

let group = false;
let debug_mode = false;

let currPlayerWins = 0;
let opponentPlayerWins = 0;
document.getElementById("startButton").disabled = true;

connection.on("UnScope", function (user, message) {
    console.log(user, message);

    var player = document.getElementById("nameText").value;
    if (user == player) {
        $('#cannon-1').trigger();
    }
});

connection.on("RoundEnd", function (user, message) {
    let player = message.split(';')[0];
    let userName = $("#name").val();

    if (player == userName) currPlayerWins++; else opponentPlayerWins++;
    let nextTurn = message.split(';')[1];
    $('.round-screen-counter').text(`${currPlayerWins} - ${opponentPlayerWins}`);
    if (round < 3) {
        handleRoundScreen(user);
        handleTurnScreen(nextTurn);
    } else {
        if (currPlayerWins > opponentPlayerWins) {
            $('.turn-screen-title').text("Congratulations! You win!");
        } else {
            $('.turn-screen-title').text("Better luck next time!");
        }

        $('.turn-screen-wrapper').removeClass('turn-hide');
        $('.turn-screen-wrapper').addClass('turn-show');
    }

});

connection.on("UnClonedBoard", function (user, message) {
    console.log(user, message);

    let player = message.split(';')[0];
    let board = message.split(';')[1];
    let turn = message.split(';')[2];
    printBoards(player, board, debug_mode);
    handleTurnScreen(turn);
    handleUnClonePowerUp(player);
});

connection.on("ClonedBoard", function (user, message) {
    console.log(user, message);


    let player = message.split(';')[0];
    let board = message.split(';')[1];
    let turn = message.split(';')[2];
    printBoards(player, board, debug_mode);
    handleTurnScreen(turn);
    handleClonePowerUp(player);
});

connection.on("StartGame", function (user, message) {

    console.log(user, message);
    let currUser = document.getElementById("nameText").value;
    let player = message.split(';')[0];
    let board = message.split(';')[1];
    let turn = message.split(';')[2];
    printBoards(player, board, debug_mode);
    handleTurnScreen(turn);
    handleShowOnStart();

    if (currUser == player) {
        handleSplashScreen(true);
    }

});

connection.on("Continue", function (user, message) {
    let currUser = document.getElementById("name").value;
    let player = message.split(';')[0];
    let board = message.split(';')[1];
    printBoards(player, board, debug_mode);
    handleShowOnStart();
    handleRound();

    if (currUser == player) {
        handleSplashScreen(true);
    }

});

/*connection.on("FireShot", function (user, message) {
    console.log(user, message);
    let player = message.split(';')[0];
    let board = message.split(';')[1];
    let turn = message.split(';')[2];
    handleTurnScreen(turn);
    if (board != null) {
        printBoards(player, board, debug_mode);
    } else {
        console.log("Nice try :)");
    }

});*/
connection.on("FireShot", function (user, message) {
    var playerUser = document.getElementById("nameText").value;

    /*let player = message.split(';')[0];
    let board = message.split(';')[1];
    let turn = message.split(';')[2];*/
    let arr = message.split(';');

    let player = arr[0];
    let board = arr[1];
    let turn = arr[2];
    let damageCount = arr[3];

    handleTurnScreen(turn);

    if (playerUser == user) {
        handleDamageCount(damageCount);
    }
    if (board != null) {
        printBoards(player, board, debug_mode);

    } else {
        console.log("Nice try :)");
    }

});

connection.start().then(function () {
    document.getElementById("startButton").disabled = false;
    console.log("Connection established");
    consoleWrite("Connection established");
}).catch(function (err) {
    return console.error(err.toString());
});

function randomizeShips() {
    var user = document.getElementById("nameText").value;

    $('#name').val(user);
    handleSplashScreen(true);

    connection.invoke("SendMessage", user, `randomize;`).catch(function (err) {
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
    if (round == 1) {
        var message = `ready;${placedShipsAsString()}`;

        connection.invoke("SendMessage", user, message).catch(function (err) {
            return console.error(err.toString());
        });
    } else {
        var message = `nextRound;${placedShipsAsString()}`;
        connection.invoke("SendMessage", user, message).catch(function (err) {
            return console.error(err.toString());
        });
    }


    event.preventDefault();
});

function selectShipCannon(cannon) {
    var user = document.getElementById("nameText").value;
    selectedCannon = cannon;
    handleCannonBoard();
    console.log(`Selected cannon ${cannon}`);
    consoleWrite(`Selected cannon ${cannon}`);
    connection.invoke("SendMessage", user, `selectWeapon;${cannon}`).catch(function (err) {
        return console.error(err.toString());
    });
}

function sinkShip(x, y) {
    var user = document.getElementById("nameText").value;
    let x_cord = x;
    let y_cord = y;

    console.log(`Selected cordinate ${x_cord} and ${y_cord}`);
    consoleWrite(`Selected cordinate ${x_cord} and ${y_cord}`);
    if (group) {
        unDemolish();
        connection.invoke("SendMessage", user, `fireGroup;${x_cord};${y_cord}`).catch(function (err) {
            return console.error(err.toString());
        });
        $("#powerUpDemolish").addClass("disabled");
    } else {
        connection.invoke("SendMessage", user, `fireWeapon;${x_cord};${y_cord}`).catch(function (err) {
            return console.error(err.toString());
        });
    }

}

let pauser = false;

function pauseGame() {
    $('.pause-modal').removeClass('d-none');
    var user = $("#name").val();

    pauser = true;
    connection.invoke("SendMessage", user, `waitingForPause;1`).catch(function (err) {
        return console.error(err.toString());
    });

}
function unpauseGame() {
    var user = $("#name").val();
    $('.pause-popup').removeClass('d-none');
    $('.pause-waiting').removeClass('d-none');
    $('.pause-popup').addClass('d-none');
    $('.pause-current').addClass('d-none');
    $('.pause.button').removeClass('d-none');

    connection.invoke("SendMessage", user, `gameWaitingUnpaused;1`).catch(function (err) {
        return console.error(err.toString());
    });

    pauser = true;
}

function confirmUnpause() {
    var user = $("#name").val();
    connection.invoke("SendMessage", user, `gameWaitingUnpaused;2`).catch(function (err) {
        return console.error(err.toString());
    });
}
function denyUnpause() {
    var user = $("#name").val();
    connection.invoke("SendMessage", user, `gameWaitingUnpaused;3`).catch(function (err) {
        return console.error(err.toString());
    });
}
connection.on("InGame", function (user, message) {
    $('.pause-modal').addClass('d-none');
    $('.unpause-popup').addClass('d-none');
    $('.pause-waiting').addClass('d-none');
    $('.pause-current').addClass('d-none');
    $('.pause-button').removeClass('d-none');
});

connection.on("GamePaused", function (user, message) {
    $('.pause-popup').addClass('d-none');
    $('.pause-waiting').addClass('d-none');
    $('.pause-current').removeClass('d-none');
    $('.pause.button').addClass('d-none');
    $('.unpause-popup').addClass('d-none');
});

connection.on("WaitingForUnpause", function (user, message) {
    if (!pauser) {
        $('.unpause-popup').removeClass('d-none');
        $('.pause-waiting').addClass('d-none');
        $('.pause-current').addClass('d-none');
        $('.pause-modal').removeClass('d-none');
    }
    pauser = false;
});

connection.on("WaitingForPause", function (user, message) {
    console.log(pauser);
    if (!pauser) {
        $('.pause-popup').removeClass('d-none');
        $('.pause-waiting').addClass('d-none');
        $('.pause-modal').removeClass('d-none');
    }
    pauser = false;
});

connection.on("Pause", function (user, message) {
    $('.pause-popup').addClass('d-none');
    $('.pause-waiting').addClass('d-none');
    $('.pause-current').removeClass('d-none');
    $('.pause-button').addClass('d-none');
});
connection.on("GameResumed", function (user, message) {
    $('.pause-popup').addClass('d-none');
    $('.pause-waiting').removeClass('d-none');
    $('.pause-modal').addClass('d-none');
});

function denyPause() {
    var user = $("#name").val();
    connection.invoke("SendMessage", user, `waitingForPause;3`).catch(function (err) {
        return console.error(err.toString());
    });
}

function confirmPause() {
    var user = $("#name").val();
    connection.invoke("SendMessage", user, `waitingForPause;2`).catch(function (err) {
        return console.error(err.toString());
    });
}
function cloneShip() {
    var user = document.getElementById("nameText").value;

    $('#powerUpClone').addClass("d-none");
    $('#powerUpUnclone').removeClass("d-none");
    connection.invoke("SendMessage", user, 'cloneShip; ').catch(function (err) {
        return console.error(err.toString());
    });
}


function sendMessage() {
    var user = $("#name").val();

    var message = $(".console-text").val();
    consoleWrite(message);

    if (message == "Debug 1") {
        debug_mode = true;
    } else if (message == "Debug 0") {
        debug_mode = false;
    } else {
        connection.invoke("SendMessage", user, 'interpret;' + message).catch(function (err) {
            return console.error(err.toString());
        });
    }


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

function demolish() {
    let demolish = $("#powerUpDemolish");
    if (!demolish.is(".disabled")) {
        $("#powerUpDemolish").addClass("d-none");
        $("#powerUpUnDemolish").removeClass("d-none");
        $(".ship-selector").addClass("cannon-active");
        group = true;
    }
}

function unDemolish() {
    $("#powerUpDemolish").removeClass("d-none");
    $("#powerUpUnDemolish").addClass("d-none");
    $(".ship-selector").removeClass("cannon-active");
    $("#cannon-1").trigger("click");
    group = false;
}

function openConsole() {
    let modal = $('.console-modal');
    $('.console-opener').addClass('d-none');
    $('.console-closer').removeClass('d-none');

    if (modal.is('.d-none')) {
        modal.removeClass('d-none');
    }
}

function closeConsole() {
    let modal = $('.console-modal');
    $('.console-opener').removeClass('d-none');
    $('.console-closer').addClass('d-none');
    if (!modal.is('d-none')) {
        modal.addClass('d-none');
    }
}