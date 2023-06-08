namespace ChessService.Chess;

public class ChessEngine
{
    private Board board;
    public ChessEngine()
    {
        this.board = new Board();
        System.Console.WriteLine(Piece.XZ[0]);
    }
}