/* Author: Edward Patch */

using System;

namespace Connect_Four
{
    class Program
    {
        protected static string[] names =
        { "John", "Mary" };

        protected static Players play;
        protected static Grid grid;
        protected static GameStatus gs;
        protected static Minimax mx;

        static void Main(string[] args)
        {
            PlayerSetup();
            GridSetup();
            GameController();
        }

        static void PlayerSetup()
        {
            play = new Players(names, 'O');

            play.SetPlayerName("James", 0);
            play.SetPlayerName("Wiliams", 1);

            play.SetPlayerIcon('X');
        }

        static void GridSetup()
        {
            // default grid
            grid = new Grid(ref play, 6, 7);
        }

        static void GameController()
        {
            gs = new GameStatus(ref play, ref grid);

            // run game
            while (gs.GetGameStatus())
            {
                grid.DrawGrid();

                // AI will always have index of 1
                if (play.GetPlayerTurn() == 0)
                {
                    Console.Write(play.GetPlayerName(play.GetPlayerTurn()) + "'s turn. Choose slot (1-" + grid.GetYSize() + "): ");
                    grid.MakeMove(play.GetPlayerTurn(), Convert.ToInt32(Console.ReadLine()) - 1);
                }

                // AI will now make move
                else mx = new Minimax(ref grid, ref play);
                
                play.NextPlayerTurn();
               
                Console.Clear();
                
            }

            if(gs.GetWinner() == -1) Console.WriteLine("Game Ended | Draw");

            else
                Console.WriteLine("Game Ended | Winner: " + play.GetPlayerName(gs.GetWinner()));
        }
    }
}
