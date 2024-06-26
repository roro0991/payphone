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
    int numberAsInt;

    string numberToDisplay;

    public Animator receiverAnimator;
    public Animator documentAnimator;

    [SerializeField] SFXManager sfxManager;
    [SerializeField] DialogueTrigger dialogueTrigger;
    [SerializeField] CoinSlot coinSlot;

    bool receiverIsPickedUp = false;

    private void Update()
    {
        string numberAsString = String.Join(string.Empty, phoneNumber);                
        int.TryParse(numberAsString, out numberAsInt);  
      
        string numberToDisplay = "";
        foreach (char number in numberAsString.ToCharArray())
        {
            numberToDisplay += number;
        }       
        if (numberToDisplay.Length <= 3)
        {
            Display.text = numberToDisplay; 
        }
        else if (numberToDisplay.Length == 4)
        {
            Display.text = String.Format("{0:###-#}", int.Parse(numberToDisplay));
        }
        else if (numberToDisplay.Length == 5)
        {
            Display.text = String.Format("{0:###-##}", int.Parse(numberToDisplay));
        }
        else if (numberToDisplay.Length == 6)
        {
            Display.text = String.Format("{0:###-###}", int.Parse(numberToDisplay));
        }
        else if (numberToDisplay.Length == 7)
        {
            Display.text = String.Format("{0:###-####}", int.Parse(numberToDisplay));
        }
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
