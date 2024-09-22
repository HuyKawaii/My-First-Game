using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    GameObject player;
    int spikeDamage = 1;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.GetComponent<PlayerCombat>().GotHit(spikeDamage);
    }
}
