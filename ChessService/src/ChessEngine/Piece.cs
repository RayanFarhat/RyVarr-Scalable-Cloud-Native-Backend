namespace ChessService.Chess;

public abstract class Piece
{
    // declare the moves (declared once and used accross all the program)
    public static readonly int[] XZdiagonal = { 2 };
    public static readonly int[] XZorthogonal = { 2 };
    public static readonly int[] XZknight = { 2 };

    public static readonly int[] XYdiagonal = { 2 };
    public static readonly int[] XYorthogonal = { 2 };
    public static readonly int[] XYknight = { 2 };

    public static readonly int[] YZdiagonal = { 2 };
    public static readonly int[] YZorthogonal = { 2 };
    public static readonly int[] YZknight = { 2 };


    public Piece()
    {
    }
    public abstract Stack<int> GetMoves();
}