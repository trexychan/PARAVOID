using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DataManagement;

namespace ParavoidUI
{
    public class Settings : MonoBehaviour
    {
        [Header("General Values")]
        public Slider sensitivity;

        //[Header("Graphics Values")]

        [Header("Audio Values")]
        public Slider masterVolume;
        public Slider musicVolume;
        public Slider SFXVolume;

        [Header("Controls Values")]

        public Transform keyBind_forward;
        public Transform keyBind_backward;
        public Transform keyBind_right;
        public Transform keyBind_left;
        public Transform keyBind_interact;

        private PlayerControls playerControls;

        [SerializeField] private GameObject rebindingBox;

        public void Awake()
        {
            playerControls = new PlayerControls();

            #region Initilize General Variables
            sensitivity = transform.Find("General").Find("Sensitivity").Find("VolumeAdjuster").GetComponent<Slider>();
            #endregion

            #region Initilize Audio Variables
            masterVolume = transform.Find("Audio").Find("MasterVolume").Find("VolumeAdjuster").GetComponent<Slider>();
            musicVolume = transform.Find("Audio").Find("MusicVolume").Find("VolumeAdjuster").GetComponent<Slider>();
            SFXVolume = transform.Find("Audio").Find("SFXVolume").Find("VolumeAdjuster").GetComponent<Slider>();
            #endregion

            #region Initilize Controls Variables
            keyBind_forward = transform.Find("Controls").Find("key_forward");
            keyBind_backward = transform.Find("Controls").Find("key_backward");
            keyBind_right = transform.Find("Controls").Find("key_right");
            keyBind_left = transform.Find("Controls").Find("key_left");
            keyBind_interact = transform.Find("Controls").Find("key_interact");

            keyBind_forward.GetComponent<Button>().onClick.AddListener(delegate
            {
                keyBind_forward.Find("text_key").GetComponent<Text>().text = SetKeyBinding(0);
            });
            #endregion

            ReloadSettingsData();
        }

        public void ApplySettingsData()
        {
            SaveSystem.SerializeGameFiles(this);
        }

        public void ReloadSettingsData()
        {
            UniversalData data = SaveSystem.DeserializeGameFiles();

            SetDefaultSettings();

            if (data == null)
            {
                return;
            }

            #region Loading General Variables

            sensitivity.value = data.sensitivity;

            #endregion

            #region Loading Audio Variables

            masterVolume.value = data.masterVolume;
            musicVolume.value = data.musicVolume;
            SFXVolume.value = data.SFXVolume;

            #endregion

            #region Loading Controls Variables

            keyBind_forward.Find("text_key").GetComponent<Text>().text = data.keyBind_forward;
            keyBind_backward.Find("text_key").GetComponent<Text>().text = data.keyBind_backward;
            keyBind_right.Find("text_key").GetComponent<Text>().text = data.keyBind_right;
            keyBind_left.Find("text_key").GetComponent<Text>().text = data.keyBind_left;
            keyBind_interact.Find("text_key").GetComponent<Text>().text = data.keyBind_interact;

            #endregion
        }

        public void SetDefaultSettings()
        {
            #region Loading Default Audio Variables

            sensitivity.value = 0.5f;

            #endregion

            #region Loading Default Audio Variables

            masterVolume.value = 1;
            musicVolume.value = 1;
            SFXVolume.value = 1;

            #endregion

            #region Loading Default Controls Variables

            keyBind_forward.Find("text_key").GetComponent<Text>().text = "W";
            keyBind_backward.Find("text_key").GetComponent<Text>().text = "S";
            keyBind_right.Find("text_key").GetComponent<Text>().text = "A";
            keyBind_left.Find("text_key").GetComponent<Text>().text = "D";
            keyBind_interact.Find("text_key").GetComponent<Text>().text = "E";

            #endregion
        }

        public string SetKeyBinding(int type)
        {
            playerControls.Player.Disable();

            GameObject rebindBox = Instantiate(rebindingBox);
            rebindBox.transform.SetParent(transform.parent.parent.parent, false);

            playerControls.Player.Move.PerformInteractiveRebinding()
                .WithControlsExcluding("<Mouse>")
                .WithCancelingThrough("<Keyboard>/Backspace")
                .WithTargetBinding(type)
                .OnCancel(callback =>
                {
                    callback.Dispose();
                    Destroy(rebindBox);
                    playerControls.Player.Enable();
                })
                .OnComplete(callback =>
                {
                    callback.Dispose();
                    Destroy(rebindBox);
                    playerControls.Player.Enable();
                })
                .Start();

            return "";
        }

    }
}
