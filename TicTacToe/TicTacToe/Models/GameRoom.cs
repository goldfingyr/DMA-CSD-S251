namespace TicTacToe.Models;

public class GameRoom
{
    public string RoomId { get; set; } = string.Empty;

    public string? PlayerX { get; set; }

    public string? PlayerO { get; set; }

    public string CurrentTurn { get; set; } = "X";

    public string[] Board { get; set; } =
    [
        "", "", "",
        "", "", "",
        "", "", ""
    ];

    public bool IsFinished { get; set; }
}