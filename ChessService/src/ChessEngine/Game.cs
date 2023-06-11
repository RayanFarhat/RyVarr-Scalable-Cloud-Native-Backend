namespace ChessService.Chess;

public class Game
{
    public bool WhiteWon;
    public bool BlackWon;

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
    private char[] board;
    private King K;
    private King k;
    private Queen Q;
    private Queen q;
    private Rook R;
    private Rook r;
    private Knight N;
    private Knight n;
    private Bishop B;
    private Bishop b;

    private Pawn P;
    private Pawn p;



    public Game()
    {

        this.WhiteWon = false;
        this.BlackWon = false;

        this.board = new char[2048];// (8*8*8)*2*2
        for (int i = 0; i < 2048; i++)
        {
            board[i] = 'O';
        }
        // * the game init
        board[512] = 'r';
        board[513] = 'n';
        board[514] = 'b';
        board[515] = 'q';
        board[516] = 'k';
        board[517] = 'b';
        board[518] = 'n';
        board[519] = 'r';
        board[528] = 'p';
        board[529] = 'p';
        board[530] = 'p';
        board[531] = 'p';
        board[532] = 'p';
        board[533] = 'p';
        board[534] = 'p';
        board[535] = 'p';

        board[880] = 'R';
        board[881] = 'N';
        board[882] = 'B';
        board[883] = 'Q';
        board[884] = 'K';
        board[885] = 'B';
        board[886] = 'N';
        board[887] = 'R';
        board[864] = 'P';
        board[865] = 'P';
        board[866] = 'P';
        board[867] = 'P';
        board[868] = 'P';
        board[869] = 'P';
        board[870] = 'P';
        board[871] = 'P';
        //******************************************************

        this.K = new King(COLOR.WHIRE, this.board);
        this.k = new King(COLOR.BLACK, this.board);
        this.Q = new Queen(COLOR.WHIRE, this.board);
        this.q = new Queen(COLOR.BLACK, this.board);
        this.R = new Rook(COLOR.WHIRE, this.board);
        this.r = new Rook(COLOR.BLACK, this.board);
        this.N = new Knight(COLOR.WHIRE, this.board);
        this.n = new Knight(COLOR.BLACK, this.board);
        this.B = new Bishop(COLOR.WHIRE, this.board);
        this.b = new Bishop(COLOR.BLACK, this.board);
        this.P = new Pawn(COLOR.WHIRE, this.board);
        this.p = new Pawn(COLOR.BLACK, this.board);

        //!test
        Stack<int> moves = this.GetMoves(33);

        while (moves.Count > 0)
        {
            int move = moves.Pop();
            this.board[move] = 'X';
        }
    }

    public Stack<int> GetMoves(int index)
    {
        switch (board[index])
        {
            case 'K':
                return this.K.GetMoves(index);
            case 'k':
                return this.k.GetMoves(index);
            case 'Q':
                return this.Q.GetMoves(index);
            case 'q':
                return this.q.GetMoves(index);
            case 'R':
                return this.R.GetMoves(index);
            case 'r':
                return this.r.GetMoves(index);
            case 'N':
                return this.N.GetMoves(index);
            case 'n':
                return this.n.GetMoves(index);
            case 'B':
                return this.B.GetMoves(index);
            case 'b':
                return this.b.GetMoves(index);
            case 'P':
                return this.P.GetMoves(index);
            case 'p':
                return this.p.GetMoves(index);
            default:
                return new Stack<int>();
        }
    }

    public bool Move(int from, int to)
    {
        Stack<int> moves = GetMoves(from);
        int move;
        while (moves.Count > 0)
        {
            move = moves.Pop();
            if (to == move)
            {
                // if the eaten piece is the king
                if (this.board[to] == 'k')
                {
                    this.WhiteWon = true;
                }
                else if (this.board[to] == 'K')
                {
                    this.BlackWon = true;
                }
                // update the board
                this.board[to] = this.board[from];
                this.board[from] = 'O';
                return true;
            }
        }
        return false;
    }

    //Upper case are whites
    public string GetFEN()
    {
        string fen = "";
        int tmp = 0;
        for (int i = 0; i < 2048; i++)
        {
            // block is valid
            if (ChessEngine.IsValidSquare(i))
            {
                if (board[i] == 'O')
                {
                    tmp += 1;
                }
                else
                {
                    if (tmp != 0)
                    {
                        fen += tmp.ToString();
                        tmp = 0;
                    }

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
            if (ChessEngine.IsValidSquare(i))
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
                // s += i.ToString();
                s += ',';
            }
        }
        return s;
    }

}
