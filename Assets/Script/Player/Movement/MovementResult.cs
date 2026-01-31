using System;
using Script;

[Serializable]
public struct MovementResult
{
    public bool bCanMove;
    public InputSequenceEnum InputSequence;
    public bool ValidNextColor;
    public GameColorEnum NextColor;
}
