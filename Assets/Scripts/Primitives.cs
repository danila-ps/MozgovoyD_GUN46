using System;

namespace Netologia.Gameplay
{
    [Flags]
    public enum NeighbourType
    {
        None = 0,
        Left = 1,
        Right = 2,
        Top = 4,
        Bottom = 8,
        LeftRight = Left | Right,
        TopBottom = Top | Bottom,
        LeftTop = Left | Top,
        LeftBottom = Left | Bottom,
        RightTop = Right | Top,
        RightBottom = Right | Bottom,
    }

    public enum Team
    {
        Player1,
        Player2,
    }
}
