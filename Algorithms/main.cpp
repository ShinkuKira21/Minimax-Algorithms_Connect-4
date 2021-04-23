#include "../../Library/Functions.h"

struct Move { int row, col; };

char player = 'x', opponent = 'o';

bool IsMovesLeft(char board[3][3])
{
    for(int i = 0; i < 3; i++)
        for(int j = 0; j < 3; j++)
            if(board[i][j] == '_')
                return true;
    return false;
}

int Evaluate(char b[3][3])
{
    for (int i = 0; i < 3; i++)
        if(b[i][0] == b[i][1] && b[i][1] == b[i][2])
        {
            if(b[i][0] == player)
                return +10;
            
            if(b[i][0] == opponent)
                return -10;
        }

    for(int i = 0; i < 3; i++)
        if(b[0][i] == b[1][i] && b[1][i] == b[2][i])
        {
            if(b[0][i] == player)
                return +10;

            if(b[0][i] == opponent)
                return -10;
        }

    if(b[0][0] == b[1][1] && b[1][1] == b[2][2])
    {
        if(b[0][0] == player)
            return +10;
        
        if(b[0][0] == player)
            return -10;
    }

    return 0;
}

int Minimax(char board[3][3], int depth, bool bMax)
{
    int score = Evaluate(board);

    if(score == 10)
        return score;

    if(score == -10)
        return score;

    if(IsMovesLeft(board) == false)
        return 0;

    if(bMax)
    {
        int best = -1000;

        for(int i = 0; i < 3; i++)
            for(int j = 0; j < 3; j++)
                if(board[i][j] == '_')
                {
                    board[i][j] = player;

                    best = std::max(best, Minimax(board, depth+1, !bMax));

                    board[i][j] = '_';
                }
        
        return best;
    }
    else
    {
        int best = 1000;

        for(int i = 0; i < 3; i++)
            for(int j = 0; j < 3; j++)
                if(board[i][j] == '_')
                {
                    board[i][j] = player;

                    best = std::min(best, Minimax(board, depth+1, !bMax));

                    board[i][j] = '_';
                }

        return best;
    }
}

Move FindBestMove(char board[3][3])
{
    int bestVal = -1000;

    Move bestMove;
    bestMove.row = -1;
    bestMove.col = -1;

    for(int i = 0; i < 3; i++)
            for(int j = 0; j < 3; j++)
                if(board[i][j] == '_')
                {
                    board[i][j] = player;
                    int moveVal = Minimax(board, 0, false);

                    board[i][j] = '_';

                    if(moveVal > bestVal)
                    {
                        bestMove.row = i;
                        bestMove.col = j;
                        bestVal = moveVal;
                    }
                }
    
    printf("The value of the best Move is: %d\n\n", bestVal);

    return bestMove;
}

int main(int argc, char** argv)
{
    char board[3][3] =
    {
        { 'x', 'o', 'x' },
        { 'x', 'o', '_' },
        { '_', 'o', '_' }
    };

    Move bestMove = FindBestMove(board);
    
    printf("The Optimal Move is:\n");
    printf("ROW: %d COL: %d\n\n", bestMove.row, bestMove.col);

    return 0;
}