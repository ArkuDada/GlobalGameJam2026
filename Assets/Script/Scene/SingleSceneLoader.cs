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
        
        public void ReloadScene()
        {
            if(sceneObject)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}