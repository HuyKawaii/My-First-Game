using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioManager audioManager;
    public float fadeSpeed;
    public float moveSpeed;
    private bool isFading = false;
    private void Update()
    { 
        if (isFading)
        {
            Color coinColor = gameObject.GetComponent<Renderer>().material.color;
            float fadeAmount = coinColor.a - fadeSpeed * Time.deltaTime;
            coinColor = new Color(coinColor.r, coinColor.g, coinColor.b, fadeAmount);
            gameObject.GetComponent<Renderer>().material.color = coinColor;

            float yPos = gameObject.transform.position.y;
            yPos = yPos + moveSpeed * Time.deltaTime;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, yPos, gameObject.transform.position.z);

            if (fadeAmount < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelManager.instance.GainScore(1000);
        audioManager.collectCoin.Play();
        gameObject.layer = 0;
        isFading = true;
    }
}
