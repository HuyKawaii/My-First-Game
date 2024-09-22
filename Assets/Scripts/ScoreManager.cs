using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    TextMeshProUGUI scoreUI;

    public void Start()
    {
        scoreUI = GetComponent<TextMeshProUGUI>();
    }
    public void Update()
    {
        scoreUI.text = "Score: " + LevelManager.instance.score;
    }
}
