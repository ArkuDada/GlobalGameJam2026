#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Script.Scene
{
    public abstract partial class GameScene : ScriptableObject, ISerializationCallbackReceiver
    {
        private SceneAsset prevSceneAsset;

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            PopulateScenePath();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
        }

        void PopulateScenePath()
        {
            if(sceneAsset != null)
            {
                if(prevSceneAsset != sceneAsset)
                {
                    prevSceneAsset = sceneAsset;
                    scenePath = AssetDatabase.GetAssetPath(sceneAsset);
                }
            }
            else
            {
                scenePath = string.Empty;
            }
        }
    }
}
#endif