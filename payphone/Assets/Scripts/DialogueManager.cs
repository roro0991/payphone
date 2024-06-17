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
    bool playerIsInDialogue = false;
    bool coroutineIsRunning = false;
    public Queue<string> lines;
    public Animator dialogueBoxAnimator;
    PayPhone payPhone;
    SFXManager sfxManager;

    void Start()
    {
        sfxManager = FindAnyObjectByType<SFXManager>();
        payPhone = FindObjectOfType<PayPhone>();
        lines = new Queue<string>();
        nameText.text = string.Empty;
        dialogueText.text = string.Empty;
    }

    private void Update()
    {
        if (payPhone.GetReceiverStatus() == false)
        {
            EndDialogue();
        }
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
