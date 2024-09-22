using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource whoosh;
    public AudioSource footstep;
    public AudioSource hit;
    public AudioSource gotHit;
    public AudioSource death;
    public AudioSource buttomHover;
    public AudioSource buttomClick;
    public AudioSource jump;
    public AudioSource collectCoin;
    public AudioSource[] audioSources;

    public void Whoosh()
    {
        whoosh.Play();
    }

    public void Footstep()
    {
        footstep.Play();
    }

    public void Hit()
    {
        hit.Play();
    }

    public void GotHit()
    {
        gotHit.Play();
    }

    public void Death()
    {
        death.Play();
    }

    public void ButtomHover()
    {
        buttomHover.Play();
    }

    public void ButtomClick()
    {
        buttomClick.Play();
    }

    public void Jump()
    {
        jump.Play();
    }
    public void CollectCoin()
    {
        collectCoin.Play();
    }
}
