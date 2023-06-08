namespace ChessService.Chess;

public class Pawn : Piece
{
    public Pawn(COLOR color, char[] board) : base(color, board)
    {

    }

    public override Stack<int> GetMoves(int index)
    {
        int i;
        int nextmove;
        Stack<int> moves = new Stack<int>();

        // * where can move
        for (i = 0; i < 4; i++)
        {
            nextmove = index + XZorthogonal[i];
            if (ChessEngine.IsValidSquare(nextmove))
            {
                if (this.IsEmptySquare(nextmove))
                {
                    moves.Push(nextmove);
                }

            }
        }
        for (i = 0; i < 4; i++)
        {
            nextmove = index + XYorthogonal[i];
            if (ChessEngine.IsValidSquare(nextmove))
            {
                if (this.IsEmptySquare(nextmove))
                {
                    moves.Push(nextmove);
                }
            }
        }
        for (i = 0; i < 4; i++)
        {
            nextmove = index + YZorthogonal[i];
            if (ChessEngine.IsValidSquare(nextmove))
            {
                if (this.IsEmptySquare(nextmove))
                {
                    moves.Push(nextmove);
                }
            }
        }

        // * where can eat
        for (i = 0; i < 4; i++)
        {
            nextmove = index + XZdiagonal[i];
            if (ChessEngine.IsValidSquare(nextmove))
            {
                if (!this.IsEmptySquare(nextmove) && this.IsEnemy(nextmove))
                {
                    moves.Push(nextmove);
                }
            }
        }
        for (i = 0; i < 4; i++)
        {
            nextmove = index + XYdiagonal[i];
            if (ChessEngine.IsValidSquare(nextmove))
            {
                if (!this.IsEmptySquare(nextmove) && this.IsEnemy(nextmove))
                {
                    moves.Push(nextmove);
                }
            }
        }
        for (i = 0; i < 4; i++)
        {
            nextmove = index + YZdiagonal[i];
            if (ChessEngine.IsValidSquare(nextmove))
            {
                if (!this.IsEmptySquare(nextmove) && this.IsEnemy(nextmove))
                {
                    moves.Push(nextmove);
                }
            }
        }
        return moves;
    }
}