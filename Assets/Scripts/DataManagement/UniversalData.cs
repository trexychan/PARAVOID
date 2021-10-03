using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParavoidUI;

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

        public UniversalData(Settings settings, Player player)
        {
            UniversalData data = SaveSystem.DeserializeGameFiles();
        
            if (data != null)
            {
                this.files = player != null ? player.files : data.files;

                #region Saving Audio Variables

                //Stores Audio Variables
                this.masterVolume = settings != null ? 
                    settings.masterVolume.value : data.masterVolume;
                this.musicVolume = settings != null ?
                    settings.musicVolume.value : data.musicVolume;
                this.SFXVolume = settings != null ? 
                    settings.SFXVolume.value : data.SFXVolume;

                #endregion    
            }
            else if (data == null)
            {
                this.files = player != null ? player.files : null;

                #region Saving Audio Variables

                //Stores Audio Variables
                this.masterVolume = settings != null ? 
                    settings.masterVolume.value : 1F;
                this.musicVolume = settings != null ?
                    settings.musicVolume.value : 1F;
                this.SFXVolume = settings != null ? 
                    settings.SFXVolume.value : 1F;

                #endregion   
            }
            
            
        }
    }
}