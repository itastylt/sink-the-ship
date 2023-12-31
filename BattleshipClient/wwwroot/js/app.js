var boardSize = [10, 10];
var placedShips = [];
var round = 1;
function consoleWrite(message) {
    var consoleElem = $('.console-content');
    console.log("testas");
    var dom = "<div class=\"console-row\">".concat(message, "</div>");
    consoleElem.append(dom);
}
function domReady(cb) {
    if (document.readyState === "complete" || document.readyState === "interactive") {
        cb();
    }
    else {
        document.addEventListener("DOMContentLoaded", function (event) {
            cb();
        });
    }
}
function generateYourBoard() {
    console.log('generating');
    var boardDOM = document.getElementById('your-board');
    var rowsDom = '';
    for (var i = 0; i < boardSize[0]; i++) {
        rowsDom += '<div class="row">';
        for (var j = 0; j < boardSize[1]; j++) {
            rowsDom += "<div class=\"col board-tile\" data-x='".concat(j, "' data-y='").concat(i, "' onclick=\"placeShip(").concat(j, ", ").concat(i, ");\">&nbsp;</div>");
        }
        rowsDom += '</div>';
    }
    boardDOM.innerHTML = rowsDom;
}
function generateEnemyBoard() {
    var boardDOM = document.getElementById('enemy-board');
    var rowsDom = '';
    for (var i = 0; i < boardSize[0]; i++) {
        rowsDom += '<div class="row">';
        for (var j = 0; j < boardSize[1]; j++) {
            rowsDom += "<div class=\"col board-tile\" data-x='".concat(j, "' data-y='").concat(i, "' onclick=\"sinkShip(").concat(j, ", ").concat(i, ");\">&nbsp;</div>");
        }
        rowsDom += '</div>';
    }
    boardDOM.innerHTML = rowsDom;
}
domReady(function () {
    generateYourBoard();
    generateEnemyBoard();
    generateShipSelector();
    generateCannonSelector();
});
var Ship = /** @class */ (function () {
    function Ship(typeString, classSelector, size, color, cannon, preview) {
        this.selector = classSelector;
        this.type = typeString;
        this.size = size;
        this.color = color;
        this.cannon = cannon;
        this.cannonPreview = preview;
    }
    Ship.prototype.getShip = function (id) {
        if (this.selector === id || this.type === id) {
            return this;
        }
        return null;
    };
    return Ship;
}());
var PlacedShip = /** @class */ (function () {
    function PlacedShip(typeString, x1, y1, size, angle, cannon) {
        this.type = typeString;
        this.x_start = x1;
        this.y_start = y1;
        this.size = size;
        this.angle = angle;
        this.cannon = cannon;
    }
    return PlacedShip;
}());
var boatDOM = "<div class=\"row\">\n                    <div class=\"col board-tile\" style=\"color:red;\">X</div>\n                    <div class=\"col board-tile\">&nbsp;</div>\n                </div>\n                <div class=\"row\">\n                    <div class=\"col board-tile\" style=\"background-color: #C4F4FF;\">&nbsp;</div>\n                    <div class=\"col board-tile\" style=\"background-color: blue;\">&nbsp;</div>\n                </div>";
var lavantierDOM = "<div class=\"row\">\n                        <div class=\"col board-tile\" style=\"background-color: #938F92; color: red;\">X</div>\n                        <div class=\"col board-tile\" style=\"background-color: #938F92;\">&nbsp;</div>\n                     </div>\n                     <div class=\"row\">\n                        <div class=\"col board-tile\">&nbsp;</div>\n                        <div class=\"col board-tile\">&nbsp;</div>\n                     </div>";
var submarineDOM = "<div class=\"row\">\n                        <div class=\"col board-tile\" style=\"background-color: #938F92; color: red;\">X</div>\n                        <div class=\"col board-tile\">&nbsp;</div>\n                     </div>\n                     <div class=\"row\">\n                        <div class=\"col board-tile\" style=\"background-color: #938F92;\">&nbsp;</div>\n                        <div class=\"col board-tile\">&nbsp;</div>\n                     </div>";
var destroyerDOM = "<div class=\"row\">\n                        <div class=\"col board-tile\" style=\"background-color: #938F92; color: red;\">X</div>\n                        <div class=\"col board-tile\">&nbsp;</div>\n                     </div>\n                     <div class=\"row\">\n                        <div class=\"col board-tile\">&nbsp;</div>\n                        <div class=\"col board-tile\" style=\"background-color: #938F92;\">&nbsp;</div>\n                     </div>";
var boat = new Ship('Boat', 'ship_boat', 1, 'brown', 1, boatDOM);
var lavantier = new Ship('Lavantier', 'ship_lavantier', 2, 'red', 2, lavantierDOM);
var submarine = new Ship('Submarine', 'ship_submarine', 3, 'green', 3, submarineDOM);
var destroyer = new Ship('Destroyer', 'ship_destroyer', 4, 'blue', 4, destroyerDOM);
var ships = [boat, lavantier, submarine, destroyer];
function generateShipSelector() {
    var shipBoard = document.getElementById('ship-board');
    var shipsDom = '';
    for (var i = 0; i < ships.length; i++) {
        shipsDom += "<div class=\"row ship-selector\" id=".concat(ships[i].type, " onclick=\"selectShip('").concat(ships[i].selector, "')\">\n                        <div class=\"col ship-title\">").concat(ships[i].type, "</div>\n                            <div class=\"col ship-preview\">\n                                <div class=\"row\">");
        for (var j = 0; j < ships[i].size; j++) {
            shipsDom += "<div class=\"col board-tile\" style=\"background-color:".concat(ships[i].color, ";\" >&nbsp;</div>");
        }
        shipsDom += "</div>\n            </div>\n        </div>";
    }
    shipBoard.innerHTML = shipsDom;
}
var selectedShip = null;
function selectShip(shipSelector) {
    for (var i = 0; i < ships.length; i++) {
        selectedShip = ships[i].getShip(shipSelector);
        if (selectedShip != null) {
            break;
        }
    }
    if (selectedShip != null) {
        console.log(selectedShip.type);
        console.log(selectedShip.size);
    }
}
function placeShip(x, y) {
    if (selectedShip == null) {
        console.log('Nice try :)');
        return;
    }
    console.log("Placed ship ".concat(selectedShip.type, " on: x = ").concat(x, " y = ").concat(y));
    if (boardSize[1] > x + selectedShip.size - 1) {
        for (var i = 0; i < selectedShip.size; i++) {
            console.log("Ship coordinate:", $("#your-board .board-tile[data-x='".concat(x + i, "'][data-y='").concat(y, "']")));
            $("#your-board .board-tile[data-x='".concat(x + i, "'][data-y='").concat(y, "']")).css("background-color", "".concat(selectedShip.color));
            $("#your-board .board-tile[data-x='".concat(x + i, "'][data-y='").concat(y, "']")).addClass('ship');
        }
        placedShips.push(new PlacedShip(selectedShip.type, x, y, selectedShip.size, 90, selectedShip.cannon));
    }
    else {
        console.log("Nice try :)");
    }
    $("#".concat(selectedShip.type)).css('display', 'none');
    selectedShip = null;
}
var selectedCannon = 1;
function placedShipsAsString() {
    var string = "[";
    for (var i = 0; i < placedShips.length; i++) {
        string += "{\"Type\": \"".concat(placedShips[i].type, "\", \"X\": ").concat(placedShips[i].x_start, ", \"Y\": ").concat(placedShips[i].y_start, ", \"Size\": ").concat(placedShips[i].size, ", \"Angle\": ").concat(placedShips[i].angle, "}");
        if (i < placedShips.length - 1) {
            string += ',';
        }
    }
    string += "]";
    return string;
}
function handleSplashScreen(state) {
    if (state == true) {
        $(".hide-on-join").addClass("d-none");
    }
    else {
        $(".hide-on-join").removeClass("d-none");
    }
    $('.ready-screen-wrapper').slideToggle("slow");
}
function handleTurnScreen(player) {
    var name = $("#name").val();
    if (name == player) {
        $('.turn-screen-title').text("Your turn");
    }
    else {
        $('.turn-screen-title').text("Enemies turn");
    }
    var wrapper = $('.turn-screen-wrapper');
    if (wrapper.is('.turn-hide')) {
        wrapper.removeClass('turn-hide');
    }
    wrapper.toggleClass('turn-show');
    setTimeout(function () { wrapper.toggleClass('turn-show'); }, 2000);
}
function printBoards(user, board, debug_mode) {
    var name = $("#name").val();
    var array = eval(board);
    $('.power-up-panel').addClass('show');
    if (name != user) {
        $("#enemy-board .board-tile").css('background', '');
        for (var i = 0; i < array.length; i++) {
            for (var j = 0; j < array[0].length; j++) {
                switch (array[i][j]) {
                    case 1: {
                        if (debug_mode) {
                            $("#enemy-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "url('img/boat.png'), brown");
                        }
                        break;
                    }
                    case 2: {
                        if (debug_mode) {
                            $("#enemy-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "url('img/submarine.png'), red");
                        }
                        break;
                    }
                    case 3: {
                        if (debug_mode) {
                            $("#enemy-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "url('img/lavantier.png'), green");
                        }
                        break;
                    }
                    case 4: {
                        if (debug_mode) {
                            $("#enemy-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "url('img/destroyer.png'), blue");
                        }
                        break;
                    }
                    case 5: {
                        if (debug_mode) {
                            $("#enemy-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "black");
                        }
                        break;
                    }
                    case 6: {
                        if (debug_mode) {
                            $("#enemy-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "yellow");
                        }
                        break;
                    }
                    case -99: {
                        $("#enemy-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "#938F92");
                        break;
                    }
                    case -1: {
                        $("#enemy-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "url('img/boat.png'), #7A5C56");
                        break;
                    }
                    case -2: {
                        $("#enemy-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "url('img/submarine.png'), #FF947D");
                        break;
                    }
                    case -3: {
                        $("#enemy-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "url('img/lavantier.png'), #C6FFC4");
                        break;
                    }
                    case -4: {
                        $("#enemy-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "url('img/destroyer.png'), #C4F4FF");
                        break;
                    }
                    case -6: {
                        $("#enemy-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background-color", "#ECFDA2");
                        break;
                    }
                }
            }
        }
    }
    else {
        $("#your-board .board-tile").css('background-color', '');
        for (var i = 0; i < array.length; i++) {
            for (var j = 0; j < array[0].length; j++) {
                $("#your-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css('background-color', '');
                switch (array[i][j]) {
                    case 1: {
                        $("#your-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "url('img/boat.png'), brown");
                        break;
                    }
                    case 2: {
                        $("#your-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "url('img/submarine.png'), red");
                        break;
                    }
                    case 3: {
                        $("#your-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "url('img/lavantier.png'), green");
                        break;
                    }
                    case 4: {
                        $("#your-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "url('img/destroyer.png'), blue");
                        break;
                    }
                    case 5: {
                        $("#your-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "black");
                        break;
                    }
                    case 6: {
                        $("#your-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "yellow");
                        break;
                    }
                    case -99: {
                        $("#your-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "#938F92");
                        break;
                    }
                    case -1: {
                        $("#your-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "url('img/boat.png'), #7A5C56");
                        break;
                    }
                    case -2: {
                        $("#your-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "url('img/submarine.png'), #FF947D");
                        break;
                    }
                    case -3: {
                        $("#your-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "url('img/lavantier.png'), #C6FFC4");
                        break;
                    }
                    case -4: {
                        $("#your-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background", "url('img/destroyer.png'), #C4F4FF");
                        break;
                    }
                    case -6: {
                        $("#your-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background-color", "#ECFDA2");
                        break;
                    }
                }
            }
        }
    }
}
function handleUnclonePowerUp(player) {
    var name = $("#name").val();
    $(".power-up-unclone").addClass("disable");
}
function handleClonePowerUp(player) {
    var name = $("#name").val();
    if (name == player) {
        $("#powerUpClone").addClass("d-none");
        $(".power-up-unclone").removeClass("d-none");
    }
}
function handleShowOnStart() {
    $('.show-on-join').addClass('show');
}
function generateCannonSelector() {
    var shipBoard = document.getElementById('cannon-board');
    var shipsDom = '';
    for (var i = 0; i < ships.length; i++) {
        if (i == 0) {
            shipsDom += "<div class=\"row ship-selector cannon-active\" id=\"cannon-".concat(ships[i].cannon, "\" onclick=\"selectShipCannon(").concat(ships[i].cannon, ")\">\n                            <div class=\"col ship-title\">").concat(ships[i].type, "</div>\n                            <div class=\"col cannon-preview\">\n                            ").concat(ships[i].cannonPreview, "\n                        </div>\n                     </div>");
        }
        else {
            shipsDom += "<div class=\"row ship-selector\" id=\"cannon-".concat(ships[i].cannon, "\" onclick=\"selectShipCannon(").concat(ships[i].cannon, ")\">\n                            <div class=\"col ship-title\">").concat(ships[i].type, "</div>\n                            <div class=\"col cannon-preview\">\n                            ").concat(ships[i].cannonPreview, "\n                        </div>\n                     </div>");
        }
    }
    shipBoard.innerHTML = shipsDom;
}
function handleCannonBoard() {
    $(".ship-selector").removeClass('cannon-active');
    $("#cannon-".concat(selectedCannon)).addClass('cannon-active');
}
function handleRound() {
    $('.show-on-join').removeClass('d-none');
}
function handleRoundScreen(user) {
    round++;
    var name = $("#name").val();
    var yourDOM = document.getElementById('your-board');
    var enemyDOM = document.getElementById('enemy-board');
    yourDOM.innerHTML = "";
    enemyDOM.innerHTML = "";
    if (round == 2) {
        boardSize = [12, 12];
    }
    else if (round == 3) {
        boardSize = [13, 13];
    }
    generateYourBoard();
    generateEnemyBoard();
    placedShips = [];
    $(".round-screen-title").text("Round ".concat(round));
    var wrapper = $('.round-screen-wrapper');
    $('.show-on-round').removeClass('d-none');
    $('.show-on-join').addClass('d-none');
    $('.ship-selector').attr("style", "");
    if (wrapper.is('.round-hide')) {
        wrapper.removeClass('round-hide');
    }
    wrapper.toggleClass('round-show');
    setTimeout(function () { wrapper.toggleClass('round-show'); }, 2000);
}
function handleDamageCount(damageCount) {
    $('#shotCount').text(damageCount);
}
function handleCurrentDamageCount(currentDamageCount) {
    $('#currentShotCount').text(currentDamageCount);
}
//# sourceMappingURL=app.js.map