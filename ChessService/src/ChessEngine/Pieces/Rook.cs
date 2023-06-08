namespace ChessService.Chess;

public class Rook : Piece
{
    public Rook(COLOR color, char[] board) : base(color, board)
    {

    }

    public override Stack<int> GetMoves(int index)
    {
        Stack<int> moves = new Stack<int>();
        this.AddMoves(moves, index, Piece.XZorthogonal, true);
        this.AddMoves(moves, index, Piece.XYorthogonal, true);
        this.AddMoves(moves, index, Piece.YZorthogonal, true);
        return moves;
    }
}