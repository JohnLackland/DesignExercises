using ConnectFour.Enums;

namespace ConnectFour.Models
{
    public class Player(string playerName, CellState playerColor)
    {
        public string PlayerName { get; set; } = playerName;
        public CellState PlayerColor { get; set; } = playerColor;
    }
}