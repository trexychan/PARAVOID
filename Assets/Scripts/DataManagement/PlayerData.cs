using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManagement
{
    public class PlayerData
    {
        #region FileData (for storing all saves and time slots)
        public string dateAndTime;

        #endregion

        #region PlayerData (for storing all player game data, vector pos and other stuff)

        public int currentLevel; //Stores what scene level player will load back to
        public float[] position; //Stores player postion in scene level

        #endregion

        public PlayerData()
        {

        }
    }
}
