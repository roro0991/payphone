using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class CoinSlot : MonoBehaviour, IDropHandler
{
    int coins = 0;

    PayPhone payphone;
    private void Start()
    {
        payphone = FindObjectOfType<PayPhone>(); 
    }
    private void Update()
    {
        Debug.Log("there are " + coins + " coins in the coinslot!");
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;  
        Destroy(dropped.gameObject); 
        if (payphone.GetReceiverStatus() == true)
        {
            coins++;
        }
        else
        {
            return;
        }
    }

    public int GetCoinCount()
    {
        return coins;
    }

    public void ResetCoinCount()
    {
        coins = 0;
    }

}
