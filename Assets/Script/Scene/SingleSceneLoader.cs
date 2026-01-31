using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Scene
{
    public class SingleSceneLoader : MonoBehaviour
    {
        [SerializeField]
        SceneObject sceneObject;

        public void LoadScene()
        {
            if(sceneObject)
            {
                SceneManager.LoadScene(sceneObject.scenePath);
            }
        }
    }
}