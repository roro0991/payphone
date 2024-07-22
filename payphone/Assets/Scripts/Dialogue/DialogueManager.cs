
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SearchService;
using Ink.UnityIntegration; 

public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button continueButton;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [SerializeField] private SFXManager sfxManager;
    [SerializeField] private PayPhone payPhone;

    public Animator dialoguePanelAnimator;

    [SerializeField] private InkFile globalsInkFile; 
    private Story currentStory;
    private DialogueVariables dialogueVariables; 

    private float textSpeed = 0.05f;

    private void Awake()
    {
        dialogueVariables = new DialogueVariables(globalsInkFile.filePath);
    }
    private void Start()
    {

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0; 
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++; 
        }
    }


    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueVariables.StartListening(currentStory);
        dialoguePanelAnimator.SetBool("inDialogue", true);
        Invoke("ContinueStory", 0.1f);
    }

    public void ExitDialogueMode()
    {
        if (currentStory != null)
        {
            dialogueVariables.StopListening(currentStory);
        }
        dialoguePanelAnimator.SetBool("inDialogue", false);
        dialogueText.text = string.Empty;                
    }

    public void ContinueStory()
    {
        if (currentStory == null)
        {
            ExitDialogueMode();
            return; 
        }
        if (currentStory.canContinue)
        {
            string line = currentStory.Continue();
            StopAllCoroutines();
            StartCoroutine(TypeLine(line));            
        }
        else
        {
            ExitDialogueMode();
        }
    }

    IEnumerator TypeLine(string line)
    {
        DisableChoices();
        DisableContinueButton();
        sfxManager.Dialogue();
        dialogueText.text = "";
        bool isAddingRichTextTag = false;
        foreach (char letter in line.ToCharArray())
        {
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                dialogueText.text += letter;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
            }
        }
        sfxManager.audioSource.Stop();
        sfxManager.audioSource.loop = false;
        EnableChoices();
        DisplayChoices();
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices; 

        if (currentChoices.Count == 0)
        {
            EnableContinueButton();
        }

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("There are too many choices to fit the UI");
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false); 
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    private void DisableChoices()
    {
        foreach (GameObject choice in choices)
        {
            choice.SetActive(false);
        }
    }

    private void EnableChoices()
    {
        foreach (GameObject choice in choices)
        {
            choice.SetActive(true);
        }
    }
    private void DisableContinueButton()
    {
        continueButton.gameObject.SetActive(false);
    }

    private void EnableContinueButton()
    {
        continueButton.gameObject.SetActive(true);
    }

    public void NotInService()
    {
        dialoguePanelAnimator.SetBool("inDialogue", true);
        string line = "The number you have dialed is not in service.";
        StopAllCoroutines();
        DisableChoices();
        StartCoroutine(TypeLineNotInService(line));

    }   

    IEnumerator TypeLineNotInService(string line)
    {
        DisableChoices();
        DisableContinueButton();
        sfxManager.Dialogue();
        dialogueText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed); 
        }
        sfxManager.audioSource.Stop();
        sfxManager.audioSource.loop = false;
        EnableContinueButton();
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue); 
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue; 
    }
}
