namespace TicTacToe2
{
    public class Game
    {
        private string[,] _board = new string[3, 3];
        private string _currentPlayer = "X";

        public bool MakeMove(string player, int x, int y)
        {
            if (_board[x, y] == null && player == _currentPlayer)
            {
                _board[x, y] = player;
                _currentPlayer = player == "X" ? "O" : "X";
                return true;
            }
            return false;
        }

        public string CheckWinner()
        {
            string[] players = { "X", "O" };
            foreach (var player in players)
            {
                // Check rows, columns, and diagonals
                if ((_board[0, 0] == player && _board[0, 1] == player && _board[0, 2] == player) ||
                    (_board[1, 0] == player && _board[1, 1] == player && _board[1, 2] == player) ||
                    (_board[2, 0] == player && _board[2, 1] == player && _board[2, 2] == player) ||
                    (_board[0, 0] == player && _board[1, 0] == player && _board[2, 0] == player) ||
                    (_board[0, 1] == player && _board[1, 1] == player && _board[2, 1] == player) ||
                    (_board[0, 2] == player && _board[1, 2] == player && _board[2, 2] == player) ||
                    (_board[0, 0] == player && _board[1, 1] == player && _board[2, 2] == player) ||
                    (_board[0, 2] == player && _board[1, 1] == player && _board[2, 0] == player))
                {
                    return player;
                }
            }
            return null;
        }
    }
}
