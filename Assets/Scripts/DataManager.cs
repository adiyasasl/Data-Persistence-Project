using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string PlayerName;
    public int HighScore;

    private string SavePath => Path.Combine(Application.persistentDataPath, "saveData.json");

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start() 
    {
        PlayerName = LoadSavedData().PlayerName;
        HighScore = LoadSavedData().HighScore;
    }

    public void Save(string PlayerName, int HighScore)
    {
        var existingData = LoadSavedData();
        if (!File.Exists(SavePath) || HighScore > existingData.HighScore)
        {
            var saveData = new SaveData
            {
                PlayerName = PlayerName,
                HighScore = HighScore
            };

            var json = JsonUtility.ToJson(saveData);
            File.WriteAllText(SavePath, json);
        }
    }

    public SaveData LoadSavedData()
    {
        if (!File.Exists(SavePath))
        {
            return new SaveData();
        }

        var json = File.ReadAllText(SavePath);
        return string.IsNullOrEmpty(json) ? new SaveData() : JsonUtility.FromJson<SaveData>(json);
    }

    public void SetName(string name)
    {
        PlayerName = name;
    }

    public void SetHighScore(int score)
    {
        HighScore = score;
    }

    [System.Serializable]
    public class SaveData
    {
        public string PlayerName;
        public int HighScore;
    }
}
