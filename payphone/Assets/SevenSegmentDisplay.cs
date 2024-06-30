using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SevenSegmentDisplay : MonoBehaviour
{
    [SerializeField] public GameObject[] digits = new GameObject[7];

    private void Start()
    {
        ClearAllDigits(); 
    }
    public void ClearAllDigits()
    {
        foreach (GameObject digit in digits)
        {
            digit.GetComponent<DigitController>().ClearDigit();
        }
    }
}
