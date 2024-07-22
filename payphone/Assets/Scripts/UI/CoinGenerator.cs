using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoinGenerator : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private GameObject coin;
    [SerializeField] private SFXManager sfxManager;
    [SerializeField] private Canvas canvas;

    

    public void OnBeginDrag(PointerEventData eventData)
    {
        sfxManager.CoinPickUp();
        GameObject newCoin = Instantiate(coin, eventData.position, Quaternion.identity);
        newCoin.transform.SetParent(canvas.transform, false);
        eventData.pointerDrag = newCoin;        
    }

    public void OnDrag(PointerEventData eventData)
    {
    }
}
