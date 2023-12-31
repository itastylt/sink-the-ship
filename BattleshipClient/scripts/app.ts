﻿let boardSize = [10, 10];
let placedShips = [];
let round = 1;
function consoleWrite(message) {
    var consoleElem = $('.console-content');
    console.log("testas");
    var dom = "<div class=\"console-row\">".concat(message, "</div>");
    consoleElem.append(dom);
}

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
    generateCannonSelector();
});

class Ship {
    type: String;
    selector: String;
    size: number
    color: String;
    cannon: number;
    cannonPreview: String;

    getShip(id: string) {
        if (this.selector === id || this.type === id) {
            return this;
        }
        return null;
    }
    
    constructor(typeString: string, classSelector: string, size: number, color: string, cannon: number, preview: string) {
        this.selector = classSelector;
        this.type = typeString;
        this.size = size;
        this.color = color;
        this.cannon = cannon;
        this.cannonPreview = preview;
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
const boatDOM = `<div class="row">
                    <div class="col board-tile" style="color:red;">X</div>
                    <div class="col board-tile">&nbsp;</div>
                </div>
                <div class="row">
                    <div class="col board-tile" style="background-color: #C4F4FF;">&nbsp;</div>
                    <div class="col board-tile" style="background-color: blue;">&nbsp;</div>
                </div>`;

const lavantierDOM = `<div class="row">
                        <div class="col board-tile" style="background-color: #938F92; color: red;">X</div>
                        <div class="col board-tile" style="background-color: #938F92;">&nbsp;</div>
                     </div>
                     <div class="row">
                        <div class="col board-tile">&nbsp;</div>
                        <div class="col board-tile">&nbsp;</div>
                     </div>`;

const submarineDOM = `<div class="row">
                        <div class="col board-tile" style="background-color: #938F92; color: red;">X</div>
                        <div class="col board-tile">&nbsp;</div>
                     </div>
                     <div class="row">
                        <div class="col board-tile" style="background-color: #938F92;">&nbsp;</div>
                        <div class="col board-tile">&nbsp;</div>
                     </div>`;

const destroyerDOM = `<div class="row">
                        <div class="col board-tile" style="background-color: #938F92; color: red;">X</div>
                        <div class="col board-tile">&nbsp;</div>
                     </div>
                     <div class="row">
                        <div class="col board-tile">&nbsp;</div>
                        <div class="col board-tile" style="background-color: #938F92;">&nbsp;</div>
                     </div>`;

const boat = new Ship('Boat', 'ship_boat', 1, 'brown', 1, boatDOM);
const lavantier = new Ship('Lavantier', 'ship_lavantier', 2, 'red', 2, lavantierDOM);
const submarine = new Ship('Submarine', 'ship_submarine', 3, 'green', 3, submarineDOM);
const destroyer = new Ship('Destroyer', 'ship_destroyer', 4, 'blue', 4, destroyerDOM);

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
function handleSplashScreen(state: boolean) {

    if (state == true) {
        $(".hide-on-join").addClass("d-none");
    } else {
        $(".hide-on-join").removeClass("d-none");
    }

    $('.ready-screen-wrapper').slideToggle("slow");

}


function handleTurnScreen(player: string) {
    let name = $("#name").val();

    if (name == player) {
        $('.turn-screen-title').text("Your turn");
    } else {
        $('.turn-screen-title').text("Enemies turn");
    }
    let wrapper = $('.turn-screen-wrapper');

    if (wrapper.is('.turn-hide')) {
        wrapper.removeClass('turn-hide');
    }
    wrapper.toggleClass('turn-show');
    setTimeout(() => { wrapper.toggleClass('turn-show'); }, 2000);

}

function printBoards(user: string, board, debug_mode: boolean) {
    let name = $("#name").val();

    let array = eval(board);

    $('.power-up-panel').addClass('show');

    if (name != user) {
        $(`#enemy-board .board-tile`).css('background', '');
        for (let i = 0; i < array.length; i++) {
            for (let j = 0; j < array[0].length; j++) {
                
                switch (array[i][j]) {
                    case 1: {
                        if (debug_mode) {
                            $(`#enemy-board .board-tile[data-x='${j}'][data-y='${i}']`).css("background", "url('img/boat.png'), brown");
                        }

                        break;
                    }
                    case 2: {
                        if (debug_mode) {
                            $(`#enemy-board .board-tile[data-x='${j}'][data-y='${i}']`).css("background", "url('img/submarine.png'), red");
                        }

                        break;
                    }
                    case 3: {
                        if (debug_mode) {
                            $(`#enemy-board .board-tile[data-x='${j}'][data-y='${i}']`).css("background", "url('img/lavantier.png'), green");
                        }
                        break;
                    }
                    case 4: {
                        if (debug_mode) {
                            $(`#enemy-board .board-tile[data-x='${j}'][data-y='${i}']`).css("background", "url('img/destroyer.png'), blue");
                        }
                       
                        break;
                    }
                    case 5: {
                        if (debug_mode) {
                            $(`#enemy-board .board-tile[data-x='${j}'][data-y='${i}']`).css(`background`, `black`);
                        }
                        break;
                    }
                    case 6: {
                        if (debug_mode) {
                            $(`#enemy-board .board-tile[data-x='${j}'][data-y='${i}']`).css(`background`, `yellow`);
                        }
                        break;
                    }
                    case -99: {
                        $(`#enemy-board .board-tile[data-x='${j}'][data-y='${i}']`).css(`background`, `#938F92`);
                        break;
                    }
                    case -1: {
                        $(`#enemy-board .board-tile[data-x='${j}'][data-y='${i}']`).css("background", "url('img/boat.png'), #7A5C56");
                        break;
                    }
                    case -2: {
                        $(`#enemy-board .board-tile[data-x='${j}'][data-y='${i}']`).css("background", "url('img/submarine.png'), #FF947D");
                        break;
                    }
                    case -3: {
                        $(`#enemy-board .board-tile[data-x='${j}'][data-y='${i}']`).css("background", "url('img/lavantier.png'), #C6FFC4");
                        break;
                    }
                    case -4: {
                        $(`#enemy-board .board-tile[data-x='${j}'][data-y='${i}']`).css("background", "url('img/destroyer.png'), #C4F4FF");
                        break;
                    }
                    case -6: {
                        $(`#enemy-board .board-tile[data-x='${j}'][data-y='${i}']`).css(`background-color`, `#ECFDA2`);
                        break;
                    }

                }
            }
        }
    } else {
        $(`#your-board .board-tile`).css('background-color', '');
        for (let i = 0; i < array.length; i++) {
            for (let j = 0; j < array[0].length; j++) {
                $(`#your-board .board-tile[data-x='${j}'][data-y='${i}']`).css('background-color', '');
                switch (array[i][j]) {
                    case 1: {
                        $(`#your-board .board-tile[data-x='${j}'][data-y='${i}']`).css("background", "url('img/boat.png'), brown");
                        break;
                    }
                    case 2: {
                        $(`#your-board .board-tile[data-x='${j}'][data-y='${i}']`).css("background", "url('img/submarine.png'), red");
                        break;
                    }
                    case 3: {
                        $(`#your-board .board-tile[data-x='${j}'][data-y='${i}']`).css("background", "url('img/lavantier.png'), green");
                        break;
                    }
                    case 4: {
                        $(`#your-board .board-tile[data-x='${j}'][data-y='${i}']`).css("background", "url('img/destroyer.png'), blue");
                        break;
                    }
                    case 5: {
                        $(`#your-board .board-tile[data-x='${j}'][data-y='${i}']`).css(`background`, `black`);
                        break;
                    }
                    case 6: {
                        $(`#your-board .board-tile[data-x='${j}'][data-y='${i}']`).css(`background`, `yellow`);
                        break;
                    }
                    case -99: {
                        $(`#your-board .board-tile[data-x='${j}'][data-y='${i}']`).css(`background`, `#938F92`);
                        break;
                    }
                    case -1: {
                        $(`#your-board .board-tile[data-x='${j}'][data-y='${i}']`).css("background", "url('img/boat.png'), #7A5C56");
                        break;
                    }
                    case -2: {
                        $(`#your-board .board-tile[data-x='${j}'][data-y='${i}']`).css("background", "url('img/submarine.png'), #FF947D");
                        break;
                    }
                    case -3: {
                        $(`#your-board .board-tile[data-x='${j}'][data-y='${i}']`).css("background", "url('img/lavantier.png'), #C6FFC4");
                        break;
                    }
                    case -4: {
                        $(`#your-board .board-tile[data-x='${j}'][data-y='${i}']`).css("background", "url('img/destroyer.png'), #C4F4FF");
                        break;
                    }
                    case -6: {
                        $(`#your-board .board-tile[data-x='${j}'][data-y='${i}']`).css(`background-color`, `#ECFDA2`);
                        break;
                    }
                }
            }
        }
    }
}

function handleUnclonePowerUp(player: string) {
    let name = $("#name").val();
    $(".power-up-unclone").addClass("disable");
}
function handleClonePowerUp(player: string) {
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
    let shipBoard = document.getElementById('cannon-board');
    let shipsDom = '';

    for (let i = 0; i < ships.length; i++) {
        if (i == 0) {
            shipsDom += `<div class="row ship-selector cannon-active" id="cannon-${ships[i].cannon}" onclick="selectShipCannon(${ships[i].cannon})">
                            <div class="col ship-title">${ships[i].type}</div>
                            <div class="col cannon-preview">
                            ${ships[i].cannonPreview}
                        </div>
                     </div>`;
        } else {
            shipsDom += `<div class="row ship-selector" id="cannon-${ships[i].cannon}" onclick="selectShipCannon(${ships[i].cannon})">
                            <div class="col ship-title">${ships[i].type}</div>
                            <div class="col cannon-preview">
                            ${ships[i].cannonPreview}
                        </div>
                     </div>`;
        }
    }

    shipBoard.innerHTML = shipsDom;
}

function handleCannonBoard() {
    $(".ship-selector").removeClass('cannon-active');
    $(`#cannon-${selectedCannon}`).addClass('cannon-active');
}

function handleRound() {
    $('.show-on-join').removeClass('d-none');
}
function handleRoundScreen(user: string) {
    round++;
    let name = $("#name").val();
    let yourDOM = document.getElementById('your-board');
    let enemyDOM = document.getElementById('enemy-board');
    yourDOM.innerHTML = "";
    enemyDOM.innerHTML = "";

    if (round == 2) {
        boardSize = [12, 12];
    } else if (round == 3) {
        boardSize = [13, 13];
    }

    generateYourBoard();
    generateEnemyBoard();
    placedShips = [];
    $(".round-screen-title").text(`Round ${round}`);
    let wrapper = $('.round-screen-wrapper');
    $('.show-on-round').removeClass('d-none');
    $('.show-on-join').addClass('d-none');
    $('.ship-selector').attr("style", "");
    

    if (wrapper.is('.round-hide')) {
        wrapper.removeClass('round-hide');
    }

    wrapper.toggleClass('round-show');
    setTimeout(() => { wrapper.toggleClass('round-show'); }, 2000);
}

function handleDamageCount(damageCount: string) {
    $('#shotCount').text(damageCount);
}
function handleCurrentDamageCount(currentDamageCount) {
    $('#currentShotCount').text(currentDamageCount);
}
