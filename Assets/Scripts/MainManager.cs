using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;
    public int lastScore;
    public int lastLevel;

    [System.Serializable]
    class SaveData
    {
        public int level;
        public int score;
    }  

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadLevel();
    }

    public void UpdateScore()
    {
        lastScore = LevelManager.instance.score;
    }

    public void SaveLevel()
    {
        SaveData newData = new SaveData();
        newData.level = lastLevel;
        newData.score = lastScore;

        string json = JsonUtility.ToJson(newData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadLevel()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            lastLevel = saveData.level;
            lastScore = saveData.score;
        }
    }

    public void ResetLevel()
    {
        lastScore = 0;
        lastLevel = 1;
    }
}
