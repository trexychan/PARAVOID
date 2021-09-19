using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManagement
{
    public class ExternalData
    {
        #region FileData (for storing all saves and time slots)

        public List<string> files = new List<string>();

        #endregion

        #region SettingsData (for storing data applied to settings)

        public float masterVolume;
        public float musicVolume;
        public float SfxVolume;

        #endregion

        public ExternalData()
        {

        }
    }
}