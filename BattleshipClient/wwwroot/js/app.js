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
function generateBoard() {
    console.log('generating');
    var boardDOM = document.getElementsByClassName('battleship-board');
    var rowsDom = '';
    for (var i = 0; i < boardSize[0]; i++) {
        rowsDom += '<div class="row">';
        for (var i_1 = 0; i_1 < boardSize[1]; i_1++) {
            rowsDom += '<div class="col board-tile">&nbsp;</div>';
        }
        rowsDom += '</div>';
    }
    for (var i = 0; i < boardDOM.length; i++) {
        boardDOM[i].innerHTML = rowsDom;
    }
}
domReady(function () {
    generateBoard();
});
//# sourceMappingURL=app.js.map