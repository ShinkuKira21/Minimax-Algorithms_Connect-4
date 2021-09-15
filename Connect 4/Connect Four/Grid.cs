/* Author: Edward Patch */

using System;

namespace Connect_Four 
{
    public class Grid
    {
        protected Players players;
        protected char[][] grid;

        // x = H and Y = W
        int x, y; // Grid size
        char gridIcon;

        // default: 3 x 3 | Icon: .
        public Grid(ref Players players)
        {
            this.players = players;

            x = 7;
            y = 6;

            gridIcon = '.';

            InitialiseGrid();
        }

        // default: ySize: -1 (Sets y to x) Icon: .
        public Grid(ref Players players, int xSize, int ySize = -1, char gridIcon = '.')
        {
            this.players = players;

            if (CheckSizes(xSize, ref ySize))
            { x = xSize; y = ySize; }

            else { x = 4; y = 4; }

            if (CheckIcon(gridIcon))
                this.gridIcon = gridIcon;

            InitialiseGrid();
        }

        // Added to work with MiniMax
        public Grid(ref Players players, char[][] grid, int xSize, int ySize, char gridIcon)
        { this.players = players; this.grid = grid; x = xSize; y = ySize; this.gridIcon = gridIcon; }

        public object Clone()
        { return this.MemberwiseClone(); }

        public char GetGridIcon()
        { return gridIcon; }

        public char[][] GetGrid()
        { return grid; }

        public int GetXSize()
        { return x; }

        public int GetYSize()
        { return y; }

        //  returns string of grid.
        public string OutputGrid()
        {
            string gridOutput = null;

            gridOutput += "    ";

            for (int i = 0; i < y; i++)
                gridOutput += (i + 1) + "   ";

            gridOutput += "\n";

            for (int i = 0; i < x; i++)
            {
                gridOutput += (i + 1) + "   ";
                for (int j = 0; j < y; j++)
                    gridOutput += grid[i][j] + "   ";

                gridOutput += "\n";
            }

            return gridOutput;
        }

        // draws grid
        public void DrawGrid() { Console.WriteLine(OutputGrid()); }

        public void ClearGrid() { InitialiseGrid(); }

        public bool MakeMove(int playerIndex, int y)
        {
            int x = (this.x - 1);

            if (CheckMove(ref x, y))
            {
                grid[x][y] = players.GetPlayerIcon(playerIndex);
                return true;
            }
                

            else return false;
        }

        // minimax alg related
        public void UndoMove(int y)
        {
            for(int i = 0; i < x; i++)
                if (grid[i][y] != gridIcon)
                {
                    grid[i][y] = gridIcon;
                    break;
                }
        }

        bool CheckMove(ref int x, int y)
        {
            if(y >= 0 && y < this.y && x >= 0 && x < this.x)
            {
                if (grid[x][y] == gridIcon)
                    return true;

                for (int i = x; i >= 0; i--)
                    if (grid[i][y] == gridIcon)
                    {
                        x = i;
                        return true;
                    }
            }
            
            return false;
        }

        bool CheckSizes(int x, ref int y)
        {
            bool bConstraintSize = false;

            if(x > 3 && x < 30) 
                bConstraintSize = true;
            
            if(y != -1)
            {
                if (y > 3 && y < 30)
                    bConstraintSize = true;
            }
                

            else y = x;

            return bConstraintSize;
        }

        bool CheckIcon(char gridIcon)
        {
            if(gridIcon == 'x' || gridIcon == 'X')
                return false;
            
            if(gridIcon == 'O' || gridIcon == 'o')
                return false;

            return true;
        }

        void InitialiseGrid()
        {
            grid = new char[x][];

            for (int i = 0; i < x; i++)
            {
                grid[i] = new char[y];

                for (int j = 0; j < y; j++)
                    grid[i][j] = gridIcon;
            }
        }
    }
}