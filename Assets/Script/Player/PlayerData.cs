using System;
using UnityEngine;

namespace Script.Player
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer spriteRenderer;

        public GameColorEnum playerColor = GameColorEnum.White;

        public bool bMixMode = false;

        private void Start()
        {
            if(spriteRenderer)
                spriteRenderer.color = Color.white;
        }

        public void SetColor(GameColorEnum newColor, Color color)
        {
            playerColor = newColor;
            if(spriteRenderer)
                spriteRenderer.color = color;
        }
    }
}