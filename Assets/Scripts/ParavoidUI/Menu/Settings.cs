using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataManagement;

namespace ParavoidUI
{
    public class Settings : MonoBehaviour
    {
        public Player player;
        #region General Variables

        #endregion

        #region Graphics Variables

        #endregion

        #region Audio Variables

        Slider masterVolume;
        Slider musicVolume;
        Slider SFXVolume;

        #endregion

        #region Controls Variables

        #endregion

        public void Awake()
        {
            player = GameObject.Find("Player").GetComponent<Player>();

            #region Initilize Audio Variables
            masterVolume = transform.Find("Audio").Find("MasterVolume").Find("VolumeAdjuster").GetComponent<Slider>();
            musicVolume = transform.Find("Audio").Find("MusicVolume").Find("VolumeAdjuster").GetComponent<Slider>();
            SFXVolume = transform.Find("Audio").Find("SFXVolume").Find("VolumeAdjuster").GetComponent<Slider>();
            #endregion

            ReloadSettingsData();
        }

        public void ApplySettingsData()
        {
            #region Saving Audio Variables

            player.masterVolume = masterVolume.value;
            player.musicVolume = musicVolume.value;
            player.SFXVolume = SFXVolume.value;

            #endregion

            SaveSystem.SerializeGameFiles(player);
        }

        public void ReloadSettingsData()
        {
            UniversalData data = SaveSystem.DeserializeGameFiles();

            #region Loading Audio Variables

            masterVolume.value = data.masterVolume; 
            musicVolume.value = data.musicVolume;
            SFXVolume.value = data.SFXVolume;

            #endregion
        }

        public void SetDefaultSettings()
        {
            
        }


    }
}
