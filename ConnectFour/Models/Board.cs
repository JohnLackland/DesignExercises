using ConnectFour.Enums;

namespace ConnectFour.Models
{
    public class Board
    {
        private static int ConnectedDotCountToWin = 4;

        private readonly int _rows = 6;
        private readonly int _columns = 7;
        private CellState[,] _grid;

        public Board()
        {
            InitGrid();
        }

        public (int tokenRow, int tokenColumn) PlaceToken(int column, CellState token)
        {
            if (column < 0 || column >= _columns)
                throw new ArgumentOutOfRangeException(nameof(column), $"Column vlaue {column} is out of range");
            if (token == CellState.Empty)
                throw new ArgumentException($"Token can not be empty", nameof(token));

            for (int row = _rows - 1; row < _rows; row--)
            {
                if (_grid[row, column] == CellState.Empty)
                {
                    _grid[row, column] = token;
                    return (row, column);
                }
            }

            throw new ArgumentException($"The specified column is already full.");
        }

        public void InitGrid()
        {
            _grid = new CellState[_rows, _columns];
            for (int row = 0; row < _rows; row++)
            {
                for (int col = 0; col < _columns; col++)
                {
                    _grid[row, col] = CellState.Empty;
                }
            }
        }

        public void PrintBoard()
        {
            Console.WriteLine();
            Console.WriteLine("| 1 | 2 | 3 | 4 | 5 | 6 | 7 |");
            for (int row = 0; row < _rows; row++)
            {
                Console.WriteLine("-----------------------------");
                Console.Write("| ");
                for (int column = 0; column < _columns; column++)
                {
                    var currentTokenColor = _grid[row, column];
                    if(currentTokenColor == CellState.Empty)
                        Console.Write("  | ");
                    if (currentTokenColor == CellState.MArkedWithO)
                        Console.Write("O | ");
                    if (currentTokenColor == CellState.MarkedWithX)
                        Console.Write("X | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("-----------------------------");
        }

        public bool CheckForWin(int tokenRow, int tokenColumn)
        {
            var tokenColor = _grid[tokenRow, tokenColumn];

            return CheckLine(tokenRow, 0, 0, 1, tokenColor) || // check horizontal
                CheckLine(0, tokenColumn, 1, 0, tokenColor) || // check vertical
                CheckDiagonals(tokenRow, tokenColumn, tokenColor); // Check both diagonals both way
        }

        public bool IsColumnFull(int column)
        {
            return _grid[0,column] != CellState.Empty;
        }

        private bool CheckLine(int startRow, int startColumn, int incrementRow, int incrementCol, CellState token)
        {
            int counter = 0;
            for (int row = startRow, col = startColumn; row < _rows && col < _columns; row += incrementRow, col += incrementCol) 
            {
                if (_grid[row, col] == token)
                    counter++;
                else
                    counter = 0;

                if (counter == ConnectedDotCountToWin)
                    return true;
            }

            return false;
        }

        private bool CheckDiagonals(int lastTokenRow, int lastTokeCol, CellState token)
        {
            // check ascending diagonal
            int counter = 0;
            for (int r = 0; r < _rows; r++)
            {
                var c = lastTokenRow + lastTokeCol - r;
                if(c >= 0 && c < _columns && _grid[r, c] == token)
                    counter++;
                else
                    counter = 0;

                if (counter == ConnectedDotCountToWin)
                    return true;
            }

            // check descending diagonal
            counter = 0;
            for (int r = 0; r < _rows; r++)
            {
                var c = lastTokeCol - lastTokenRow + r;
                if (c >= 0 && c < _columns && _grid[r, c] == token)
                    counter++;
                else
                    counter = 0;

                if (counter == ConnectedDotCountToWin)
                    return true;
            }

            return false;
        }
    }
}