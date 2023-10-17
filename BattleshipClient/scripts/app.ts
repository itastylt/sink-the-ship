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
    cannon: number;

    getShip(id: string) {
        if (this.selector === id || this.type === id) {
            return this;
        }
        return null;
    }
    
    constructor(typeString: string, classSelector: string, size: number, color: string, cannon: number) {
        this.selector = classSelector;
        this.type = typeString;
        this.size = size;
        this.color = color;
        this.cannon = cannon;
    }
}

class PlacedShip {
    type: String;
    x_start: number;
    y_start: number;
    size: number;
    angle: number;
    cannon: number;

    constructor(typeString: string, x1: number, y1: number, size: number, angle :number, cannon: number) {
        this.type = typeString;
        this.x_start = x1;
        this.y_start = y1;
        this.size = size;
        this.angle = angle;
        this.cannon = cannon;
    }
}

const boat = new Ship('Boat', 'ship_boat', 1, 'brown', 1);
const lavantier = new Ship('Lavantier', 'ship_lavantier', 2, 'red', 2);
const submarine = new Ship('Submarine', 'ship_submarine', 3, 'green', 3);
const destroyer = new Ship('Destroyer', 'ship_destroyer', 4, 'blue', 4);

let ships = [boat, lavantier, submarine, destroyer];

function generateShipSelector() {
    let shipBoard = document.getElementById('ship-board');
    let shipsDom = '';
    for (let i = 0; i < ships.length; i++) {
        shipsDom += `<div class="row ship-selector" id=${ships[i].type} onclick="selectShip('${ships[i].selector}')">
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


function placeShip(x: number, y: number) {
    if (selectedShip == null) {
        console.log('Nice try :)');
        return;
    }

    console.log(`Placed ship ${selectedShip.type} on: x = ${x} y = ${y}`);
    if (boardSize[1] > x + selectedShip.size - 1) {

        for (let i = 0; i < selectedShip.size; i++) {
            console.log("Ship coordinate:", $(`#your-board .board-tile[data-x='${x + i}'][data-y='${y }']`));
            $(`#your-board .board-tile[data-x='${x + i}'][data-y='${y}']`).css(`background-color`, `${selectedShip.color}`);
            $(`#your-board .board-tile[data-x='${x + i}'][data-y='${y}']`).addClass('ship');
            $(`#your-board .board-tile[data-x='${x + i}'][data-y='${y}']`).attr('onClick', `selectShipCannon(${selectedShip.cannon})`);
        }

        placedShips.push(new PlacedShip(selectedShip.type, x, y, selectedShip.size, 90, selectedShip.cannon));
        
    } else {
        console.log("Nice try :)");
    }
    $(`#${selectedShip.type}`).css('display', 'none');
    selectedShip = null;

}

let selectedCannon = 1;

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

function printEnemyBoard(user: string, board) {
    let name = $("#name").val();

    let array = eval(board);
    console.log(array);

    if (name != user) {
        console.log('testas');
        for (let i = 0; i < array.length; i++) {
            for (let j = 0; j < array[0].length; j++) {
                console.log(array[i][j]);
                switch (array[i][j]) {
                    case 1: {
                        $(`#enemy-board .board-tile[data-x='${j}'][data-y='${i}']`).css(`background-color`, `brown`);
                        break;
                    }
                    case 2: {
                        $(`#enemy-board .board-tile[data-x='${j}'][data-y='${i}']`).css(`background-color`, `red`);
                        break;
                    }
                    case 3: {
                        $(`#enemy-board .board-tile[data-x='${j}'][data-y='${i}']`).css(`background-color`, `green`);
                        break;
                    }
                    case 4: {
                        $(`#enemy-board .board-tile[data-x='${j}'][data-y='${i}']`).css(`background-color`, `blue`);
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