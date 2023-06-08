namespace ChessService.Chess;

public class Knight : Piece
{
    public Knight(COLOR color, char[] board) : base(color, board)
    {

    }

    public override Stack<int> GetMoves(int index)
    {
        Stack<int> moves = new Stack<int>();
        this.AddMoves(moves, index, Piece.XZknight, false);
        this.AddMoves(moves, index, Piece.XYknight, false);
        this.AddMoves(moves, index, Piece.YZknight, false);
        return moves;
    }
}