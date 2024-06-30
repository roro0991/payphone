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
    [SerializeField] public GameObject[] bombLights = new GameObject[3];

    int?[] phoneNumber = new int?[7];

    int currentPhoneNumberIndex = 0;
    int triggeredBombLightIndex = 0;
    int currentDigitIndex = 0;
    int numberAsInt;

    string numberToDisplay;

    public Animator receiverAnimator;
    public Animator documentAnimator;

    [SerializeField] SFXManager sfxManager;
    [SerializeField] DialogueTrigger dialogueTrigger;
    [SerializeField] CoinSlot coinSlot;
    [SerializeField] SevenSegmentDisplay sevenSegmentDisplay;

    bool receiverIsPickedUp = false;

    private void Update()
    {
        string numberAsString = String.Join(string.Empty, phoneNumber);
        int.TryParse(numberAsString, out numberAsInt);
    }


    public void NumberButton(int value)
    {
        if (!receiverIsPickedUp
            | coinSlot.GetCoinCount() == 0
            || dialogueTrigger.GetIsDailingStatus() == true)
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
            if (dialogueTrigger.GetBombCounterTriggerStatus() == false)
            {
                sfxManager.audioSource.Stop();
                sfxManager.ReceiverDown();
            }
            else if (dialogueTrigger.GetBombCounterTriggerStatus() == true)
            {
                sfxManager.audioSource.Stop();
                sfxManager.BombLight();
                dialogueTrigger.SetBombCounterTriggerStatus(false);
                bombLights[triggeredBombLightIndex].GetComponent<Image>().color = Color.red;
                triggeredBombLightIndex++;
            }

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