
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Ink.Runtime;
using System;
using UnityEngine.UI;

public class TextAnimator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private TextAsset openingText;
    [SerializeField] private Button continueButton;
    private Story currentStory;
    [SerializeField] float textDelay;

    private void Start()
    {
        StartTextAnimation(openingText); 
    }
    private void StartTextAnimation(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        ContinueStory();
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
        string line = currentStory.Continue();
        StopAllCoroutines();
        StartCoroutine(TypeLine(line));
        }
        
    }

    IEnumerator TypeLine (string line)
    {
        displayText.text = "";
        DisableContinueButton();
        foreach (char letter in line.ToCharArray())
        {
            displayText.text += letter;
            yield return new WaitForSeconds(textDelay);
        }
        EnableContinueButton();
    }

    private void EnableContinueButton()
    {
        continueButton.gameObject.SetActive(true);
    }

    private void DisableContinueButton()
    {
        continueButton.gameObject.SetActive(false);
    }
}
