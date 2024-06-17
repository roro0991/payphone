using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    PayPhone payPhone;

    private void Start()
    {
        payPhone = FindObjectOfType<PayPhone>();
    }

    public void TriggerDialogue()
    {
        Invoke("Call", 5f); 
    }

    public void Call()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}

