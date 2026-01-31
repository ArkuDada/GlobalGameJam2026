using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Player
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField]
        MeshRenderer _meshRenderer;

        [SerializeField]
        private Material playerMaterialBase;


        public GameColorEnum playerColor = GameColorEnum.White;

        public bool bMixMode = false;

        private void Start()
        {
            //Create mat instance to avoid changing the shared material

            if(playerMaterialBase)
            {
                Material matVariant = new Material(playerMaterialBase);
                if(_meshRenderer)
                {
                    _meshRenderer.material = matVariant;
                }
            }

            SetColor(GameColorEnum.White, Color.white);
        }

        public void SetColor(GameColorEnum newColor, Color color)
        {
            playerColor = newColor;

            if(_meshRenderer)
            {
                _meshRenderer.material.SetColor("_BaseColor", color);
            }
        }
    }
}