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
            Time.timeScale = 1f;
            PlayerCarryOverData.UpdatePlayerData(GameObject.Find("Player").GetComponent<Player>());
            //GameObject.Find("VisualCanvas").GetComponent<Fader>().SceneTransitioner();
            SceneManager.LoadSceneAsync(scene);
        }

        public void OnTriggerEnter(Collider collider)
        {
            LoadScene("MazeLevelMaster");
        }


    }
}
