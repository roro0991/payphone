using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    float textSpeed = 0.05f;
    
    public Queue<string> lines;

    bool playerIsInDialogue = false;
    bool coroutineIsRunning = false;
    
    PayPhone payPhone;
    SFXManager sfxManager;

    public Animator dialogueBoxAnimator;
    void Start()
    {
        nameText.text = string.Empty;
        dialogueText.text = string.Empty;
        
        lines = new Queue<string>();

        sfxManager = FindAnyObjectByType<SFXManager>();        
        payPhone = FindObjectOfType<PayPhone>();
    }


    public void StartDialogue(Dialogue dialogue)
    {
        dialogueBoxAnimator.SetBool("inDialogue", true); 

        playerIsInDialogue = true;

        nameText.text = dialogue.name;

        lines.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            lines.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
         if (lines.Count == 0)
         {
             EndDialogue();
             return;
         }
         string line = lines.Dequeue();
         StopAllCoroutines();
         StartCoroutine(TypeLine(line));
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        playerIsInDialogue = false;
        nameText.text = string.Empty;
        dialogueText.text = string.Empty;
        dialogueBoxAnimator.SetBool("inDialogue", false);
    }

    IEnumerator TypeLine(string line)
    {
        coroutineIsRunning = true;
        sfxManager.Dialogue();
        dialogueText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        sfxManager.audioSource.Stop();
        sfxManager.audioSource.loop = false;
        coroutineIsRunning = false;
    }

    public bool GetInDialogueStatus()
    {
        return playerIsInDialogue; 
    }

    public bool GetCoroutineStatus()
    {
        return coroutineIsRunning;
    }
}
