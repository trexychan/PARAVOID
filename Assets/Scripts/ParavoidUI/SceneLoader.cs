using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

namespace ParavoidUI
{
    public static class SceneLoader
    {
        public static void LoadScene(string scene)
        {
            SceneManager.LoadSceneAsync(scene);
        }
    }
}
