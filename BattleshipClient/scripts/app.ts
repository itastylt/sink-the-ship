let boardSize = [10, 10];
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
            rowsDom += `<div class="col board-tile" data-x='${i}' data-y='${j}' onclick="placeShip(${i}, ${j});">&nbsp;</div>`;
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
            rowsDom += `<div class="col board-tile" data-x='${i}' data-y='${j}' onclick="sinkShip(${i}, ${j});">&nbsp;</div>`;
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
        if (this.selector === id) {
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
    if (boardSize[1] > y + selectedShip.size-1) {
        for (let i = 0; i < selectedShip.size; i++) {
            console.log("Ship coordinate:", $(`#your-board .board-tile[data-x='${x}'][data-y='${y + i}']`));
            $(`#your-board .board-tile[data-x='${x}'][data-y='${y + i}']`).css(`background-color`, `${selectedShip.color}`);
        }
    } else {
        console.log("NIce try :)");
    }

}
