using ConnectFour.Enums;
using ConnectFour.Models;

namespace DesignExercises
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, the game will start soon...");
            var player1 = new Player("Player 1", CellState.MArkedWithO);
            var player2 = new Player("Player 2 ", CellState.MarkedWithX);
            var game = new Game([player1, player2], 2);
            game.Play();
        }
    }
}
