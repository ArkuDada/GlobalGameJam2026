using System;
using UnityEngine;

namespace Script.Player
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer spriteRenderer;

        [SerializeField]
        Material playerMaterial;

        public GameColorEnum playerColor = GameColorEnum.White;

        public bool bMixMode = false;

        private void Start()
        {
            SetColor(GameColorEnum.White, Color.white);
        }

        public void SetColor(GameColorEnum newColor, Color color)
        {
            playerColor = newColor;
            if(spriteRenderer)
                spriteRenderer.color = color;

            if(playerMaterial)
            {
                //set property base color
                playerMaterial.SetColor("_BaseColor", color);
            }
        }
    }
}