namespace ChessService.Chess;

public class Board
{
    //remember that Y up/down, X left/Right, Z forward/backward
    // point 0,0,0 is in the bottom board of the cube in the left up 
    private char[] board = new char[512];// 8*8*8

    public Board()
    {
        for (int i = 0; i < 512; i++)
        {
            board[i] = 'O';
        }
        board[64] = 'H';
        board[264] = 'X';

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
        for (int i = 0; i < 512; i++)
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
        if (tmp > 0)
            fen += tmp.ToString();
        return fen;
    }


    public override String ToString()
    {
        String s = "";
        for (int y = 0; y < 8; y++)
        {
            s += "\nlayer " + (y + 1);

            for (int z = 0; z < 8; z++)
            {
                s += '\n';
                for (int x = 0; x < 8; x++)
                {
                    s += board[y * 64 + z * 8 + x];
                    s += ',';
                }
            }
        }
        return s;
    }

}
