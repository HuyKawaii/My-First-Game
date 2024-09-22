using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenuManager : MonoBehaviour
{
    [SerializeField] GameObject deathMenu;
    float deathDelay = 2.0f;
    public void OnPlayerDie()
    {
        StartCoroutine(delayDeathMenu());
    }

    IEnumerator delayDeathMenu()
    {
        yield return new WaitForSeconds(deathDelay);
        deathMenu.SetActive(true);
    }
}
