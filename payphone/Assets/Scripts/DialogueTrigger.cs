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
    bool callInProgress = false;

    private void Start()
    {
        payPhone = FindObjectOfType<PayPhone>();
        sfxManager = FindObjectOfType<SFXManager>();
    }
    private void Update()
    {
        if (!callInProgress)
        {
            if (payPhone.GetPhoneNumber() == "555-0001")
            {
                callInProgress = true;
                StartCoroutine(Call());
            }
        }

        if (payPhone.GetReceiverStatus() == false)
        {
            StopAllCoroutines();
            callInProgress = false;
        }

        IEnumerator Call()
        {
            yield return new WaitForSeconds(1.5f); 
            sfxManager.DialRing();
            yield return new WaitForSeconds(5.5f);
            sfxManager.audioSource.Stop();
            JSONDialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
    }


}

