namespace ConnectFour.Models
{
    public class Game
    {
        private readonly Board _board;
        private readonly int _targetScore;
        private readonly Player[] _players;
        private readonly Dictionary<Player, int> _score;

        public Game(Player[] players, int _targetScore)
        {
            this._targetScore = _targetScore;
            _players = players;

            _score = [];
            foreach (var player in _players)
                _score.Add(player, 0);
            _board = new Board();
        }

        public void Play()
        {
            var maxScore = 0;
            Player winner = null;
            Console.WriteLine("Game started");
            while (maxScore != _targetScore)
            {
                winner = PlayRound();

                if (winner == null)
                {
                    Console.WriteLine("Restarting the game...");
                    _board.InitGrid();
                    foreach (var player in _players)
                    {
                        _score[player] = 0; // Reset scores
                    }
                    maxScore = 0;
                    continue;
                }

                _score[winner]++;
                maxScore = Math.Max(maxScore, _score[winner]);

                _board.InitGrid();
            }

            Console.WriteLine($"The winner of the game is {winner.PlayerName}");
        }

        private Player PlayRound()
        {
            Console.WriteLine("Round started");
            _board.PrintBoard();
            while (true)
            {
                foreach (var player in _players)
                {
                    Console.WriteLine($"It is {player.PlayerName}'s turn, please choose a column from 1 to 7 to place your token");
                    int column = GetValidColumnInput();
                    if (column == -1)
                        return null;

                    var (tokenRow, tokenColumn) = _board.PlaceToken(column, player.PlayerColor);
                    _board.PrintBoard();
                    if (_board.CheckForWin(tokenRow, tokenColumn))
                    {
                        Console.WriteLine($"{player.PlayerName} won the round");
                        return player;
                    }
                }
            }
        }

        private int GetValidColumnInput()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "RESTART")
                {
                    return -1;
                }
                if (int.TryParse(input, out int column) && column >= 1 && column <= 7)
                {
                    column--; // Convert to 0-based index
                    if (_board.IsColumnFull(column))
                    {
                        Console.WriteLine("The chosen column is already full, please choose another one");
                        continue;
                    }
                    return column;
                }
                Console.WriteLine("Invalid input. Please choose a number between 1 and 7.");
            }
        }
    }
}