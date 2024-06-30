using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DocumentManager : MonoBehaviour
{
    [SerializeField] SFXManager sfxManager;

    public Animator documentAnimator; 



    bool documentIsOpen = false;

    public void OpenCloseDocument()
    {
        if (documentIsOpen == false)
        {
            sfxManager.DocumentSlide();
            documentIsOpen = true;
            documentAnimator.SetBool("isOpen", true);
        }
        else if (documentIsOpen == true)
        {
            sfxManager.DocumentSlide();
            documentIsOpen = false;
            documentAnimator.SetBool("isOpen", false);
        }
    }
}
