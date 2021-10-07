using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ParavoidUI;

namespace DataManagement
{   
    public static class SaveSystem
    {
        //Player Saving and Loading Methods
        public static void SerializePlayerData(Player player, string slotName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/" + slotName +".playerData";
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerData data = new PlayerData(player);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static PlayerData DeserializePlayerData(string slotName)
        {
            string path = Application.persistentDataPath + "/" + slotName +".playerData";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
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

        public static bool doesFileExist(string slotName)
        {
            string path = Application.persistentDataPath + "/" + slotName +".playerData";

            if (File.Exists(path))
                return true;

            return false;
        }

        //File Saving and Loading Methods
        public static void SerializeGameFiles(Settings settings, Player player)
        {
            UniversalData data = new UniversalData(settings, player);

            BinaryFormatter formatter = new BinaryFormatter();
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
                BinaryFormatter formatter = new BinaryFormatter();
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

        public static void DeleteFile(string slotName)
        {
            string path = Application.persistentDataPath + "/" + slotName +".playerData";
            if (File.Exists(path))
                File.Delete(path);      
        }
    } 
}
