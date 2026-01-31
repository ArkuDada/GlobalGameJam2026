using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Scene
{
    public class SingleSceneLoader : MonoBehaviour
    {
        [SerializeField]
        SceneObject sceneObject;
        
        bool bSceneLoaded = false;

        public void LoadScene()
        {
            if(sceneObject && !bSceneLoaded)
            {
                bSceneLoaded = true;
                SceneManager.LoadSceneAsync(sceneObject.scenePath);
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