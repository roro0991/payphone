
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JSONDialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [SerializeField] private Button continueButton;

    public Animator dialoguePanelAnimator; 

    private Story currentStory; 

    private static JSONDialogueManager instance;

    SFXManager sfxManager;
    PayPhone payPhone;

    private float textSpeed = 0.05f;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    private void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();
        payPhone = FindObjectOfType<PayPhone>();

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0; 
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++; 
        }
    }

    private void Update()
    {        
        if (payPhone.GetReceiverStatus() == false)
        {
            ExitDialogueMode();
        }       
    }

    public static JSONDialogueManager GetInstance()
    {
        return instance; 
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        sfxManager.BombLight();
        foreach (GameObject bombLight in payPhone.bombLights)
        {
            bombLight.GetComponent<Image>().color = Color.green;
        }
        currentStory = new Story(inkJSON.text);
        dialoguePanelAnimator.SetBool("inDialogue", true);
        Invoke("ContinueStory", 0.1f);
    }

    public void ExitDialogueMode()
    {
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
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
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
}
