using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtomSound : MonoBehaviour
{
    public AudioManager audioManager;

    private void OnMouseEnter()
    {
        audioManager.buttomHover.Play();
    }
}
