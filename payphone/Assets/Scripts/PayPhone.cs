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

    int?[] phoneNumber = new int?[7];

    int currentPhoneNumberIndex;

    public Animator receiverAnimator;

    SFXManager sfxManager;

    bool receiverIsPickedUp;

    private void Start()
    {        
        sfxManager = FindObjectOfType<SFXManager>();

        currentPhoneNumberIndex = 0;

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

    public void Number(int value)
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
            receiverAnimator.SetBool("OffTheHook", true);
            receiverIsPickedUp = true;
        }
        else if (receiverIsPickedUp)
        {            
            sfxManager.ReceiverDown();
            receiverAnimator.SetBool("OffTheHook", false);
            receiverIsPickedUp = false;
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
