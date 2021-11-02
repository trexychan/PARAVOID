using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ParavoidUI;

namespace DataManagement
{
    public static class SaveSystem
    {
        private static string playerDataExt = "playerdata";
        private static BinaryFormatter formatter = new BinaryFormatter();

        //Player Saving and Loading Methods
        public static void SerializePlayerData(Player player, string slotName)
        {
            string path = Application.persistentDataPath + "/" + slotName + "." + playerDataExt;
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerData data = new PlayerData(player);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static PlayerData DeserializePlayerData(string slotName)
        {
            string path = Application.persistentDataPath + "/" + slotName + "." + playerDataExt;
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);

                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogError("Save file not found in " + path);
                return null;
            }
        }

        public static bool DoesFileExist(string slotName)
        {
            string path = Application.persistentDataPath + "/" + slotName + "." + playerDataExt;

            if (File.Exists(path))
                return true;

            return false;
        }

        public static void DeleteFile(string slotName)
        {
            string path = Application.persistentDataPath + "/" + slotName + "." + playerDataExt;
            if (File.Exists(path))
                File.Delete(path);
        }

        public static List<string> GetPlayerFiles()
        {
            var filesInDir = Directory
                .GetFiles(Application.persistentDataPath, "*." + playerDataExt, SearchOption.TopDirectoryOnly);

            List<string> files = new List<string>();


            //Debug.Log(filesInDir.Length);

            for (int i = 0; i < filesInDir.Length; i++)
                files.Add(Path.GetFileNameWithoutExtension(filesInDir[i]));

            return files;
        }

        //Settings Saving and Loading
        public static void SerializeGameFiles(Settings settings)
        {
            UniversalData data = new UniversalData(settings);

            string path = Application.persistentDataPath + "/universal.gameData";
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static UniversalData DeserializeGameFiles()
        {
            string path = Application.persistentDataPath + "/universal.gameData";
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);

                UniversalData data = formatter.Deserialize(stream) as UniversalData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogError("GameData not found in " + path);
                return null;
            }
        }
    }
}
