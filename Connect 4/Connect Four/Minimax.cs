using System;

namespace Connect_Four
{
    class Minimax
    {
        protected int col;
        protected GameStatus evaluation;
        protected Grid grid;
        protected Players players;
 
        // finds the best move
        public Minimax(ref Grid grid, ref Players players)
        { 
            this.grid = grid; this.players = players;

            evaluation = new GameStatus(ref players, ref grid);

            NextMove();
        }

        // minimax algorithm
        private int Prediction(int depth, bool bMax)
        {
            if(!evaluation.GetGameStatus())
            {
                if (evaluation.GetWinner() == 0) return 10;
                else if (evaluation.GetWinner() == 1) return -10;
                else return 0;
            }

            if (bMax)
            {
                int points = 1000;

                for (int i = 0; i < grid.GetYSize(); i++)
                {
                    // make move
                    if (grid.MakeMove(0, i))
                    {
                        if (depth != 3)
                            points = Math.Max(points, Prediction(depth + 1, !bMax));

                        // Undo move
                        grid.UndoMove(i);
                    }
                }
                    return points; 
            }

            else
            {
                int points = -1000;

                for (int i = 0; i < grid.GetYSize(); i++)
                {
                    // make move
                    if(grid.MakeMove(1, i))
                    {
                        if (depth != 3)
                            points = Math.Min(points, Prediction(depth + 1, !bMax));

                        // Undo move
                        grid.UndoMove(i);
                    }
                }

                return points;
            }
        }

        void NextMove()
        {
            int suggestiveMove = -1000;
            col = -1;

            for(int i = 0; i < grid.GetYSize(); i++)
            {
                int moveVal = 0;
                if (grid.MakeMove(0, i))
                {
                    moveVal = Prediction(1, false);

                    grid.UndoMove(i);
                }

                if (moveVal > suggestiveMove)
                    col = i;

                if(col == -1 && moveVal == -1000)
                    col = i; 
            }
            
            grid.MakeMove(1, col);
        }
    }
}


