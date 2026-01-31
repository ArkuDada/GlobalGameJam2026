using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Scene
{
    public abstract partial class GameScene : ScriptableObject
    {
#if UNITY_EDITOR
        public UnityEditor.SceneAsset sceneAsset;
#endif

        [HideInInspector]
        public string scenePath;
    }
}