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

    int currentPhoneNumberIndex;
    int triggeredBombLightIndex;

    public Animator receiverAnimator;

    SFXManager sfxManager;
    DialogueTrigger dialogueTrigger;

    bool receiverIsPickedUp;

    private void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();
        dialogueTrigger = FindObjectOfType<DialogueTrigger>();

        currentPhoneNumberIndex = 0;
        triggeredBombLightIndex = 0;

        receiverIsPickedUp = false;
    }

    private void Update()
    {
        string numberAsString = String.Join(string.Empty, phoneNumber); 
        string numberToDisplay = "";
        foreach (char number in numberAsString.ToCharArray())
        {
            numberToDisplay += number;
            if (numberToDisplay.Length == 3)
            {
                numberToDisplay += "-";
            }
        }
        Display.text = numberToDisplay;
    }

    public void NumberButton(int value)
    {
        if (!receiverIsPickedUp)
        {
            sfxManager.ButtonPress();
            return;
        }

        sfxManager.ButtonPress();
        if (currentPhoneNumberIndex < phoneNumber.Length)
        {
            phoneNumber[currentPhoneNumberIndex] = value;
            currentPhoneNumberIndex++; 
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
                sfxManager.ReceiverDown();           
            }
            else if (dialogueTrigger.GetBombCounterTriggerStatus() == true)
            {
                sfxManager.BombLight();
                dialogueTrigger.SetBombCounterTriggerStatus(false);
                bombLights[triggeredBombLightIndex].GetComponent<Image>().color = Color.red;
                triggeredBombLightIndex++;
            }
            
            receiverIsPickedUp = false;
            receiverAnimator.SetBool("OffTheHook", false);
            Array.Clear(phoneNumber, 0, phoneNumber.Length);
            currentPhoneNumberIndex = 0;
        }               
    }

    public string GetPhoneNumber()
    {
        return Display.text;
    }
    public bool GetReceiverStatus()
    {
        return receiverIsPickedUp;
    }
}
