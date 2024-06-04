//using UnityEditor.PackageManager;

public enum Move
{
    None,
    Up,
    Down,
    Left,
    Right
}

class Utils
{
    public static Move oppositeMove(Move move)
    {
        if(move == Move.Up)
        {
            return Move.Down;
        }
        else if(move == Move.Down)
        {
            return Move.Up;
        }
        else if(move == Move.Right)
        {
            return Move.Left;
        }
        else if(move == Move.Left)
        {
            return Move.Right;
        }
        else
        {
            return Move.None;
        }
    }
}

// public enum TileType
// {
//     Start,
//     Finish,
//     Rock,
//     Powerup
// }