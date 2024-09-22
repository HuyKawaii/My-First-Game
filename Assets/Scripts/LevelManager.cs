using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int score;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        MainManager.instance.lastLevel = SceneManager.GetActiveScene().buildIndex;
        score = MainManager.instance.lastScore;
    }

    public void GainScore(int scoreGained)
    {
        score += scoreGained;
    }
}
