using System;
using Script.GameColor;
using UnityEngine;

namespace Script.Tile
{
    public class TileInfo : MonoBehaviour
    {
        [SerializeField]
        private GameColorEnum _tileColor;

        [SerializeField]
        private SpriteRenderer _sprite;

        public GameColorEnum TileColor => _tileColor;

        public TileTypeEnum TileType;

        [SerializeField]
        private ColorDict _colorDict;

        private void Awake()
        {
            if(_sprite)
            {
                _sprite.color = _colorDict.GetColor(_tileColor);
            }
        }
    }
}