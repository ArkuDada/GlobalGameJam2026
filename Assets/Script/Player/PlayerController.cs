using System.Collections.Generic;
using System.Linq;
using Obvious.Soap;
using Script;
using Script.GameColor;
using Script.Player;
using Script.Tile;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PlayerData _playerData;

    [SerializeField]
    private float _moveSpeed = 1f;

    [SerializeField]
    private List<MovementEnum> _inputSequence = new List<MovementEnum>();

    [SerializeField]
    private ColorRecognizer colorRecognizer;

    [SerializeField]
    private GridHelper _gridHelper;

    [SerializeField]
    private ColorDict _colorDict;

    [Header("Events")]
    [SerializeField]
    private ScriptableEventMovementResult OnMovementResult;

    [SerializeField]
    private ScriptableEventNoParam OnPlayerReachedGoal;

    [SerializeField]
    private ScriptableEventNoParam OnPlayerRequestRestart;


    public void HandleInput(InputSequenceEnum inputSequenceEnum)
    {
        if(inputSequenceEnum.IsMovementInput())
        {
            Debug.Log("Received Input: " + inputSequenceEnum);
            HandleMovement(inputSequenceEnum.ToMovementEnum());
        }

        if(inputSequenceEnum == InputSequenceEnum.Reset)
        {
            if(OnPlayerRequestRestart != null)
            {
                OnPlayerRequestRestart.Raise();
            }
        }
    }

    private void HandleMovement(MovementEnum movementEnum)
    {
        bool canMove = false;
        GameColorEnum currentColor = _playerData.playerColor;
        if(CanMoveTo(movementEnum, out Vector3 targetPos, out GameColorEnum predictedColor,
               out bool validColorSequence))
        {
            canMove = true;

            _inputSequence.Add(movementEnum);

            transform.position = targetPos;

            if(validColorSequence)
            {
                currentColor = predictedColor;
                ChangePlayerColor(currentColor);
            }
        }


        MovementResult movementResult = new MovementResult
        {
            bCanMove = canMove,
            InputSequence = movementEnum.ToInputSequenceEnum(),
            ValidNextColor = validColorSequence,
            NextColor = predictedColor
        };

        if(OnMovementResult)
        {
            OnMovementResult.Raise(movementResult);
        }

        if(canMove)
        {
            CheckCurrentGridEffect();
        }
    }

    private bool TryGetNextColor(GameColorEnum currentColor, GameColorEnum nextColor, out GameColorEnum predictedColor)
    {
        if(nextColor is not GameColorEnum.Black and not GameColorEnum.White)
        {
            if(_playerData.bMixMode)
            {
                if(_colorDict.MixColors(currentColor, nextColor, out var mixedColor))
                {
                    predictedColor = mixedColor;
                    return true;
                }
            }

            predictedColor = nextColor;
            return true;
        }

        predictedColor = GameColorEnum.Black;
        return false;
    }

    private void ChangePlayerColor(GameColorEnum nextColor)
    {
        if(nextColor is not GameColorEnum.Black and not GameColorEnum.White)
        {
            _playerData.SetColor(nextColor, _colorDict.GetColor(nextColor));
            if(_playerData.bMixMode && nextColor.IsMixedColor())
            {
                _playerData.bMixMode = false;
            }
        }
    }

    private void CheckCurrentGridEffect()
    {
        if(_gridHelper && _gridHelper.TryGetGameObjectAtPlayer(out GameObject tileGameObject))
        {
            if(tileGameObject.TryGetComponent(out TileInfo tileInfo))
            {
                if(tileInfo.TileType.HasFlag(TileTypeEnum.Goal))
                {
                    if(OnPlayerReachedGoal != null)
                    {
                        OnPlayerReachedGoal.Raise();
                    }
                }

                if(tileInfo.TileType.HasFlag(TileTypeEnum.Mazetara))
                {
                    _playerData.bMixMode = true;
                    _inputSequence.Add(MovementEnum.Null);

                    MovementResult movementResult = new MovementResult
                    {
                        bCanMove = true,
                        InputSequence = InputSequenceEnum.Mix,
                        ValidNextColor = true,
                        NextColor = _playerData.playerColor
                    };

                    if(OnMovementResult)
                    {
                        OnMovementResult.Raise(movementResult);
                    }
                }
            }
        }
    }

    private Vector3 GetTargetPosition(MovementEnum movementEnum)
    {
        return transform.position + (movementEnum.ToVector3() * _moveSpeed);
    }

    private bool CanMoveTo(MovementEnum movementEnum, out Vector3 targetPos, out GameColorEnum predictedColor,
        out bool validColorSequence)
    {
        validColorSequence = false;

        //Assume Player Same Color
        GameColorEnum currentPredictedColor = _playerData.playerColor;
        bool bCanMove = false;
        var movementEnums = _inputSequence.ToList();
        movementEnums.Add(movementEnum);

        //Predict Target Position
        targetPos = GetTargetPosition(movementEnum);

        // Predict Color From Movement
        validColorSequence = colorRecognizer.GetColor(movementEnums, out var newPredictedColor);
        if(validColorSequence)
        {
            if(TryGetNextColor(currentPredictedColor, newPredictedColor, out var nextColor))
            {
                currentPredictedColor = nextColor;
            }
        }

        if(_gridHelper)
        {
            if(!_gridHelper.TryGetGameObjectAtPosition(targetPos, out GameObject tileGameObject))
            {
                // can pass if empty tile
                bCanMove = true;
            }

            if(tileGameObject && tileGameObject.TryGetComponent(out TileInfo tileInfo))
            {
                if(tileInfo.TileType.HasFlag(TileTypeEnum.Floor))
                {
                    bCanMove = !tileInfo.TileType.HasFlag(TileTypeEnum.ColorGate);
                    if(tileInfo.TileColor == currentPredictedColor)
                    {
                        bCanMove = true;
                    }
                }
            }
        }

        predictedColor = currentPredictedColor;

        return bCanMove;
    }
}