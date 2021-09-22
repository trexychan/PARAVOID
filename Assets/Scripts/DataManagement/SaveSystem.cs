using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

        //File Saving and Loading Methods
        public static void SerializeGameFiles(Player player)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/external.gameData";
            FileStream stream = new FileStream(path, FileMode.Create);

            ExternalData data = new ExternalData(player);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static ExternalData DeserializeGameFiles()
        {
            string path = Application.persistentDataPath + "/external.gameData";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                ExternalData data = formatter.Deserialize(stream) as ExternalData;
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
