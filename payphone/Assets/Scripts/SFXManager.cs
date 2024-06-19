using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip receiverUp, receiverDown, dialogue, dialRinging;
    public List<AudioClip> buttonPresses = new List<AudioClip>(); 

    public void ButtonPress()
    {
        audioSource.volume = 1f;
        int index = Random.Range(0, buttonPresses.Count);
        AudioClip buttonPressClip = buttonPresses[index];
        audioSource.clip = buttonPressClip;
        audioSource.Play();

    }

    public void DialRing()
    {
        audioSource.volume = .2f; 
        audioSource.clip = dialRinging;
        audioSource.loop = true; 
        audioSource.Play();

    }
    public void ReceiverUP()
    {
        audioSource.volume = 1.25f;
        audioSource.clip = receiverUp;
        audioSource.Play();
    }

    public void ReceiverDown()
    {
        audioSource.volume = 1.25f;
        audioSource.clip = receiverDown;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void Dialogue()
    {
        audioSource.volume = .5f;
        audioSource.clip = dialogue;
        audioSource.loop = true;
        audioSource.Play(); 
    }
}
