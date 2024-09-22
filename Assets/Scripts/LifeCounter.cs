using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    [SerializeField] GameObject heart;
    GameObject player;
    GameObject[] hearts;

    private void Awake()
    {
        player = GameObject.Find("Player");
        hearts = new GameObject[10];
    }
    void Start()
    {
        for (int i = 0; i < player.GetComponent<PlayerCombat>().playerHealth; i++)
        {
            hearts[i] = Instantiate(heart, transform) as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReduceHealth(float damageTaken)
    {
        for (int i = 1; i <= damageTaken; i++)
        {
            Destroy(hearts[player.GetComponent<PlayerCombat>().playerHealth - i]);
        }
    }
}
