namespace ChessService.Chess;

public enum COLOR
{
    WHITE,
    BLACK
}

public enum XZ_MOVE
{
    right = +1,
    left = -1,
    down = 8,
    up = -8,
    down_left = 7,
    down_right = 9,
    up_left = -9,
    up_right = -7,
}

public enum XZ_KNIGHT
{
    m1 = 10,
    m2 = 6,
    m3 = 17,
    m4 = 15,
    m5 = -10,
    m6 = -6,
    m7 = -17,
    m8 = -15,
}
public enum XY_MOVE
{
    right = +1,
    left = -1,
    down = 64,
    up = -64,
    down_left = -65,
    down_right = -63,
    up_left = 63,
    up_right = 65,
}
public abstract class Piece
{
    public Piece()
    {
    }
    public abstract void GetMoves();
}