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

function generateBoard() {
    console.log('generating');
    let boardDOM = document.getElementsByClassName('battleship-board');
    let rowsDom = '';
    for (let i = 0; i < boardSize[0]; i++) {

        rowsDom += '<div class="row">';

        for (let i = 0; i < boardSize[1]; i++) {
            rowsDom += '<div class="col board-tile">&nbsp;</div>';
        }

        rowsDom += '</div>';
    }

    for (let i = 0; i < boardDOM.length; i++) {
        boardDOM[i].innerHTML = rowsDom;
    }

}

domReady(() => {
    generateBoard();
});