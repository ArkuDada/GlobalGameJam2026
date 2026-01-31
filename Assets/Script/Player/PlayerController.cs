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

    [SerializeField]
    private ScriptableEventNoParam OnPlayerReachedGoal;


    public void HandleMovement(MovementEnum movementEnum)
    {
        if(!CanMoveTo(movementEnum, out Vector3 targetPos, out GameColorEnum predictedColor, out bool validColorSequence))
        {
            //Hit Wall
            return;
        }

        _inputSequence.Add(movementEnum);

        Debug.Log("Moving: " + movementEnum);

        transform.position = targetPos;

        if(validColorSequence)
            ChangePlayerColor(predictedColor);


        CheckCurrentGridEffect();
    }

    private void ChangePlayerColor(GameColorEnum predictedColor)
    {
        if(predictedColor is not GameColorEnum.Black and not GameColorEnum.White)
        {
            if(_playerData.bMixMode)
            {
                if(_colorDict.MixColors(_playerData.playerColor, predictedColor, out var mixedColor))
                {
                    _playerData.SetColor(mixedColor, _colorDict.GetColor(mixedColor));
                }
            }
            else
            {
                _playerData.SetColor(predictedColor, _colorDict.GetColor(predictedColor));
            }

            _playerData.bMixMode = false;
        }

        Debug.Log($"Is Mix Mode {_playerData.bMixMode}");
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
                        Debug.Log("Reached Goal!");

                        OnPlayerReachedGoal.Raise();
                    }
                }

                if(tileInfo.TileType.HasFlag(TileTypeEnum.Mazetara))
                {
                    _playerData.bMixMode = true;
                    _inputSequence.Add(MovementEnum.Null);
                }
            }
        }
    }

    private Vector3 GetTargetPosition(MovementEnum movementEnum)
    {
        return transform.position + (movementEnum.ToVector3() * _moveSpeed);
    }

    private bool CanMoveTo(MovementEnum movementEnum, out Vector3 targetPos, out GameColorEnum predictedColor, out bool validColorSequence)
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
            currentPredictedColor = newPredictedColor;
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

        Debug.Log($"Can Move: {bCanMove}, To Position: {targetPos}, Predicted Color: {currentPredictedColor}");

        predictedColor = currentPredictedColor;

        return bCanMove;
    }
}