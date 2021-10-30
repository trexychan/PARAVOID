using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ParavoidUI
{
    public class SceneLoader : MonoBehaviour
    {
        public static void LoadScene(string scene)
        {
            SceneManager.LoadSceneAsync(scene);
            Time.timeScale = 1f;
        }
    }
}
