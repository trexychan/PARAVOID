using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManagement
{
    [System.Serializable]
    public class UniversalData
    {
        #region FileData (for storing all saves and time slots)

        public List<string> files = new List<string>();

        #endregion

        #region Settings Preferences (for storing data applied to settings)

        public float masterVolume;
        public float musicVolume;
        public float SFXVolume;

        #endregion

        public UniversalData(Player player)
        {
            //Stores SaveFile Data
            files = player.files;

            //Stores Settings Preferences
            masterVolume = player.masterVolume;
            musicVolume = player.musicVolume;
            SFXVolume = player.SFXVolume;
        }
    }
}