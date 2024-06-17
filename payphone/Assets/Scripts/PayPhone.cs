using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PayPhone : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Display;
    bool receiverIsPickedUp = false;
    public Animator receiverAnimator;
    SFXManager sfxManager;
    DialogueManager dialogueManager;

    private void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        if (dialogueManager.GetInDialogueStatus() == true && Input.GetButtonDown("Fire1"))
        {
            dialogueManager.DisplayNextSentence();
        }
    }

    public void Execute()
    {
        if (Display.text == "4166276911")
        {
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();
            sfxManager.DialRing();            
        }
    }

    public void Number(int number)
    {
        if (receiverIsPickedUp)
        {
            sfxManager.ButtonPress();
            Display.text += number.ToString(); ;
        }
    }

    public void Symbol(string symbol)
    {
        if (receiverIsPickedUp)
        {
            sfxManager.ButtonPress(); 
            Display.text += symbol.ToString();
        }        
    }

    public void PickUpReceiver()
    {
        if (!receiverIsPickedUp)
        {
            sfxManager.ReceiverUP(); 
            receiverAnimator.SetBool("OffTheHook", true);
            receiverIsPickedUp = true;
        }
        else if (receiverIsPickedUp)
        {
            sfxManager.ReceiverDown();
            receiverAnimator.SetBool("OffTheHook", false);            
            receiverIsPickedUp = false;
            Display.text = string.Empty; 
        }
    }

    public bool GetReceiverStatus()
    {
        return receiverIsPickedUp;
    }
}
