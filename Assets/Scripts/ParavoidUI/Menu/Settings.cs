using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataManagement;

namespace ParavoidUI
{
    public class Settings : MonoBehaviour
    {
        #region General Variables

        #endregion

        #region Graphics Variables

        #endregion

        #region Audio Variables

        public Slider masterVolume;
        public Slider musicVolume;
        public Slider SFXVolume;

        #endregion

        #region Controls Variables

        #endregion

        public void Awake()
        {
            #region Initilize Audio Variables
            masterVolume = transform.Find("Audio").Find("MasterVolume").Find("VolumeAdjuster").GetComponent<Slider>();
            musicVolume = transform.Find("Audio").Find("MusicVolume").Find("VolumeAdjuster").GetComponent<Slider>();
            SFXVolume = transform.Find("Audio").Find("SFXVolume").Find("VolumeAdjuster").GetComponent<Slider>();
            #endregion

            ReloadSettingsData();
        }

        public void ApplySettingsData()
        {
            SaveSystem.SerializeGameFiles(this);
            UpdateChanges();
        }

        public void ReloadSettingsData()
        {
            UniversalData data = SaveSystem.DeserializeGameFiles();

            if (data == null)
            {
                SetDefaultSettings();
                SaveSystem.SerializeGameFiles(this);
                return;
            }

            #region Loading Audio Variables

            masterVolume.value = data.masterVolume;
            musicVolume.value = data.musicVolume;
            SFXVolume.value = data.SFXVolume;

            #endregion

            UpdateChanges();
        }

        public void SetDefaultSettings()
        {
            #region Loading Default Audio Variables

            masterVolume.value = 1;
            musicVolume.value = 1;
            SFXVolume.value = 1;

            #endregion
        }

        public void UpdateChanges()
        {

        }


    }
}
