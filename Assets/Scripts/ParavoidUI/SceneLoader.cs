using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DataManagement;

namespace ParavoidUI
{
    public class SceneLoader : MonoBehaviour
    {
        public static void LoadScene(string scene)
        {
            PlayerCarryOverData.UpdatePlayerData(GameObject.Find("Player").GetComponent<Player>());
            SceneManager.LoadSceneAsync(scene);
            Time.timeScale = 1f;
        }

        public void OnTriggerEnter(Collider collider)
        {
            LoadScene("MazeLevelMaster");
        }
    }
}
