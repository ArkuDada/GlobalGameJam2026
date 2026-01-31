using System;
using UnityEngine;

public enum MovementEnum
{
    Null = -1,
    Up = 0,
    Down,
    Left,
    Right,
}

//Helper func

public static class MovementEnumHelper
{
    public static InputSequenceEnum ToInputSequenceEnum(this MovementEnum movement)
    {
        return movement switch
        {
            MovementEnum.Up => InputSequenceEnum.Up,
            MovementEnum.Down => InputSequenceEnum.Down,
            MovementEnum.Left => InputSequenceEnum.Left,
            MovementEnum.Right => InputSequenceEnum.Right,
            _ => throw new ArgumentOutOfRangeException(nameof(movement), movement, null)
        };
    }

    public static Vector2 ToVector2(this MovementEnum movementEnum)
    {
        switch(movementEnum)
        {
            case MovementEnum.Up:
                return Vector2.up;
            case MovementEnum.Down:
                return Vector2.down;
            case MovementEnum.Left:
                return Vector2.left;
            case MovementEnum.Right:
                return Vector2.right;
        }

        return Vector2.zero;
    }

    public static Vector3 ToVector3(this MovementEnum movementEnum)
    {
        switch(movementEnum)
        {
            case MovementEnum.Up:
                return Vector3.up;
            case MovementEnum.Down:
                return Vector3.down;
            case MovementEnum.Left:
                return Vector3.left;
            case MovementEnum.Right:
                return Vector3.right;
        }

        return Vector3.zero;
    }
}