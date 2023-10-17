var boardSize = [10, 10];
var placedShips = [];
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
});
var Ship = /** @class */ (function () {
    function Ship(typeString, classSelector, size, color, cannon) {
        this.selector = classSelector;
        this.type = typeString;
        this.size = size;
        this.color = color;
        this.cannon = cannon;
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
var boat = new Ship('Boat', 'ship_boat', 1, 'brown', 1);
var lavantier = new Ship('Lavantier', 'ship_lavantier', 2, 'red', 1);
var submarine = new Ship('Submarine', 'ship_submarine', 3, 'green', 2);
var destroyer = new Ship('Destroyer', 'ship_destroyer', 4, 'blue', 2);
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
            $("#your-board .board-tile[data-x='".concat(x + i, "'][data-y='").concat(y, "']")).attr('onClick', "selectShipCannon(".concat(selectedShip.cannon, ")"));
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
function printEnemyBoard(user, board) {
    var name = $("#name").val();
    var array = eval(board);
    console.log(array);
    if (name != user) {
        console.log('testas');
        for (var i = 0; i < array.length; i++) {
            for (var j = 0; j < array[0].length; j++) {
                console.log(array[i][j]);
                switch (array[i][j]) {
                    case 1: {
                        $("#enemy-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background-color", "brown");
                        break;
                    }
                    case 2: {
                        $("#enemy-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background-color", "red");
                        break;
                    }
                    case 3: {
                        $("#enemy-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background-color", "green");
                        break;
                    }
                    case 4: {
                        $("#enemy-board .board-tile[data-x='".concat(j, "'][data-y='").concat(i, "']")).css("background-color", "blue");
                        break;
                    }
                }
            }
        }
    }
}
/*function sinkShip(x: number, y: number) {
    for (let i = x; i < x + selectedCannon; i++) {
        if (i < boardSize[0]) {
            $(`#enemy-board .board-tile[data-x='${i}'][data-y='${y}']`).css(`background-color`, `black`);
        }
    }
}*/ 
//# sourceMappingURL=app.js.map