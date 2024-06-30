using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip receiverUp, receiverDown, dialogue, dialRinging, documentSlide, coinPickUp;
    public List<AudioClip> buttonPresses = new List<AudioClip>();
    public List<AudioClip> coinInserts = new List<AudioClip>();

    public void CoinPickUp()
    {
        audioSource.volume = 1f;        
        audioSource.loop = false;
        audioSource.PlayOneShot(coinPickUp);
    }
    public void coinInsert()
    {
        int index = Random.Range(0, coinInserts.Count);
        AudioClip coinInsertClip = coinInserts[index];
        audioSource.PlayOneShot(coinInsertClip); 
    }
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

    public void DocumentSlide()
    {
        audioSource.volume = 1f;
        audioSource.loop = false;
        audioSource.PlayOneShot(documentSlide);
    }
}
