var boardSize = [10, 10];
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
            rowsDom += "<div class=\"col board-tile\" data-x='".concat(i, "' data-y='").concat(j, "' onclick=\"placeShip(").concat(i, ", ").concat(j, ");\">&nbsp;</div>");
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
            rowsDom += "<div class=\"col board-tile\" data-x='".concat(i, "' data-y='").concat(j, "' onclick=\"sinkShip(").concat(i, ", ").concat(j, ");\">&nbsp;</div>");
        }
        rowsDom += '</div>';
    }
    boardDOM.innerHTML = rowsDom;
}
domReady(function () {
    generateYourBoard();
    generateEnemyBoard();
    generateShipSelector();
});
var Ship = /** @class */ (function () {
    function Ship(typeString, classSelector, size, color) {
        this.selector = classSelector;
        this.type = typeString;
        this.size = size;
        this.color = color;
    }
    Ship.prototype.getShip = function (id) {
        if (this.selector === id) {
            return this;
        }
        return null;
    };
    return Ship;
}());
var boat = new Ship('Boat', 'ship_boat', 1, 'brown');
var lavantier = new Ship('Lavantier', 'ship_lavantier', 2, 'red');
var submarine = new Ship('Submarine', 'ship_submarine', 3, 'green');
var destroyer = new Ship('Destroyer', 'ship_destroyer', 4, 'blue');
var ships = [boat, lavantier, submarine, destroyer];
function generateShipSelector() {
    var shipBoard = document.getElementById('ship-board');
    var shipsDom = '';
    for (var i = 0; i < ships.length; i++) {
        shipsDom += "<div class=\"row ship-selector\" onclick=\"selectShip('".concat(ships[i].selector, "')\">\n                        <div class=\"col ship-title\">").concat(ships[i].type, "</div>\n                            <div class=\"col ship-preview\">\n                                <div class=\"row\">");
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
function sinkShip(x, y) {
    $("#enemy-board .board-tile[data-x='".concat(x, "'][data-y='").concat(y, "']")).css("background-color", "black");
}
function placeShip(x, y) {
    if (selectedShip == null) {
        console.log('Nice try :)');
        return;
    }
    console.log("Placed ship ".concat(selectedShip.type, " on: x = ").concat(x, " y = ").concat(y));
    if (boardSize[1] > y + selectedShip.size - 1) {
        for (var i = 0; i < selectedShip.size; i++) {
            console.log("Ship coordinate:", $("#your-board .board-tile[data-x='".concat(x, "'][data-y='").concat(y + i, "']")));
            $("#your-board .board-tile[data-x='".concat(x, "'][data-y='").concat(y + i, "']")).css("background-color", "".concat(selectedShip.color));
        }
    }
    else {
        console.log("NIce try :)");
    }
}
//# sourceMappingURL=app.js.map