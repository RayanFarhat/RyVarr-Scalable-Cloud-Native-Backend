namespace ChessService.Chess;
public enum COLOR
{
    WHIRE, BLACK
}
public abstract class Piece
{
    private COLOR color;
    private char[] board;

    // declare the moves (declared once and used accross all the program)
    public static readonly int[] XZorthogonal = { 1, -1, 16, -16 };
    public static readonly int[] XZdiagonal = { 17, 15, -15, -17 };
    public static readonly int[] XZknight = { 18, 14, -18, -14, 33, 31, -33, -31 };

    public static readonly int[] XYorthogonal = { 1, -1, 256, -256 };
    public static readonly int[] XYdiagonal = { 257, 255, -257, -255 };
    public static readonly int[] XYknight = { 258, 254, -258, -254, 511, 513, -511, -513 };

    public static readonly int[] YZorthogonal = { 16, -16, 256, -256 };
    public static readonly int[] YZdiagonal = { 272, 240, -272, -240 };
    public static readonly int[] YZknight = { 224, 288, -224, -288, 528, 496, -528, -496 };


    public Piece(COLOR color, char[] board)
    {
        this.color = color;
        this.board = board;
    }
    public bool IsEmptySquare(int index)
    {
        return this.board[index] == 'O';
    }
    public bool IsEnemy(int index)
    {
        if (this.color == COLOR.WHIRE)
        {
            return char.IsLower(this.board[index]);
        }
        else
        {
            return char.IsUpper(this.board[index]);
        }
    }

    //* not applied to pawn
    public void AddMoves(Stack<int> moves, int index, int[] movespattern, bool infinite)
    {
        int i;
        int nextmove;
        //queen,castle,bishop
        if (infinite)
        {
            for (i = 0; i < movespattern.Length; i++)
            {
                nextmove = index + movespattern[i];
                while (ChessEngine.IsValidSquare(nextmove) && this.IsEmptySquare(nextmove))
                {
                    moves.Push(nextmove);
                    nextmove = nextmove + movespattern[i];
                }
                if (this.IsEnemy(nextmove))
                {
                    moves.Push(nextmove);
                }
            }
        }
        // king, knight
        else
        {
            for (i = 0; i < movespattern.Length; i++)
            {
                nextmove = index + movespattern[i];
                if (ChessEngine.IsValidSquare(nextmove))
                {
                    if (!this.IsEmptySquare(nextmove))
                    {
                        if (this.IsEnemy(nextmove))
                        {
                            moves.Push(nextmove);
                        }
                    }
                    else
                    {
                        moves.Push(nextmove);
                    }
                }
            }
        }
    }
    public abstract Stack<int> GetMoves(int index);
}