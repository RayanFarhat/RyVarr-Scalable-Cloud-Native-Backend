namespace ChessService.Chess;

public class Board
{
    //remember that Y up/down, X left/Right, Z forward/backward
    // point 0,0,0 is in the bottom board of the cube in the left up 
    /*
    Rathar than have 8*8*8=512 blocks I use 0X88 so it be 1024 blocks
    I also use somthing like 0X88 but also between every layer so it will be 2048 blocks
    The trick is to replace 0X88 with 0x80 so we can make the first 128 blocks valid and the next are nor and keep going...
    * when block index is & 0x80 bigger than 0 or block index is & 0x88 bigger than 0 then the block is not valid!

    0 1 2 3 4 5 6 7 |8 9 10 11 12 13 14 15  <- the right is invalid
    16 ...          |
                        ... 124 125 126 127
    128 .. 255                    <- is invalid
    */
    private char[] board = new char[2048];// (8*8*8)*2*2

    public Board()
    {
        for (int i = 0; i < 2048; i++)
        {
            board[i] = 'O';
        }
        board[17] = 'l';
        board[3] = 'v';
    }

    public Stack<int> GetMoves(int index)
    {
        Stack<int> moves = new Stack<int>();
        return moves;
    }

    //Upper case are whites
    public string GetFEN()
    {
        string fen = "";
        int tmp = 0;
        for (int i = 0; i < 2048; i++)
        {
            // block is valid
            if ((i & 0x80) == 0 && (i & 0x88) == 0)
            {
                if (board[i] == 'O')
                {
                    tmp += 1;
                }
                else
                {
                    fen += tmp.ToString();
                    tmp = 0;
                    fen += board[i];
                }
            }
        }
        if (tmp > 0)
            fen += tmp.ToString();
        return fen;
    }


    public override String ToString()
    {
        String s = "";
        int l = 1;
        for (int i = 0; i < 2048; i++)
        {
            // block is valid
            if ((i & 0x80) == 0 && (i & 0x88) == 0)
            {
                if ((i % 8) == 0)
                {
                    s += '\n';
                }
                if ((i % 128) == 0)
                {
                    s += "Layer " + l.ToString();
                    l++;
                    s += '\n';
                }
                s += board[i];
                s += ',';
            }
        }
        return s;
    }

}
