using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")] 
    [SerializeField] private TextAsset inkJSON;
    PayPhone payPhone;
    SFXManager sfxManager;
    float ringTime;
    bool callInProgress = false;
    bool bombCounterTriggered = false;

    private void Start()
    {
        payPhone = FindObjectOfType<PayPhone>();
        sfxManager = FindObjectOfType<SFXManager>();
    }
    private void Update()
    {
        if (!callInProgress)
        {
            if (payPhone.GetPhoneNumber().Length == 8)
            {
                if (payPhone.GetPhoneNumber() == "726-2693")
                {                    
                    callInProgress = true;
                    StartCoroutine(Call());
                }
                else
                {
                    callInProgress = true; 
                    StartCoroutine(NumberNotInService());
                    bombCounterTriggered = true;
                }
            }
        }

        if (payPhone.GetReceiverStatus() == false)
        {
            StopAllCoroutines();
            callInProgress = false;
        }

    }
    IEnumerator Call()
    {
        ringTime = Random.Range(5.5f, 10.5f);
        yield return new WaitForSeconds(1.5f); 
        sfxManager.DialRing();
        yield return new WaitForSeconds(ringTime);
        sfxManager.audioSource.Stop();
        JSONDialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }

    IEnumerator NumberNotInService()
    {
        yield return new WaitForSeconds(1.5f);
        sfxManager.DialRing();
        yield return new WaitForSeconds(3.5f);
        sfxManager.audioSource.Stop();
        JSONDialogueManager.GetInstance().NotInService();
    }

    public bool GetBombCounterTriggerStatus()
    {
        return bombCounterTriggered;
    }

    public void SetBombCounterTriggerStatus(bool triggerStatus)
    {
        bombCounterTriggered = triggerStatus; 
    }

}

