namespace ChessService.Chess;

public class King : Piece
{
    public King(COLOR color, char[] board) : base(color, board)
    {

    }

    public override Stack<int> GetMoves(int index)
    {
        Stack<int> moves = new Stack<int>();
        this.AddMoves(moves, index, Piece.XZorthogonal, false);
        this.AddMoves(moves, index, Piece.XZdiagonal, false);
        this.AddMoves(moves, index, Piece.XYorthogonal, false);
        this.AddMoves(moves, index, Piece.XYdiagonal, false);
        this.AddMoves(moves, index, Piece.YZorthogonal, false);
        this.AddMoves(moves, index, Piece.YZdiagonal, false);
        return moves;
    }
}