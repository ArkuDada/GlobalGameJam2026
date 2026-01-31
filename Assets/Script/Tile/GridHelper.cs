using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Script.Tile
{
    public class GridHelper : MonoBehaviour
    {
        public Tilemap targetTilemap;
        public Grid gridLayout;
        public Transform playerTransform;

        private void Awake()
        {
            if(targetTilemap && targetTilemap.TryGetComponent(out TilemapRenderer renderer))
            {
                renderer.enabled = false;
            }
        }

        private Vector3Int GetPlayerCellPosition()
        {
            Vector3 playerWorldPosition = playerTransform.position;
            Vector3Int cellPosition = gridLayout.WorldToCell(playerWorldPosition);
            return cellPosition;
        }

        public bool TryGetGameObjectAtPosition(Vector3 pos, out GameObject tileGameObject)
        {
            Vector3Int cellPosition = gridLayout.WorldToCell(pos);
            tileGameObject = targetTilemap.GetInstantiatedObject(cellPosition);

            // Debug.Log($"Cell Position: {cellPosition}, Tile GameObject: {tileGameObject}");
            return tileGameObject != null;
        }

        public bool TryGetGameObjectAtPlayer(out GameObject tileGameObject)
        {
            Vector3Int cellPosition = GetPlayerCellPosition();
            tileGameObject = targetTilemap.GetInstantiatedObject(cellPosition);

            // Debug.Log($"Cell Position: {cellPosition}, Tile GameObject: {tileGameObject}");
            return tileGameObject != null;
        }
    }
}