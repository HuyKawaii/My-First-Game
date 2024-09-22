using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{
    GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelManager.instance.GainScore(player.GetComponent<PlayerCombat>().playerHealth * 500);
        int level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(level + 1);
        MainManager.instance.UpdateScore();
        MainManager.instance.SaveLevel();
    }
}
