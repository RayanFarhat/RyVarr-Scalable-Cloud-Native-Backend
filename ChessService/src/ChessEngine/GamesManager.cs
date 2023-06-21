namespace ChessService.Chess;
using System.Collections.Generic;


public class GamesManager
{
    private ChessEngine game;
    private Dictionary<int, ChessEngine> map;
    private Queue<int> openRooms;

    public GamesManager()
    {
        this.map = new Dictionary<int, ChessEngine>();
        this.openRooms = new Queue<int>();

        this.game = new ChessEngine();
        System.Console.WriteLine(this.game.GetFEN());
    }

    public void JoinRoom()
    {
        if (this.openRooms.Count > 0)
        {
            int roomid = this.openRooms.Dequeue();
            
        }
    }


    public override string ToString()
    {
        return "Chess Games Manager Started!";
    }

}