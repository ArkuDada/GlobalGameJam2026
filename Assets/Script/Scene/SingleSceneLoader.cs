using System;
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

        private void Awake()
        {
            bSceneLoaded = false;   
        }

        public void LoadScene()
        {
            if(sceneObject && !bSceneLoaded)
            {
                bSceneLoaded = true;
                SceneManager.LoadSceneAsync(sceneObject.name);
            }
        }
        
        public void ReloadScene()
        {
            if(sceneObject && !bSceneLoaded)
            {
                bSceneLoaded = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}