using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameScore : MonoBehaviour
{
    TextMeshProUGUI scoreUI;
    void Start()
    {
        scoreUI = GetComponent<TextMeshProUGUI>();
        scoreUI.text = "Your score is: " + MainManager.instance.lastScore;
    }

}
