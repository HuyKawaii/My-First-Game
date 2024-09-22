using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class HighScore : MonoBehaviour
{
    int highScore;
    int score;
    TextMeshProUGUI highScoreUI;

    private void Awake()
    {
        highScoreUI = GetComponent<TextMeshProUGUI>();
        score = MainManager.instance.lastScore;
       
    }
    void Start()
    {
        highScore = GetHighScore();
        if (score > highScore)
        {
            highScoreUI.text = "New record: " + score;
            highScore = score;
            SaveHighScore(score);
        }else
        {
            highScoreUI.text = "High score: " + highScore;
        }
    }

    int GetHighScore()
    {
        int scoreSaved;
        string path = Application.persistentDataPath + "/highscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            scoreSaved = JsonUtility.FromJson<int>(json);
        }else
        {
            scoreSaved = 0;
        }

        return scoreSaved;
    }

    void SaveHighScore(int newScore)
    {
        string path = Application.persistentDataPath + "/highscore.json";
        string json = JsonUtility.ToJson(newScore);
        File.WriteAllText(path, json);
    }
}
