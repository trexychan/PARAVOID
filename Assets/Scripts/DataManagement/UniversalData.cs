using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParavoidUI;

namespace DataManagement
{
    [System.Serializable]
    public class UniversalData
    {
        #region Settings Preferences (for storing data applied to settings)

        public float masterVolume;
        public float musicVolume;
        public float SFXVolume;

        #endregion

        public UniversalData(Settings settings) //Settings is expected to not be null
        {
            #region Saving Audio Variables

            //Stores Audio Variables
            this.masterVolume = settings.masterVolume.value;
            this.musicVolume = settings.musicVolume.value;
            this.SFXVolume =  settings.SFXVolume.value;

            #endregion              
        }
    }
}