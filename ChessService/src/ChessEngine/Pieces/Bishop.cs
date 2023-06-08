namespace ChessService.Chess;

public class Bishop : Piece
{
    public Bishop(COLOR color, char[] board) : base(color, board)
    {

    }

    public override Stack<int> GetMoves(int index)
    {
        Stack<int> moves = new Stack<int>();
        this.AddMoves(moves, index, Piece.XZdiagonal, true);
        this.AddMoves(moves, index, Piece.XYdiagonal, true);
        this.AddMoves(moves, index, Piece.YZdiagonal, true);
        return moves;
    }
}