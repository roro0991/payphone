using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip receiverUp, receiverDown, dialogue, dialRinging, bombLight, documentSlide;
    public List<AudioClip> buttonPresses = new List<AudioClip>(); 

    public void ButtonPress()
    {
        int index = Random.Range(0, buttonPresses.Count);
        AudioClip buttonPressClip = buttonPresses[index];
        audioSource.PlayOneShot(buttonPressClip);

    }

    public void DialRing()
    {
        audioSource.volume = .1f; 
        audioSource.clip = dialRinging;
        audioSource.loop = true; 
        audioSource.Play();

    }
    public void ReceiverUP()
    {
        audioSource.volume = 1f;
        audioSource.loop = false;
        audioSource.PlayOneShot(receiverUp);
    }

    public void ReceiverDown()
    {
        audioSource.volume = 1f;
        audioSource.loop = false;
        audioSource.PlayOneShot(receiverDown);
    }

    public void Dialogue()
    {
        audioSource.volume = .5f;
        audioSource.clip = dialogue;
        audioSource.loop = true;
        audioSource.Play(); 
    }

    public void BombLight()
    {
        audioSource.volume = 0.75f;
        audioSource.loop = false;
        audioSource.PlayOneShot(bombLight);
    }

    public void DocumentSlide()
    {
        audioSource.volume = 1f;
        audioSource.loop = false;
        audioSource.PlayOneShot(documentSlide);
    }
}
