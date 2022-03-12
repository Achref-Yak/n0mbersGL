using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    // Makes it a singleton / single instance
    static public SaveSystem instance;
    string filePath;

    private void Awake()
    {
        // Check there are no other instances of this class in the scene
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        filePath = Application.persistentDataPath + "/save20.data";
    }

    public void SaveGame(GameData saveData)
    {
        string json = JsonUtility.ToJson(saveData);
  
            Debug.Log(json);
            File.WriteAllText(filePath, json);
    
            
    }

    public GameData LoadGame()
    {
        if (File.Exists(filePath))
        {
            // File exists 
            string json = File.ReadAllText(filePath);


            GameData saveData = JsonUtility.FromJson<GameData>(json);
 
            return saveData;
        }
        else
        {
            // File does not exist
            GameData saveData = new GameData();
            string json = JsonUtility.ToJson(saveData);

            Debug.Log(json);
            File.WriteAllText(filePath, json);
            return saveData;
        }
    }
}