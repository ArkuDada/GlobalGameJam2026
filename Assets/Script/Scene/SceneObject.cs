using System;
using UnityEngine;

namespace Script.Scene
{
    [Serializable]
    public enum SceneEnum
    {
        MainMenu,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        Level9,
        Level10,
        Level11,
        Level12
        
    }

    [CreateAssetMenu(fileName = "SceneSO", menuName = "SceneObject")]
    public class SceneObject : GameScene
    {
        public SceneEnum sceneEnum;
    }
}