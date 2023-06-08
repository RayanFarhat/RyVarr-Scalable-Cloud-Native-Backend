namespace ChessService.Chess;

public class ChessEngine
{
    private Game game;
    public ChessEngine()
    {
        this.game = new Game();
        System.Console.WriteLine(this.game.GetFEN());
    }

    public static bool IsValidSquare(int index)
    {
        return ((index & 0x80) == 0 && (index & 0x88) == 0 && index >= 0 && index <= 2048);
    }

}