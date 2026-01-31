using System;

public enum InputSequenceEnum
{
    Up,
    Down,
    Left,
    Right,
    Mix,
    Reset,
    Undo
}

//helper class

public static class InputSequenceEnumHelper
{
    
    public static bool IsMovementInput(this InputSequenceEnum input)
    {
        return input == InputSequenceEnum.Up ||
               input == InputSequenceEnum.Down ||
               input == InputSequenceEnum.Left ||
               input == InputSequenceEnum.Right;
    }
    public static MovementEnum ToMovementEnum(this InputSequenceEnum input)
    {
        return input switch
        {
            InputSequenceEnum.Up => MovementEnum.Up,
            InputSequenceEnum.Down => MovementEnum.Down,
            InputSequenceEnum.Left => MovementEnum.Left,
            InputSequenceEnum.Right => MovementEnum.Right,
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, null)
        };
    }
    
    public static string ToDisplayString(this InputSequenceEnum input)
    {
        return input switch
        {
            InputSequenceEnum.Up => "↑",
            InputSequenceEnum.Down => "↓",
            InputSequenceEnum.Left => "←",
            InputSequenceEnum.Right => "→",
            InputSequenceEnum.Mix => "@",
            InputSequenceEnum.Reset => "Reset",
            InputSequenceEnum.Undo => "Undo",
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, null)
        };
    }
}