using System.IO;
using UnityEngine;
 
public class SaveManager : Singleton<SaveManager>
{
    private const string PATH = "/savegame.json";

    private int coin;
    public int Coin
    {
        get { return coin; }
        set
        {
            coin = value;
            SaveData();
        }
    }

    private int diamond;
    public int Diamond
    {
        get { return diamond; }
        set
        {
            diamond = value;
            SaveData();
        }

    }

    private int unlockLevel;
    public int UnlockLevel
    {
        get { return unlockLevel; }
        set
        {
            unlockLevel = value;
            SaveData();
        }
    }

    private void Awake()
    {
        LoadData();
    }
    public void SaveData()
    {

        //Custom data before saving
        GameData saveData = new GameData
        {
            coin = this.Coin,
            diamond = this.Diamond,
            unlockLevel = this.UnlockLevel,

        };

        string path = Application.persistentDataPath + PATH;
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + PATH;

        //Custom default data
        GameData defaultData = new GameData
        {
            coin = 0,
            diamond = 0,
            unlockLevel = 0,
        };
        if (!File.Exists(path))
        {
            Debug.Log("Cann't load data, file not found");
            this.coin = defaultData.coin;
            this.diamond = defaultData.diamond;
            this.unlockLevel = defaultData.unlockLevel;
            SaveData();
            return;
        }
        string json = File.ReadAllText(path);
        defaultData = JsonUtility.FromJson<GameData>(json);
        this.coin = defaultData.coin;
        this.diamond = defaultData.diamond;
        this.unlockLevel = defaultData.unlockLevel;

    }

}


public class GameData
{
    public int coin;
    public int diamond;
    public int unlockLevel;
}


