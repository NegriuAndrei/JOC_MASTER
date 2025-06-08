using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public SaveData activeSave;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



   public void LoadGame()
    {
        string dataPath = Application.persistentDataPath;
        Debug.Log(dataPath+" -------------------------------------");

        if(File.Exists(dataPath + "/Save Game.data"))
        {
            
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/Save Game.data", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();
            Debug.Log("Data Loaded");
        }
    }

    public void SaveGame()
    {
        string dataPath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/Save Game.data", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();
        Debug.Log("Data Saved");
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }


}

[System.Serializable]
public class SaveData
{
    public int  currentCoins,attackDamage ;
    public float maxMagic, maxHealth,healthRegenspeed,magicRegenspeed;

    public int playerLevel = 1;
    public int[] expToNextLevel;
    public int maxLevel = 5;
    public int currentEXP;
    public int baseEXP = 500;
}