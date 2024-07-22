using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PayPhone : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Display;

    private int?[] phoneNumber = new int?[7];

    private int currentPhoneNumberIndex = 0;
    private int currentDigitIndex = 0;
    private int numberAsInt;

    private string numberToDisplay;

    public Animator receiverAnimator;
    public Animator documentAnimator;

    [SerializeField] private SFXManager sfxManager;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private DialogueTrigger dialogueTrigger;    
    [SerializeField] private CoinSlot coinSlot;
    [SerializeField] private SevenSegmentDisplay sevenSegmentDisplay;

    private bool receiverIsPickedUp = false;

    private void Update()
    {
        string numberAsString = String.Join(string.Empty, phoneNumber);
        int.TryParse(numberAsString, out numberAsInt);
    }


    public void NumberButton(int value)
    {
        if (!receiverIsPickedUp
            || coinSlot.GetCoinCount() == 0
            || dialogueTrigger.GetIsDailingStatus() == true
            || dialogueTrigger.GetInCallStatus() == true)
        {
            sfxManager.ButtonPress();
            return;
        }
        else
        {
            sfxManager.ButtonPress();
            if (currentPhoneNumberIndex < phoneNumber.Length)
            {
                phoneNumber[currentPhoneNumberIndex] = value;
                currentPhoneNumberIndex++;
                sevenSegmentDisplay.digits[currentDigitIndex].GetComponent<DigitController>().DisplayDigit(value);
                currentDigitIndex++;
            }
        }
    }

    public void PickUpReceiver()
    {
        if (!receiverIsPickedUp)
        {
            sfxManager.ReceiverUP();
            receiverIsPickedUp = true;
            receiverAnimator.SetBool("OffTheHook", true);
        }
        else if (receiverIsPickedUp)
        {
            dialogueManager.ExitDialogueMode();
            sfxManager.audioSource.Stop();
            sfxManager.ReceiverDown();
            receiverIsPickedUp = false;
            receiverAnimator.SetBool("OffTheHook", false);
            Array.Clear(phoneNumber, 0, phoneNumber.Length);
            currentPhoneNumberIndex = 0;
            sevenSegmentDisplay.ClearAllDigits();
            currentDigitIndex = 0;
            coinSlot.ResetCoinCount();
        }
    }

    public int GetPhoneNumber()
    {
        return numberAsInt;
    }

    public bool GetReceiverStatus()
    {
        return receiverIsPickedUp;
    }

}