namespace ChessService;

public class ChessEngine
{
    private int[,,] board = new int[8, 8, 8];
    public ChessEngine()
    {

    }

    public override String ToString()
    {
        String s = "";
        for (int i = 0; i < board.GetLength(0); i++)
        {
            s += '\n';
            for (int j = 0; j < board.GetLength(1); j++)
            {
                s += ',';
                for (int k = 0; k < board.GetLength(2); k++)
                {
                    s += board[i, j, k];
                }
            }
        }
        return s;
    }

}