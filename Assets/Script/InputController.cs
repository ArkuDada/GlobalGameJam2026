using System;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    private InputSystem_Actions _inputActions;

    public UnityEvent<MovementEnum> onMoveEvent;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.Player.Up.performed += ctx => MoveUp();
        _inputActions.Player.Down.performed += ctx => MoveDown();
        _inputActions.Player.Left.performed += ctx => MoveLeft();
        _inputActions.Player.Right.performed += ctx => MoveRight();
    }

    private void MoveRight()
    {
        Move(InputSequenceEnum.Right);
    }

    private void MoveLeft()
    {
        Move(InputSequenceEnum.Left);
    }

    private void MoveDown()
    {
        Move(InputSequenceEnum.Down);
    }

    private void MoveUp()
    {
        Move(InputSequenceEnum.Up);
    }

    private void Move(InputSequenceEnum movement)
    {
        onMoveEvent?.Invoke(movement.ToMovementEnum());
    }
}