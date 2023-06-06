namespace ChessService.Chess;

public class king : Piece
{
    public king()
    {

    }

    public abstract Stack<int> GetMoves()
    {
        Stack<int> moves = new Stack<int>();
        return moves;
    }
}