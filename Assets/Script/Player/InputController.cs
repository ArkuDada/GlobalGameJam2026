using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private InputSystem_Actions _inputActions;

    [SerializeField]
    private ScriptableEventInputSequenceEnum onInputScriptableEvent;

    private void Awake()
    {
        Debug.Log("InputController Awake");
        _inputActions = new InputSystem_Actions();
        _inputActions.Enable();
    }

    private void OnEnable()
    {
        _inputActions.Player.Up.performed += MoveUp;
        _inputActions.Player.Down.performed += MoveDown;
        _inputActions.Player.Left.performed += MoveLeft;
        _inputActions.Player.Right.performed += MoveRight;
        _inputActions.Player.Undo.performed += RedoInput;
        _inputActions.Player.Restart.performed += RestartInput;
    }
    private void OnDisable()
    {
        _inputActions.Player.Up.performed -= MoveUp;
        _inputActions.Player.Down.performed -= MoveDown;
        _inputActions.Player.Left.performed -= MoveLeft;
        _inputActions.Player.Right.performed -= MoveRight;
    }

    private void RestartInput(InputAction.CallbackContext obj)
    {
        InvokeScriptableEvent(InputSequenceEnum.Reset);
    }

    private void RedoInput(InputAction.CallbackContext obj)
    {
        InvokeScriptableEvent(InputSequenceEnum.Undo);
    }

    private void MoveRight(InputAction.CallbackContext callbackContext)
    {
        InvokeScriptableEvent(InputSequenceEnum.Right);
    }

    private void MoveLeft(InputAction.CallbackContext callbackContext)
    {
        InvokeScriptableEvent(InputSequenceEnum.Left);
    }

    private void MoveDown(InputAction.CallbackContext callbackContext)
    {
        InvokeScriptableEvent(InputSequenceEnum.Down);
    }

    private void MoveUp(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Move Up Invoked");
        InvokeScriptableEvent(InputSequenceEnum.Up);
    }

    private void InvokeScriptableEvent(InputSequenceEnum movement)
    {
        onInputScriptableEvent.Raise(movement);
    }
}