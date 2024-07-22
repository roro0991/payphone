using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    [SerializeField] private Animator cameraAnimator;
    private int currentCameraPosIndex;
    private void Start()
    {
        cameraAnimator.SetInteger("CameraView", 0);
        currentCameraPosIndex = 0;
    }
    
    public void RotateCameraRight()
    {
        if (currentCameraPosIndex < 3)
        {
            currentCameraPosIndex++;
        }
        else
        {
            currentCameraPosIndex = 0;
        }
        cameraAnimator.SetInteger("CameraView", currentCameraPosIndex);
    }

    public void RotateCameraLeft()
    {
        if (currentCameraPosIndex > 0)
        {
            currentCameraPosIndex--;
            
        }
        else
        {
            currentCameraPosIndex = 3;
        }
        cameraAnimator.SetInteger("CameraView", currentCameraPosIndex);
    }
}
