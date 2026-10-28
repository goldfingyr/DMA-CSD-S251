using TicTacToe.Models;

namespace TicTacToe.Services;

public class GameManager
{
    private readonly Dictionary<string, GameRoom> _rooms = new();

    public GameRoom CreateRoom(string roomId)
    {
        var room = new GameRoom
        {
            RoomId = roomId
        };

        _rooms[roomId] = room;

        return room;
    }

    public GameRoom? GetRoom(string roomId)
    {
        return _rooms.GetValueOrDefault(roomId);
    }

    public bool RoomExists(string roomId)
    {
        return _rooms.ContainsKey(roomId);
    }
}