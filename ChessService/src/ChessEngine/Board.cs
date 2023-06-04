namespace ChessService.Chess;

public class Board
{
    //remember that Y up/down, X left/Right, Z forward/backward
    private char?[,,] board = new char?[8, 8, 8];

    public Board()
    {
        // init
        for (int x = 0; x < board.GetLength(0); x++)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                for (int z = 0; z < board.GetLength(2); z++)
                {
                    board[x, y, z] = null;
                }
            }
        }

    }
    public override String ToString()
    {
        String s = "";
        for (int x = 0; x < board.GetLength(0); x++)
        {
            s += "\nlayer " + (x + 1);
            for (int y = 0; y < board.GetLength(1); y++)
            {
                s += '\n';
                for (int z = 0; z < board.GetLength(2); z++)
                {
                    s += board[x, y, z];
                    s += ',';
                }
            }
        }
        return s;
    }

}
