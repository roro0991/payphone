using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")] 
    [SerializeField] private TextAsset samBoyd; //726-2693
    [SerializeField] private TextAsset nineOneOne; 

    [SerializeField] PayPhone payPhone;
    [SerializeField] SFXManager sfxManager;
    [SerializeField]DialogueManager dialogueManager;

    int numberToCall;

    float ringTime;

    bool isDailing = false;
    bool callInProgress = false;
  
    private void Update()
    {
        int numberToCall = payPhone.GetPhoneNumber();

        if (!callInProgress && numberToCall.ToString().Length == 3)
        {
            switch (numberToCall)
            {
                case 0:
                    break;
                case 911:
                    callInProgress = true;
                    StartCoroutine(Call(nineOneOne));
                    break;
                default:
                    break;
            }
        }
        else if (!callInProgress && numberToCall.ToString().Length == 7)
        {
            switch (numberToCall)
            {
                case 0:
                    break;
                case 7262693:
                    callInProgress = true;
                    StartCoroutine(Call(samBoyd));
                    break;
                default:
                    callInProgress = true;
                    StartCoroutine(NumberNotInService());                    
                    break;
            }
        }

        if (payPhone.GetReceiverStatus() == false)
        {
            StopAllCoroutines();
            isDailing = false; 
            callInProgress = false;
        }

    }
    IEnumerator Call(TextAsset Number)
    {
        ringTime = Random.Range(5.5f, 10.5f);
        yield return new WaitForSeconds(1.5f);        
        sfxManager.DialRing();
        isDailing = true;
        yield return new WaitForSeconds(ringTime);
        sfxManager.audioSource.Stop();
        isDailing = false;
        dialogueManager.EnterDialogueMode(Number); 
    }

    IEnumerator NumberNotInService()
    {
        yield return new WaitForSeconds(1.5f);
        sfxManager.DialRing();
        isDailing = true;
        yield return new WaitForSeconds(3.5f);
        isDailing = false;
        sfxManager.audioSource.Stop();
        dialogueManager.NotInService();
    }

    public bool GetIsDailingStatus()
    {
        return isDailing; 
    }

    public bool GetInCallStatus()
    {
        return callInProgress; 
    }
}

