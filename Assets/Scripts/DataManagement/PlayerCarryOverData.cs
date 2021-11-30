using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerCarryOverData
{
    public static Player playerDupe = null;

    public static void UpdatePlayerData(Player player)
    {
        if (GameObject.Find("PlayerDupe") != null)
        {
            MonoBehaviour.Destroy(GameObject.Find("PlayerDupe"));
        }

        playerDupe = player;
        playerDupe.name = "PlayerDupe";
        MonoBehaviour.DontDestroyOnLoad(playerDupe);

        //Destroys all irrelevant componenets
        foreach (var comp in playerDupe.GetComponents<Component>())
        {
            if (!(comp is Player) && !(comp is Transform))
            {
                GameObject.Destroy(comp);
            }
        }

        //Destroys all irrelevant children under player
        foreach (Transform child in playerDupe.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
