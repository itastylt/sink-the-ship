let boardSize = [10, 10];
let placedShips = [];

function domReady(cb: Function): void {
    if (document.readyState === "complete" || document.readyState === "interactive") {
        cb();
    } else {
        document.addEventListener("DOMContentLoaded", (event: Event) => {
            cb();
        });
    }
}
function generateYourBoard() {
    console.log('generating');
    let boardDOM = document.getElementById('your-board');
    let rowsDom = '';
    for (let i = 0; i < boardSize[0]; i++) {

        rowsDom += '<div class="row">';

        for (let j = 0; j < boardSize[1]; j++) {
            rowsDom += `<div class="col board-tile" data-x='${j}' data-y='${i}' onclick="placeShip(${j}, ${i});">&nbsp;</div>`;
        }

        rowsDom += '</div>';
    }

    boardDOM.innerHTML = rowsDom;
}

function generateEnemyBoard() {
    let boardDOM = document.getElementById('enemy-board');
    let rowsDom = '';
    for (let i = 0; i < boardSize[0]; i++) {

        rowsDom += '<div class="row">';

        for (let j = 0; j < boardSize[1]; j++) {
            rowsDom += `<div class="col board-tile" data-x='${j}' data-y='${i}' onclick="sinkShip(${j}, ${i});">&nbsp;</div>`;
        }

        rowsDom += '</div>';
    }
    boardDOM.innerHTML = rowsDom;
}

domReady(() => {
    generateYourBoard();
    generateEnemyBoard();
    generateShipSelector();
});

class Ship {
    type: String;
    selector: String;
    size: number
    color: String;

    getShip(id: string) {
        if (this.selector === id || this.type === id) {
            return this;
        }
        return null;
    }
    
    constructor(typeString: string, classSelector: string, size: number, color: string) {
        this.selector = classSelector;
        this.type = typeString;
        this.size = size;
        this.color = color;
    }
}

class PlacedShip {
    type: String;
    x_start: number;
    y_start: number;
    size: number;
    angle: number;

    constructor(typeString: string, x1: number, y1: number, size: number, angle :number) {
        this.type = typeString;
        this.x_start = x1;
        this.y_start = y1;
        this.size = size;
        this.angle = angle;
    }
}

const boat = new Ship('Boat', 'ship_boat', 1, 'brown');
const lavantier = new Ship('Lavantier', 'ship_lavantier', 2, 'red');
const submarine = new Ship('Submarine', 'ship_submarine', 3, 'green');
const destroyer = new Ship('Destroyer', 'ship_destroyer', 4, 'blue');

let ships = [boat, lavantier, submarine, destroyer];

function generateShipSelector() {
    let shipBoard = document.getElementById('ship-board');
    let shipsDom = '';
    for (let i = 0; i < ships.length; i++) {
        shipsDom += `<div class="row ship-selector" onclick="selectShip('${ships[i].selector}')">
                        <div class="col ship-title">${ships[i].type}</div>
                            <div class="col ship-preview">
                                <div class="row">`;
        for (let j = 0; j < ships[i].size; j++) {
            shipsDom += `<div class="col board-tile" style="background-color:${ships[i].color};" >&nbsp;</div>`;
        }

        shipsDom += `</div>
            </div>
        </div>`;
                         
    }

    shipBoard.innerHTML = shipsDom;
}

let selectedShip = null;

function selectShip(shipSelector: string) {
    for (let i = 0; i < ships.length; i++) {
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

function sinkShip(x: number, y: number) {
    $(`#enemy-board .board-tile[data-x='${x}'][data-y='${y}']`).css(`background-color`, `black`);
}
function placeShip(x: number, y: number) {
    if (selectedShip == null) {
        console.log('Nice try :)');
        return;
    }

    console.log(`Placed ship ${selectedShip.type} on: x = ${x} y = ${y}`);
    if (boardSize[1] > x + selectedShip.size - 1) {

        for (let i = 0; i < selectedShip.size; i++) {
            console.log("Ship coordinate:", $(`#your-board .board-tile[data-x='${x + i}'][data-y='${y }']`));
            $(`#your-board .board-tile[data-x='${x + i}'][data-y='${y }']`).css(`background-color`, `${selectedShip.color}`);
        }

        placedShips.push(new PlacedShip(selectedShip.type, x, y, selectedShip.size, 90));
        
    } else {
        console.log("Nice try :)");
    }

}

function placedShipsAsString() {
    let string = "[";
    for (let i = 0; i < placedShips.length; i++) {
        string += `{"Type": "${placedShips[i].type}", "X": ${placedShips[i].x_start}, "Y": ${placedShips[i].y_start}, "Size": ${placedShips[i].size}, "Angle": ${placedShips[i].angle}}`;
        if (i < placedShips.length - 1) {
            string += ',';
        }
    }
    string += "]";

    return string;
}

function printEnemyBoard(user: string, json: string) {
    let name = $("#name").val();
    console.log(name);
    if (name !== user) {
        var objects = $.parseJSON(json);
        console.log(objects);
        for (var ship in objects) {
            printEnemyShip(objects[ship]);
        }
    }
}

function printEnemyShip(enemyShip) {
    let printedShip = null;

    for (let ship in ships) {
        console.log(ships[ship]);
        if (ships[ship].type === enemyShip.Type) {
            printedShip = ships[ship];
            break;
        }
    }
    let color = printedShip.color;

    for (let i = enemyShip.X; i < enemyShip.X + printedShip.size; i++) {
        $(`#enemy-board .board-tile[data-x='${i}'][data-y='${enemyShip.Y}']`).css(`background-color`, `${color}`);
    }
}
