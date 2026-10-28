using Microsoft.AspNetCore.SignalR;
using TicTacToe.Services;

namespace TicTacToe.Hubs;

public class GameHub : Hub
{
    private readonly GameManager _gameManager;

    public GameHub(GameManager manager)
    {
        _gameManager = manager;
    }

    public async Task CreateRoom(string roomId)
    {
        var room = _gameManager.CreateRoom(roomId);

        room.PlayerX = Context.ConnectionId;

        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            roomId);

        await Clients.Caller.SendAsync(
            "RoomCreated",
            roomId,
            "X");
    }

    public async Task JoinRoom(string roomId)
    {
        var room = _gameManager.GetRoom(roomId);

        if (room == null)
        {
            await Clients.Caller.SendAsync(
                "Error",
                "Room not found");

            return;
        }

        room.PlayerO = Context.ConnectionId;

        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            roomId);

        await Clients.Group(roomId)
            .SendAsync("GameStarted");
    }

    public async Task MakeMove(
        string roomId,
        int position)
    {
        var room = _gameManager.GetRoom(roomId);

        if (room is null || room.IsFinished)
            return;

        var symbol =
            room.PlayerX == Context.ConnectionId
                ? "X"
                : "O";

        if (symbol != room.CurrentTurn)
            return;

        if (!string.IsNullOrEmpty(room.Board[position]))
            return;

        room.Board[position] = symbol;

        if (CheckWinner(room.Board, symbol))
        {
            room.IsFinished = true;

            await Clients.Group(roomId)
                .SendAsync(
                    "GameWon",
                    symbol,
                    room.Board);

            return;
        }

        if (room.Board.All(x => !string.IsNullOrEmpty(x)))
        {
            room.IsFinished = true;

            await Clients.Group(roomId)
                .SendAsync(
                    "GameDraw",
                    room.Board);

            return;
        }

        room.CurrentTurn =
            symbol == "X" ? "O" : "X";

        await Clients.Group(roomId)
            .SendAsync(
                "MoveMade",
                room.Board,
                room.CurrentTurn);
    }

    private bool CheckWinner(
        string[] board,
        string player)
    {
        int[,] wins =
        {
            {0,1,2},
            {3,4,5},
            {6,7,8},
            {0,3,6},
            {1,4,7},
            {2,5,8},
            {0,4,8},
            {2,4,6}
        };

        for (int i = 0; i < wins.GetLength(0); i++)
        {
            if (
                board[wins[i, 0]] == player &&
                board[wins[i, 1]] == player &&
                board[wins[i, 2]] == player)
            {
                return true;
            }
        }

        return false;
    }
}