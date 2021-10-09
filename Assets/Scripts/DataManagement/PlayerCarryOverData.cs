using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerCarryOverData
{
    public static Player playerDupe = null;

    public static void UpdatePlayerData(Player player)
    {
        playerDupe = player;
        MonoBehaviour.DontDestroyOnLoad(playerDupe);
    }
}
