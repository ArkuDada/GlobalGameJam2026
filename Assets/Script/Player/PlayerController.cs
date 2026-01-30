using System.Collections.Generic;
using System.Linq;
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

    public void HandleMovement(MovementEnum movementEnum)
    {
        if(!CanMoveTo(movementEnum, out Vector3 targetPos, out GameColorEnum predictedColor))
        {
            //Hit Wall
            return;
        }

        _inputSequence.Add(movementEnum);

        Debug.Log("Moving: " + movementEnum);

        transform.position = targetPos;

        if(predictedColor is not GameColorEnum.Black and not GameColorEnum.White)
            _playerData.SetColor(predictedColor, _colorDict.GetColor(predictedColor));


        if(_gridHelper && _gridHelper.TryGetGameObjectAtPlayer(out GameObject tileGameObject))
        {
            if(tileGameObject.TryGetComponent(out TileInfo tileInfo))
            {
                Debug.Log($"Step on Tile Color: {tileInfo.TileColor}, Player Color: {_playerData.playerColor}");
            }
        }
    }

    private Vector3 GetTargetPosition(MovementEnum movementEnum)
    {
        return transform.position + (movementEnum.ToVector3() * _moveSpeed);
    }

    private bool CanMoveTo(MovementEnum movementEnum, out Vector3 targetPos, out GameColorEnum predictedColor)
    {
        //temp move seq
        var movementEnums = _inputSequence.ToList();
        movementEnums.Add(movementEnum);

        targetPos = GetTargetPosition(movementEnum);

        if(!colorRecognizer.GetColor(movementEnums, out predictedColor))
        {
            predictedColor = GameColorEnum.Black;
        }

        if(predictedColor == GameColorEnum.Black)
        {
            predictedColor = _playerData.playerColor;
        }

        if(_gridHelper)
        {
            if(!_gridHelper.TryGetGameObjectAtPosition(targetPos, out GameObject tileGameObject))
            {
                // can pass if empty tile
                return true;
            }

            if(tileGameObject.TryGetComponent(out TileInfo tileInfo))
            {
                if(tileInfo.TileColor == predictedColor)
                {
                    return true;
                }
            }
        }

        return false;
    }
}