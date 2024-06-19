
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class JSONDialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory; 

    private static JSONDialogueManager instance;

    SFXManager sfxManager;
    PayPhone payPhone;

    private float textSpeed = 0.05f;

    public Animator dialoguePanelAnimator; 
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
        currentStory = new Story(inkJSON.text);
        dialoguePanelAnimator.SetBool("inDialogue", true);
        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        dialoguePanelAnimator.SetBool("inDialogue", false);
        dialogueText.text = string.Empty;
    }

    public void ContinueStory()
    {
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
        sfxManager.Dialogue();
        dialogueText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        sfxManager.audioSource.Stop();
        sfxManager.audioSource.loop = false;
    }
}
