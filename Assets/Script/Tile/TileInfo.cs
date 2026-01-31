using UnityEngine;

namespace Script.Tile
{
    public class TileInfo : MonoBehaviour
    {
        [SerializeField]
        private GameColorEnum _tileColor;
        
        public GameColorEnum TileColor => _tileColor;

        public TileTypeEnum TileType;
    }
}