const connection =
    new signalR.HubConnectionBuilder()
        .withUrl("/gamehub")
        .build();

let roomId = "";

const board =
    document.getElementById("board");

for (let i = 0; i < 9; i++) {
    const cell =
        document.createElement("div");

    cell.className = "cell";

    cell.onclick = () =>
        connection.invoke(
            "MakeMove",
            roomId,
            i);

    board.appendChild(cell);
}

connection.on(
    "MoveMade",
    (state, turn) => {
        render(state);
        document.getElementById("status")
            .innerText =
            `Turn: ${turn}`;
    });

connection.on(
    "GameWon",
    (winner, state) => {
        render(state);

        document.getElementById("status")
            .innerText =
            `${winner} wins`;
    });

connection.on(
    "GameDraw",
    (state) => {
        render(state);

        document.getElementById("status")
            .innerText =
            "Draw";
    });

function render(state) {
    document
        .querySelectorAll(".cell")
        .forEach((cell, index) =>
            cell.innerText = state[index]);
}

async function createRoom() {
    roomId =
        document.getElementById("room").value;

    await connection.invoke(
        "CreateRoom",
        roomId);
}

async function joinRoom() {
    roomId =
        document.getElementById("room").value;

    await connection.invoke(
        "JoinRoom",
        roomId);
}

connection.start();
