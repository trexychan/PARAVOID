using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ParavoidUI;

namespace DataManagement
{
    [System.Serializable]
    public class UniversalData
    {
        #region Settings Preferences (for storing data applied to settings)

        //General Values//
        public float sensitivity;

        //Audio Values//
        public float masterVolume;
        public float musicVolume;
        public float SFXVolume;

        //Controls Values//
        public string keyBind_forward;
        public string keyBind_backward;
        public string keyBind_right;
        public string keyBind_left;
        public string keyBind_interact;

        #endregion

        public UniversalData(Settings settings) //Settings is expected to not be null
        {
            #region Saving General Variables

            //Stores Audio Variables
            this.sensitivity = settings.sensitivity.value;

            #endregion

            #region Saving Audio Variables

            //Stores Audio Variables
            this.masterVolume = settings.masterVolume.value;
            this.musicVolume = settings.musicVolume.value;
            this.SFXVolume = settings.SFXVolume.value;

            #endregion

            #region Saving Controls Variables

            //Stores Audio Variables
            this.keyBind_forward = settings.keyBind_forward.Find("text_key").GetComponent<Text>().text;
            this.keyBind_backward = settings.keyBind_backward.Find("text_key").GetComponent<Text>().text;
            this.keyBind_right = settings.keyBind_right.Find("text_key").GetComponent<Text>().text;
            this.keyBind_left = settings.keyBind_left.Find("text_key").GetComponent<Text>().text;
            this.keyBind_interact = settings.keyBind_interact.Find("text_key").GetComponent<Text>().text;

            #endregion              
        }
    }
}