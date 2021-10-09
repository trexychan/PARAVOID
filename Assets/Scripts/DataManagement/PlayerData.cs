using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

namespace DataManagement
{
    [System.Serializable]
    public class PlayerData
    {
        #region FileData (for storing file specific data)

        public bool empty;
        public string dateAndTime;
        public string playerFileName;

        #endregion

        #region PlayerData (for storing all player game data, vector pos and other stuff)

        public string currentScene; //Stores what scene level player will load back to
        public float[] currentPosition = new float[3]; //Stores player postion in scene level

        #endregion

        public PlayerData(Player player)
        {
            if (player != null)
            {
                empty = false;

                //Stores File specifc data
                dateAndTime = player.dateAndTime;
                playerFileName = player.playerFileName;

                //Stores player current position in currentlevel
                currentPosition[0] = player.currentPosition.x;
                currentPosition[1] = player.currentPosition.y;
                currentPosition[2] = player.currentPosition.z;

                //Stores the last scene the player loaded to
                currentScene = player.currentScene;
            }  
            else
            {
                empty = true;
            }     
        }
    }
}
