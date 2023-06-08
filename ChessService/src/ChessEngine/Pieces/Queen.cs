namespace ChessService.Chess;

public class Queen : Piece
{
    public Queen(COLOR color, char[] board) : base(color, board)
    {

    }

    public override Stack<int> GetMoves(int index)
    {
        Stack<int> moves = new Stack<int>();
        this.AddMoves(moves, index, Piece.XZorthogonal, true);
        this.AddMoves(moves, index, Piece.XYorthogonal, true);
        this.AddMoves(moves, index, Piece.YZorthogonal, true);
        this.AddMoves(moves, index, Piece.XZdiagonal, true);
        this.AddMoves(moves, index, Piece.XYdiagonal, true);
        this.AddMoves(moves, index, Piece.YZdiagonal, true);
        return moves;
    }
}