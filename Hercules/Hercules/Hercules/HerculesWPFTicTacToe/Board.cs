using System;

public enum GameState
{
    InProgress,
    ComputerWins,
    HumanWins,
    Draw
}


class  Move
{
    public int iCol;
    public int iRow;
    public int iRank;
    public Move(int col, int row)
    {
        iCol = col;
        iRow = row;
        iRank = 0;
    }
}

class Board
{

    public static readonly int Empty =  0;
    public static readonly int X     = -1;
    public static readonly int O     =  1;
    
    //represents the state of the game
    public GameState BoardState;

    //Size of board in one dimension
    public int iBoardSize;

    //Count of unplayed squares used to check for draw condition
    public int iEmptySquares;
    
    //The board
    public int[,] aiBoard;
    
    //
    //Create a new Board object from a size parameter
    //
    public Board(int iSize)
    {
        this.iBoardSize    = iSize;
        this.iEmptySquares = iSize * iSize;
        this.aiBoard       = new int[iSize, iSize];
        this.BoardState    = GameState.InProgress;
    }
    
    //
    //Create a new Board object by copying an existing one
    //
    public Board(Board board)
    {
        this.iEmptySquares = board.iEmptySquares;
        this.iBoardSize    = board.iBoardSize;
        this.BoardState    = board.BoardState;
        this.aiBoard       = new int[iBoardSize, iBoardSize];
        
        //Copy aiBoard
        int i, j;
        for (i = 0; i < this.iBoardSize; i++)
        {
            for (j = 0; j < this.iBoardSize; j++)
            {
                this.aiBoard[i, j] = board.aiBoard[i, j];
            }
        }
    }

    
    //
    //  Apply a move 
    //
    public void MakeMove(int CurrentPlayer, Move move)
    {
            aiBoard[move.iCol, move.iRow] = CurrentPlayer;
         
            //Check for draw condition
            iEmptySquares--;
            if (iEmptySquares == 0)
                this.BoardState = GameState.Draw;
    }
    
    //
    //  Checks the board for a winner and reassigns
    //  this.BoardState as appropriate.
    //
    public int[] CheckBoard()
    {
        int i, j, iTotal;
        
        //Check the rows
        for (i = 0; i < iBoardSize; i++)
        {
            iTotal = 0;
            for(j = 0; j< iBoardSize; j++)
            {
                iTotal += aiBoard[i, j];
            }
            if (iTotal == -iBoardSize)
            {
                this.BoardState = GameState.ComputerWins;
                return new int[] {0,i};
            }
            if (iTotal == iBoardSize)
            {
                this.BoardState = GameState.HumanWins;
                return new int[] { 0, i };
            }
            
        }
        //Check the columns
        for (j = 0; j < iBoardSize; j++)
        {
            iTotal = 0;

            for (i = 0; i < iBoardSize; i++)
            {
                iTotal += aiBoard[i, j];
            }
            if (iTotal == -iBoardSize)
            {
                this.BoardState = GameState.ComputerWins;
                return new int[] { 1, j };
            }
            if (iTotal == iBoardSize)
            {
                this.BoardState = GameState.HumanWins;
                return new int[] { 1, j };
            }
        }
        //Check Top-Left to Bottom-Right diagonal
        iTotal = 0;
        for (i = 0; i < iBoardSize; i++)
        {
            iTotal += aiBoard[i, i];
        }
        if (iTotal == -iBoardSize)
        {
            this.BoardState = GameState.ComputerWins;
            return new int[] { 2, 0 };
        }
        if (iTotal == iBoardSize)
        {
            this.BoardState = GameState.HumanWins;
            return new int[] { 2, 0 };
        }
        //Check Top-Right to Bottom-Left diagonal
        iTotal = 0;
        for (i = iBoardSize - 1, j = 0; i >= 0 && j<iBoardSize; i--, j++)
        {
             iTotal += aiBoard[i, j];
            
        }
        if (iTotal == -iBoardSize)
        {
            this.BoardState = GameState.ComputerWins;
            return new int[] { 2, 1 };
        }
        if (iTotal == iBoardSize)
        {
            this.BoardState = GameState.HumanWins;
            return new int[] { 2, 1 };
        }

        return null;
    }
    
}
