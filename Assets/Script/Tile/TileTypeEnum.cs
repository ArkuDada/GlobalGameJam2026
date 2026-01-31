using System;

namespace Script.Tile
{
    [Flags]
    public enum TileTypeEnum
    {
        Floor = 1 << 0,
        Wall = 1 << 1,
        ColorGate = 1 << 2,
        Mazetara = 1 << 3,
        Goal = 1 << 4,  
    }
    
    //Helper
    public static class TileTypeEnumExtensions
    {
        public static bool HasFlag(this TileTypeEnum value, TileTypeEnum flag)
        {
            return (value & flag) == flag;
        }
    }
}